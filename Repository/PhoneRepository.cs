using System.Linq;
using desafio_.Net.Contexts;
using desafio_.Net.Models;
using desafio_.Net.Repository.Interface;

namespace desafio_.Net.Repository
{
    public class PhoneRepository : IPhoneRepository
    {

        private readonly DesafioDbContext _contextDb;
        public PhoneRepository(DesafioDbContext ctx)
        {
            _contextDb = ctx;
        }

        public Phone Find(long id)
        {
            return _contextDb.Phones.Find(id);
        }

        public void Remove(long id)
        {
            var entity = _contextDb.Phones.First(u => u.number == id);
            _contextDb.Phones.Remove(entity);
            _contextDb.SaveChanges();        
        }
    }
}