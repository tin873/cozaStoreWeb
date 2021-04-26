using System;

namespace cozaStore.DataAccessLayer
{
    public interface IDbFactory : IDisposable
    {
        cozaStoreDbContext Init();
    }
}
