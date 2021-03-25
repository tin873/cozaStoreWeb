using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace cozaStore.DataAccessLayer.Infrastructure
{
    public interface IDbFactory : IDisposable
    {
        cozaStoreDbContext Init();
    }
}
