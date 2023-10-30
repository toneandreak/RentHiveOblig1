using RentHiveOblig.Models;

namespace RentHiveOblig.DAL.Repositories
{
    public interface IEiendomRepository
    {
        Eiendom GetById(int id);
        List<Eiendom> GetAll();
        void Add(Eiendom eiendom);
        void Update(Eiendom eiendom);
        void Delete(int id);
    }
}