using LinqToDB;
using OTUS_L47_HW.DataAccess;
using OTUS_L47_HW.Model;

namespace OTUS_L47_HW.Data
{
    public class CustomerData : IData<Customer>
    {
        private readonly AppDataContext _db;
        public CustomerData(AppDataContext db)
        {
            _db = db;
        }
        public Task Add(Customer obj)
        {
            return _db.InsertWithIdentityAsync(obj);
        }

        public Task Delete(int id)
        {
            return _db.Customers.DeleteAsync(a => a.Id == id);
        }

        public Task<Customer?> Get(int id)
        {
            var q = _db.Customers.LoadWith(u => u.Orders).ThenLoad(cd => cd.Products).FirstOrDefaultAsync(a => a.Id == id);
            return q;
        }

        public Task<List<Customer>> GetAll()
        {
            return _db.Customers.ToListAsync();
        }

        public Task Update(Customer obj)
        {
            return _db.UpdateAsync(obj);
        }
    }
}
