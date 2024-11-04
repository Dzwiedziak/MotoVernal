using DB.Entities;

namespace BusinessLogic.Repositories.Interfaces
{
    public interface IOfferRepository
    {
        List<Offer> GetAll();
        Offer? GetOne(int id);
        void Add(Offer user);
        void Update(Offer user);
    }
}
