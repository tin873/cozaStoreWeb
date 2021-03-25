using cozaStore.DataAccessLayer;
using cozaStore.Models;

namespace cozaStore.BusinessLogicLayer.Services
{
    public class ContactServices : BaseServices<Comment>, ICommentServices
    {
        public ContactServices(IUnitOfWork unitOfWork, IGenericReposistory<Comment> genericReposistory) : base(unitOfWork, genericReposistory) { }
    }
}
