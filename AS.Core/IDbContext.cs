namespace AS.Core
{
    public interface IDbContext : IDisposable
    {
        int SaveChanges();
    }
}
