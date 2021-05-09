using cozaStore.DataAccessLayer;
using cozaStore.Models;

namespace cozaStore.BusinessLogicLayer
{
    public class PromotionServices : BaseServices<Promotion>, IPromotion
    {

        public PromotionServices(IUnitOfWork unitOfWork, IGenericReposistory<Promotion> genericReposistory) : base(unitOfWork, genericReposistory) { }

    }
}
