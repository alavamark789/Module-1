using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApp1
{
    using System;
    using System.Collections.Generic;

    public class DictionaryRepository<TKey, TValue> where TKey : IComparable<TKey>
    {
        private Dictionary<TKey, TValue> _items = new Dictionary<TKey, TValue>();

       
        public void Add(TKey key, TValue item)
        {
            if (_items.ContainsKey(key))
            {
                throw new ArgumentException($"An item with the key {key} already exists.");
            }
            _items[key] = item;
        }

        public TValue Get(TKey key)
        {
            if (!_items.ContainsKey(key))
            {
                throw new KeyNotFoundException($"No item found with key {key}.");
            }
            return _items[key];
        }

        public void Update(TKey key, TValue newItem)
        {
            if (!_items.ContainsKey(key))
            {
                throw new KeyNotFoundException($"No item found with key {key} to update.");
            }
            _items[key] = newItem;
        }

        public void Delete(TKey key)
        {
            if (!_items.ContainsKey(key))
            {
                throw new KeyNotFoundException($"No item found with key {key} to delete.");
            }
            _items.Remove(key);
        }
        public void DisplayAllItems()
        {
            if (_items.Count == 0)
            {
                Console.WriteLine("No items available.");
            }
            else
            {
                foreach (var item in _items)
                {
                    Console.WriteLine($"Key: {item.Key}, Value: {item.Value}");
                }
            }
        }
    }

    public class Product
    {
        public int ProductId { get; set; }
        public string ProductName { get; set; }

        public Product(int productId, string productName)
        {
            ProductId = productId;
            ProductName = productName;
        }

        public override string ToString() => $"ProductId: {ProductId}, ProductName: {ProductName}";
    }

    public class Program
    {
        public static void Main()
        {
            DictionaryRepository<int, Product> productRepo = new DictionaryRepository<int, Product>();

            while (true)
            {
                Console.Clear();
                Console.WriteLine("=== Product Management System ===");
                Console.WriteLine("1. Add Product");
                Console.WriteLine("2. Get Product");
                Console.WriteLine("3. Update Product");
                Console.WriteLine("4. Delete Product");
                Console.WriteLine("5. Display All Products");
                Console.WriteLine("6. Exit");
                Console.Write("Select an option: ");
                string choice = Console.ReadLine();

                try
                {
                    switch (choice)
                    {
                        case "1":
                            
                            Console.Write("Enter Product ID: ");
                            int addId = int.Parse(Console.ReadLine());
                            Console.Write("Enter Product Name: ");
                            string addName = Console.ReadLine();
                            productRepo.Add(addId, new Product(addId, addName));
                            Console.WriteLine("Product added successfully.");
                            break;

                        case "2":
                         
                            Console.Write("Enter Product ID to retrieve: ");
                            int getId = int.Parse(Console.ReadLine());
                            var product = productRepo.Get(getId);
                            Console.WriteLine($"Product found: {product}");
                            break;

                        case "3":
                           
                            Console.Write("Enter Product ID to update: ");
                            int updateId = int.Parse(Console.ReadLine());
                            Console.Write("Enter new Product Name: ");
                            string updateName = Console.ReadLine();
                            productRepo.Update(updateId, new Product(updateId, updateName));
                            Console.WriteLine("Product updated successfully.");
                            break;

                        case "4":
                           
                            Console.Write("Enter Product ID to delete: ");
                            int deleteId = int.Parse(Console.ReadLine());
                            productRepo.Delete(deleteId);
                            Console.WriteLine("Product deleted successfully.");
                            break;

                        case "5":
                     
                            Console.WriteLine("\nAll Products:");
                            productRepo.DisplayAllItems();
                            break;

                        case "6":
                            Console.WriteLine("Exiting...");
                            return;

                        default:
                            Console.WriteLine("Invalid option. Please select a valid option.");
                            break;
                    }
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"Error: {ex.Message}");
                }

                Console.WriteLine("\nPress any key to continue...");
                Console.ReadKey();
            }
        }
    }

}

