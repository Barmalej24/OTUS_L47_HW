using LinqToDB.Mapping;
using System.ComponentModel.DataAnnotations.Schema;
using ColumnAttribute = LinqToDB.Mapping.ColumnAttribute;
using TableAttribute = LinqToDB.Mapping.TableAttribute;

namespace OTUS_L47_HW.Model
{
    [Table("Orders")]
    public class Order
    {
        [PrimaryKey, Identity]
        public int Id { get; set; }
        [Column]
        [ForeignKey("FK_Customer")]
        public int CustomerID { get; set; }
        [Column]
        [ForeignKey("FK_Product")]
        public int ProductID { get; set; }
        [Column]
        public int Quantity { get; set; }

        [Association(ThisKey = nameof(Order.ProductID), OtherKey = nameof(Product.Id))]
        public IEnumerable<Product> Products { get; set; } = new List<Product>();

    }
}
