using OTUS_L47_HW.Model;
using LinqToDB;
using LinqToDB.Data;

namespace OTUS_L47_HW.DataAccess
{
    public class AppDataContext : DataConnection
    {
        public AppDataContext(DataOptions options)
        : base(options) { }

        public ITable<Customer> Customers => this.GetTable<Customer>();
        public ITable<Product> Products => this.GetTable<Product>();
        public ITable<Order> Orders => this.GetTable<Order>();
    }
}
