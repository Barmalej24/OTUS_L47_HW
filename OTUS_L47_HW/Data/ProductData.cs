using LinqToDB;
using OTUS_L47_HW.DataAccess;
using OTUS_L47_HW.Model;


namespace OTUS_L47_HW.Data
{
    public class ProductData : IData<Product>
    {
        private readonly AppDataContext _db;
        public ProductData(AppDataContext db)
        {
            _db = db;
        }
        public Task Add(Product obj)
        {
            return _db.InsertWithIdentityAsync(obj);
        }

        public Task Delete(int id)
        {
            return _db.Products.DeleteAsync(a => a.Id == id);
        }

        public Task<Product?> Get(int id)
        {
            return _db.Products.FirstOrDefaultAsync(a => a.Id == id);
        }

        public Task<List<Product>> GetAll()
        {
            return _db.Products.ToListAsync();
        }

        public Task Update(Product obj)
        {
            return _db.UpdateAsync(obj);
        }
    }
}
