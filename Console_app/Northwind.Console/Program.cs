using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Net.Http.Headers;
using Newtonsoft.Json;

namespace Northwind.ConsoleApp
{
    public class Product
    {
        public string ProductID { get; set; }
        public string ProductName { get; set; }
        public string QuantityPerUnit { get; set; }
        public decimal? UnitPrice { get; set; }
        public short? UnitsInStock { get; set; }
        public short? UnitsOnOrder { get; set; }
        public short? ReorderLevel { get; set; }
    }

    public class Category
    {
        public int CategoryID { get; set; }
        public string CategoryName { get; set; }
        public string Description { get; set; }
    }

    class Program
    {
        static HttpClient client = new HttpClient();

        static void Main(string[] args)
        {
            client.BaseAddress = new Uri("https://localhost:44346/");
            client.DefaultRequestHeaders.Accept.Clear();
            client.DefaultRequestHeaders.Accept.Add(
                new MediaTypeWithQualityHeaderValue("application/json"));
            Console.WriteLine("Hello!");
            while (Console.ReadKey().KeyChar != '0') 
            {
                Console.WriteLine("Categories:");
                GetAllCategories("api/categories");
                Console.WriteLine("=====================");
                GetAllProducts("api/products");
                Console.WriteLine("=====================");
            }
            
        }

        static void GetAllCategories(string path)
        {
            IEnumerable<Category> categories = new List<Category>();
            HttpResponseMessage response = client.GetAsync(path).Result;
            if (response.IsSuccessStatusCode)
            {
                var rawResponse = response.Content.ReadAsStringAsync().Result;
                categories = JsonConvert.DeserializeObject<IEnumerable<Category>>(rawResponse);
            }
            foreach (var category in categories) 
            {
                ShowCategory(category);
            }
        }

        static void GetAllProducts(string path)
        {
            IEnumerable<Product> products = new List<Product>();
            HttpResponseMessage response = client.GetAsync(path).Result;
            if (response.IsSuccessStatusCode)
            {
                var rawResponse = response.Content.ReadAsStringAsync().Result;
                products = JsonConvert.DeserializeObject<IEnumerable<Product>>(rawResponse);
            }
            foreach (var product in products)
            {
                ShowProduct(product);
            }
        }

        static void ShowCategory(Category category)
        {
            Console.WriteLine($"Name: {category.CategoryName}\tDescription: {category.Description}");
        }

        static void ShowProduct(Product product)
        {
            Console.WriteLine($"Name: {product.ProductName}\tPrice: " +
                $"{product.UnitPrice}");
        }
    }
}
