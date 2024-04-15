using LinqToDB;
using OTUS_L47_HW.Model;
using OTUS_L47_HW.DataAccess;

namespace OTUS_L47_HW.Data
{
    public class OrderData : IData<Order>
    {
        private readonly AppDataContext _db;
        public OrderData(AppDataContext db)
        {
            _db = db;
        }
        public Task Add(Order obj)
        {
            return _db.InsertWithIdentityAsync(obj);
        }

        public Task Delete(int id)
        {
            return _db.Orders.DeleteAsync(a => a.Id == id);
        }

        public Task<Order?> Get(int id)
        {
            return _db.Orders.LoadWith(u => u.Products).FirstOrDefaultAsync(a => a.Id == id); 
        }

        public Task<List<Order>> GetAll()
        {
            return _db.Orders.ToListAsync();
        }

        public Task Update(Order obj)
        {
            return _db.UpdateAsync(obj);
        }
    }
}
