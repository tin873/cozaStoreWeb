using cozaStore.DataAccessLayer;
using cozaStore.Models;

namespace cozaStore.BusinessLogicLayer
{
    public class StatusServices : BaseServices<Status>, IStatusServices
    {
        public StatusServices(IUnitOfWork unitOfWork, IGenericReposistory<Status> genericReposistory) : base(unitOfWork, genericReposistory) { }
    }
}

    
