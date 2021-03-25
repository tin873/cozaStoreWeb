using cozaStore.DataAccessLayer;
using cozaStore.Models;

namespace cozaStore.BusinessLogicLayer.Services
{
    public class SupplierServices : BaseServices<Comment>, ICommentServices
    {
        public SupplierServices(IUnitOfWork unitOfWork, IGenericReposistory<Comment> genericReposistory) : base(unitOfWork, genericReposistory) { }
    }
}
