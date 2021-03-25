using cozaStore.DataAccessLayer;
using cozaStore.Models;

namespace cozaStore.BusinessLogicLayer.Services
{
    public class UserServices : BaseServices<Comment>, ICommentServices
    {
        public UserServices(IUnitOfWork unitOfWork, IGenericReposistory<Comment> genericReposistory) : base(unitOfWork, genericReposistory) { }
    }
}
