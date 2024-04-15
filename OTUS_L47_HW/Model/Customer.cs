using LinqToDB.Mapping;

namespace OTUS_L47_HW.Model
{
    [Table("Customers")]
    public class Customer
    {
        [PrimaryKey, Identity]
        public int Id { get; set; }
        [Column]
        public string FirstName { get; set; }
        [Column]
        public string LastName { get; set; }
        [Column]
        public int Age { get; set; }
        [Association(ThisKey = nameof(Customer.Id), OtherKey = nameof(Order.CustomerID))]
        public IEnumerable<Order> Orders { get; set; } = new List<Order>();        

    }
}
