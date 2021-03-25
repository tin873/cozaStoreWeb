using System.Threading.Tasks;

namespace cozaStore.DataAccessLayer.Infrastructure
{
    public interface IUnitOfWork
    {
        int Commit();
        Task<int> CommitAsync();
    }
}
