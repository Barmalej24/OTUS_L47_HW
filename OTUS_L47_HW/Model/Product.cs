using LinqToDB.Mapping;

namespace OTUS_L47_HW.Model
{
    [Table("Products")]
    public class Product
    {
        [PrimaryKey, Identity]
        public int Id { get; set; }
        [Column]
        public string Name { get; set; }
        [Column]
        public string Description { get; set; }
        [Column]
        public int StockQuantity { get; set; }
        [Column]
        public int Price { get; set; }

    }
}
