using cozaStore.DataAccessLayer;
using cozaStore.Models;

namespace cozaStore.BusinessLogicLayer.Services
{
    public class OrderDetailServices : BaseServices<Comment>, ICommentServices
    {
        public OrderDetailServices(IUnitOfWork unitOfWork, IGenericReposistory<Comment> genericReposistory) : base(unitOfWork, genericReposistory) { }
    }
}
