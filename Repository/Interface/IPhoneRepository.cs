using desafio_.Net.Models;

namespace desafio_.Net.Repository.Interface
{
    public interface IPhoneRepository
    {
         Phone Find(long id);

         void Remove(long id);
    }
}