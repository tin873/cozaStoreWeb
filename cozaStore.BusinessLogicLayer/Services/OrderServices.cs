using cozaStore.DataAccessLayer;
using cozaStore.Models;

namespace cozaStore.BusinessLogicLayer.Services
{
    public class OrderServices : BaseServices<Comment>, ICommentServices
    {
        public OrderServices(IUnitOfWork unitOfWork, IGenericReposistory<Comment> genericReposistory) : base(unitOfWork, genericReposistory) { }
    }
}
