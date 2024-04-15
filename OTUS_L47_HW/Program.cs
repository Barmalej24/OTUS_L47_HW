using LinqToDB;
using OTUS_L47_HW.Data;
using OTUS_L47_HW.DataAccess;
using OTUS_L47_HW.Model;
using System.Collections.Generic;

namespace OTUS_L47_HW
{
    internal class Program
    {
        static void Main(string[] args)

        {
            var sqlString = @"Data Source=(localdb)\MSSQLLocalDB;Initial Catalog=Shop;Integrated Security=True;Connect Timeout=30;Encrypt=False;Trust Server Certificate=False;Application Intent=ReadWrite;Multi Subnet Failover=False";
            using var db = new AppDataContext(new DataOptions().UseSqlServer(sqlString));

            Console.WriteLine("Start");

            var productData = new ProductData(db);

            PrintAllProduct(productData);
            AddProduct(productData);
            PrintAllProduct(productData);

            var customerData = new CustomerData(db);

            PrintAllCustomer(customerData);

            Console.WriteLine("Выбери пользователя (введи ID):");
            if (int.TryParse(Console.ReadLine(), out int id))
            {
                PrintOrdersCustomer(customerData, id);
            }
            else
            {
                Console.WriteLine("Введено не число");
            }

            Console.WriteLine("Введите возрост покупателя:");
            if (!int.TryParse(Console.ReadLine(), out int customerAge))
            {
                Console.WriteLine("Введено не число");
                return;
            }

            Console.WriteLine("Введите Id товара:");
            if (!int.TryParse(Console.ReadLine(), out int productId))
            {
                Console.WriteLine("Введено не число");
                return;
            }

            PrintSelectItems(db, customerAge, productId);

        }

        private static void PrintSelectItems(AppDataContext db, int age, int pId)
        {
            var selectItems = db.Customers
                            .SelectMany(o => db.Orders.Where(q => q.CustomerID == o.Id).DefaultIfEmpty(), (o1, q) => new { o1, q })
                            .SelectMany(z => db.Products.Where(x => x.Id == z.q.ProductID).DefaultIfEmpty(), (z1, x) => new { z1, x })
                            .Select(t => new { t.z1.o1.FirstName, t.z1.o1.LastName, t.z1.q.Quantity, t.x.Name, t.x.Price, t.x.Id, t.z1.o1.Age })
                            .Where(e => e.Age > age && e.Id == pId)
                            .ToList();
            if (selectItems.Count > 0)
            {
                Console.WriteLine("______________________________Selected Items______________________________");
                Console.WriteLine($"|{" FirstName ",15}|{" FirstName ",15}|{" Age ",5}|{" Name ",15}|{" Quantity ",5}|{" Price ",5}|");
                Console.WriteLine("--------------------------------------------------------------------------");
                foreach (var item in selectItems)
                {
                    Console.WriteLine($"|{item.FirstName,-15}|{item.LastName,-15}|{item.Age,-5}|{item.Name,-15}|{item.Quantity,10}|{item.Price,7}|");
                }
                Console.WriteLine("__________________________________________________________________________\n");
            }
            else
            {
                Console.WriteLine("Ничего не найдено");
            }
        }

        private static void PrintAllProduct(ProductData productData)
        {
            var products = productData.GetAll();
            Console.WriteLine("______________________________________________________________________________Products____________________________________________________________________________________");
            Console.WriteLine($"|{" Id ",5}|{" Name ",15}|{" Description ",130}|{" Price ",5}|{" Count ",7}|");
            Console.WriteLine("--------------------------------------------------------------------------------------------------------------------------------------------------------------------------");
            foreach (var item in products.Result)
            {
                Console.WriteLine($"|{item.Id,-5}|{item.Name,-15}|{item.Description,-130}|{item.Price,7}|{item.StockQuantity,7}|");
            }
            Console.WriteLine("--------------------------------------------------------------------------------------------------------------------------------------------------------------------------\n");
        }
        private static void AddProduct(ProductData productData)
        {
            Console.WriteLine("Введите название товара:");
            var name = Console.ReadLine();
            if ((name == string.Empty) && (name == ""))
            {
                Console.WriteLine("Не задано название");
                return;
            }

            Console.WriteLine("Введите описание товара:");
            var description = Console.ReadLine();
            if ((description == string.Empty) && (description == ""))
            {
                Console.WriteLine("Не задано описание");
                return;
            }

            Console.WriteLine("Введите цену товара:");
            if (!int.TryParse(Console.ReadLine(), out int price))
            {
                Console.WriteLine("Введено не число");
                return;
            }

            Console.WriteLine("Введите количество товара на складе:");
            if (!int.TryParse(Console.ReadLine(), out int count))
            {
                Console.WriteLine("Введено не число");
                return;
            }

            var product = new Product()
            {
                Name = name,
                Description = description,
                Price = price,
                StockQuantity = count,
            };

            productData.Add(product);
        }
        private static void PrintOrdersCustomer(CustomerData customerData, int id)
        {
            var customer = customerData.Get(id).Result;

            if (customer != null)
            {
                Console.WriteLine("_______________Customer Orders_______________");
                Console.WriteLine($"|{" FirstName ",18}|{" LastName ",18}|{" Age ",5}|");
                Console.WriteLine("---------------------------------------------");
                Console.WriteLine($"|{customer.FirstName,-18}|{customer.LastName,-18}|{customer.Age,5}|");
                Console.WriteLine("---------------------------------------------");
                Console.WriteLine($"|{" Id ",5}|{" PId ",5}|{" ProductsName ",20}|{" Quantity ",10}|");
                Console.WriteLine("---------------------------------------------");
                foreach (var data in customer.Orders)
                {
                    var line = $"|{data.Id,-5}|{data.ProductID,-5}|{data.Products.First().Name,20}|{data.Quantity,10}|";
                    Console.WriteLine(line);
                }
                Console.WriteLine("---------------------------------------------\n");
            }
            else
            {
                Console.WriteLine($"Пользователь не найден");
            }
        }
        private static void PrintAllCustomer(CustomerData customerData)
        {
            var customers = customerData.GetAll();
            Console.WriteLine("___________________Customers_________________");
            Console.WriteLine($"|{" Id ",5}|{" FirstName ",15}|{" LastName ",15}|{" Age ",5}|");
            Console.WriteLine("---------------------------------------------");
            foreach (var item in customers.Result)
            {
                Console.WriteLine($"|{item.Id,-5}|{item.FirstName,-15}|{item.LastName,-15}|{item.Age,5}|");
            }
            Console.WriteLine("---------------------------------------------\n");
        }

    }
}
