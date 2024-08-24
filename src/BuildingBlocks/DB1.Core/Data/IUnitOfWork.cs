namespace DB1.Core.Data
{
    public interface IUnitOfWork
    {
        Task<bool> Commit();
    }
}
