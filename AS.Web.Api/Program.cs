using AS.Business.AutoMapperProfile;
using AS.Business.DependencyResolvers;
using AS.Core.Extensions;
using AS.Core.Security;
using AS.Core.Utilities.IoC;
using AS.Core.ValueObjects;
using AS.Data;
using AS.Data.DependencyResolvers;
using AutoMapper;
using IDS.WebApi.Infrastructure;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Diagnostics.HealthChecks;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Pomelo.EntityFrameworkCore.MySql.Infrastructure;
using ServiceStack;
using StackExchange.Redis;
using System.Security.Claims;
using System.Security.Principal;
using System.Text;
using System.Text.Json;
using System.Text.Json.Serialization;
using Swashbuckle.AspNetCore.SwaggerUI;
using Microsoft.OpenApi.Models;
using Microsoft.AspNetCore.Mvc;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddHealthChecks();



builder.Services.AddDependencyResolvers(new ICoreModule[]
{
   // new CoreModule(),
    new DataAccessModule(),
    new BusinessModule(),
});
builder.Services.AddHttpClient();


string configuration = builder.Configuration.GetConnectionString("MySqlConnectionString");
var serverVersion = new MySqlServerVersion(new Version(8, 0, 33));

builder.Services.AddDbContext<EfDbContext>(options => options.UseMySql(
    configuration,
    serverVersion,
    o => o.SchemaBehavior(MySqlSchemaBehavior.Translate, (schema, table) => $"{schema}_{table}")),
ServiceLifetime.Transient
);



builder.Services.AddCors(options => options.AddDefaultPolicy(builder => builder.AllowAnyHeader().AllowAnyMethod().AllowAnyOrigin()));


builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();


builder.Services.AddSwaggerGen(setup =>
{
    // Public API grubu
    setup.SwaggerDoc("is-Public", new OpenApiInfo
    {
        Title = "Public API",
        Version = "v1",
        Description = "Public Endpoints"
    });

    // Private API grubu
    setup.SwaggerDoc("is-Private", new OpenApiInfo
    {
        Title = "Private API",
        Version = "v2",
        Description = "Private Endpoints - Requires Authentication"
    });

    // JWT Authentication için ayarlar
    var jwtSecurityScheme = new OpenApiSecurityScheme
    {
        BearerFormat = "JWT",
        Name = "JWT Authentication",
        In = ParameterLocation.Header,
        Type = SecuritySchemeType.Http,
        Scheme = JwtBearerDefaults.AuthenticationScheme,
        Description = "Put **_ONLY_** your JWT Bearer token on textbox below!",
        Reference = new OpenApiReference
        {
            Id = JwtBearerDefaults.AuthenticationScheme,
            Type = ReferenceType.SecurityScheme
        }
    };

    setup.AddSecurityDefinition(jwtSecurityScheme.Reference.Id, jwtSecurityScheme);

    setup.AddSecurityRequirement(new OpenApiSecurityRequirement
    {
        { jwtSecurityScheme, Array.Empty<string>() }
    });

    // Grupları ApiExplorerSettings'e göre filtrele
    setup.DocInclusionPredicate((docName, apiDesc) =>
    {
        // ApiExplorerSettings'ten grup adını al
        var groupName = apiDesc.ActionDescriptor.EndpointMetadata
            .OfType<ApiExplorerSettingsAttribute>()
            .FirstOrDefault()?.GroupName;

        // Eğer grup adı belirtilmişse, docName ile eşleşip eşleşmediğini kontrol et
        if (!string.IsNullOrEmpty(groupName))
        {
            return docName == groupName;
        }

        // Grup adı belirtilmemişse, varsayılan olarak tüm dokümanlarda göster
        return true;
    });
});

#region ----------AutoMapper -------------

var mappingConfig = new MapperConfiguration(mc =>
{
    //mc.AddProfile<BusinessProfile>(builder.Configuration);
    mc.AddProfile(new BusinessProfile(builder.Configuration));
    mc.AddProfile(new ModelProfile(builder.Configuration));
});
IMapper mapper = mappingConfig.CreateMapper();
builder.Services.AddSingleton(mapper);
builder.Services.AddAutoMapper(typeof(BusinessProfile), typeof(ModelProfile));

#endregion------- AutoMapper----------

builder.Services.AddHttpContextAccessor();
builder.Services.AddTransient<IHttpContextAccessor, HttpContextAccessor>();
builder.Services.AddHttpClient();
ClaimsPrincipal GetPrincipal(IServiceProvider sp) =>
    sp.GetService<IHttpContextAccessor>()?.HttpContext?.User ??
    new ClaimsPrincipal(new ClaimsIdentity("Unknown"));

builder.Services.AddScoped<IPrincipal>(GetPrincipal);

//builder.Services.AddTransient<IPrincipal>(provider => provider.GetService<IHttpContextAccessor>().HttpContext.User);
//builder.Services.AddControllers(options =>
//{
//    options.Filters.Add<HttpResponseExceptionFilter>();
//});

//builder.Services.AddExceptionHandler<CustomExceptionHandler>(new ConfigurationOptions
//{
//    EndPoints = { "xdfs" },
//    AbortOnConnectFail = false,
//    SyncTimeout = 15000,
//    AsyncTimeout = 10000,
//});


builder.Services.AddControllers()
    .AddJsonOptions(p => { p.JsonSerializerOptions.ReferenceHandler = ReferenceHandler.IgnoreCycles; });




var redisEndpoint = builder.Configuration.GetSection("Redis:Endpoint").Value ?? "";

var multiplexer = ConnectionMultiplexer.Connect(new ConfigurationOptions
{
    EndPoints = { redisEndpoint },
    AbortOnConnectFail = false,
    SyncTimeout = 15000,
    AsyncTimeout = 10000,
});
multiplexer.ConnectionFailed += Multiplexer_ConnectionFailed;
multiplexer.ConnectionRestored += Multiplexer_ConnectionRestored;

void Multiplexer_ConnectionRestored(object sender, ConnectionFailedEventArgs e)
{
    // TODO: should be logged. 
    Console.WriteLine("Redis connection restored at: " + DateTime.Now.ToLongDateString());
}

void Multiplexer_ConnectionFailed(object sender, ConnectionFailedEventArgs e)
{
    // TODO: should be logged. 
    Console.WriteLine("Connecting to redis failed: " + e?.Exception?.Message ?? String.Empty);
}

builder.Services.AddSingleton<IConnectionMultiplexer>(multiplexer);


//JWT
builder.Services.Configure<JwtSettings>(builder.Configuration.GetSection(nameof(JwtSettings)));
var jwtSettings = builder.Configuration.GetSection(nameof(JwtSettings)).Get<JwtSettings>();

builder.Services.AddAuthorization().AddAuthentication(options =>
{
    options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
    options.DefaultScheme = JwtBearerDefaults.AuthenticationScheme;
    options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
})
    .AddJwtBearer(options =>
    {
        options.RequireHttpsMetadata = false;
        options.SaveToken = true;
        options.TokenValidationParameters = new Microsoft.IdentityModel.Tokens.TokenValidationParameters
        {
            ValidIssuer = jwtSettings.ValidIssuer,
            ValidateIssuer = jwtSettings.ValidateIssuer,
            ValidateAudience = jwtSettings.ValidateAudience,
            ValidAudience = jwtSettings.ValidAudience,
            ValidateLifetime = jwtSettings.ValidateLifetime,
            ValidateIssuerSigningKey = Convert.ToBoolean(jwtSettings.ValidateIssuerSigningKey),
            IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(jwtSettings.SecretKey)),
            ClockSkew = TimeSpan.Zero
        };
        options.Events = new JwtBearerEvents
        {
            OnTokenValidated = ctx => Task.CompletedTask,
            OnAuthenticationFailed = ctx =>
            {
                Console.WriteLine(@"Exception:{0}", ctx.Exception.Message);
                return Task.CompletedTask;
            }
        };
    });

var app = builder.Build();


if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
    app.UseMiddleware<SecurityMiddleware>(); // şuan sadece otomatik kayıt için kullanılıyor

}
app.UseSwagger();


app.UseSwaggerUI(c =>
{
    // Public API grubu
    c.SwaggerEndpoint("/swagger/is-Public/swagger.json", "Public API");
    // Private API grubu
    c.SwaggerEndpoint("/swagger/is-Private/swagger.json", "Private API");

    c.RoutePrefix = string.Empty;
    c.DocExpansion(DocExpansion.List);



});

ServiceTool.ServiceProvider = app.Services;

app.UseHealthChecks("/Healthly", new HealthCheckOptions
{
    ResponseWriter = async (context, report) =>
    {
        context.Response.ContentType = "application/json";
        await context.Response.WriteAsync(JsonSerializer.Serialize(new SuccessResult("��lem Ba�ar�l�")));
    }
});

app.UseMiddleware<ExceptionHandlingMiddleware>();

app.UseCors();

app.UseRouting();

app.UseAuthentication();

app.UseAuthorization();

app.MapControllers();

app.UseStaticFiles();


app.Run();
