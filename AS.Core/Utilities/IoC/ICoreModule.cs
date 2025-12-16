using Microsoft.Extensions.DependencyInjection;

namespace AS.Core.Utilities.IoC
{
    public interface ICoreModule
    {
        void Load(IServiceCollection collection);
    }
}
