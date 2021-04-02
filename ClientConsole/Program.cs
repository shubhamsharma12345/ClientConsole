using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;

namespace ClientConsole
{
    class Program
    {
        static void Main(string[] args)
        {
            Products product = new Products();
            GetAllProduct().Wait();
            Console.WriteLine("Enter the Id");
            int id = Convert.ToInt32(Console.ReadLine());
            GetProductById(id).Wait();
            Console.WriteLine("Enter the Product Id");
            product.ProductId = Convert.ToInt32(Console.ReadLine());
            Console.WriteLine("Enter the Product Name");
            product.Name = Console.ReadLine();
            Console.WriteLine("Enter the QuntyInStock");
            product.QntyInStock = Convert.ToInt32(Console.ReadLine());
            Console.WriteLine("Enter the Description");
            product.Description = Console.ReadLine();
            Console.WriteLine("Enter the Supplier");
            product.Suppiler = Console.ReadLine();
            Insert(product).Wait();
            GetAllProduct().Wait();
            Put().Wait();
            GetAllProduct().Wait();
            Delete().Wait();
            GetAllProduct().Wait();
            Console.ReadKey();
        }
        static async Task GetAllProduct()
        {
            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri("https://localhost:44305/");
                client.DefaultRequestHeaders.Accept.Clear();
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                HttpResponseMessage response = await client.GetAsync("api/Product/");
                if (response.IsSuccessStatusCode)
                {
                    var jasonString = response.Content.ReadAsStringAsync();
                    jasonString.Wait();
                    var ProductList = JsonConvert.DeserializeObject<List<Products>>(jasonString.Result);
                    foreach (var temp in ProductList)
                    {
                        Console.WriteLine("Product Id:" + temp.ProductId + " ProductName:" + temp.Name);
                    }
                }
                else
                {
                    Console.WriteLine(response.StatusCode);
                }

            }
        }
        static async Task GetProductById(int id)

        {
            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri("https://localhost:44305/");

                client.DefaultRequestHeaders.Accept.Clear();

                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

                HttpResponseMessage response = await client.GetAsync("api/Product/" + id);

                if (response.IsSuccessStatusCode)
                {
                    Products product = await response.Content.ReadAsAsync<Products>();

                    Console.WriteLine("Id:{0}\tName:{1}", product.ProductId, product.Name);

                    //  Console.WriteLine("No of Employee in Department: {0}", department.Employees.Count);

                }

                else

                {

                    Console.WriteLine(response.StatusCode);

                }
            }
        }
        static async Task Insert(Products product)
        {
            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri("https://localhost:44305/");
                HttpResponseMessage response = await client.PostAsJsonAsync("api/Product", product);
                if (response.IsSuccessStatusCode)
                {
                    Console.WriteLine(response.StatusCode);
                }
            }
        }
        static async Task Put()

        {

            using (var client = new HttpClient())

            {

                //Send HTTP requests from here. 
                client.BaseAddress = new Uri("https://localhost:44305/");

                //PUT Method  
                var product = new Products() { ProductId = 9, Description = "Description Updated" };
                int id = 1;
                HttpResponseMessage response = await client.PutAsJsonAsync("api/Product/" + id, product);

                if (response.IsSuccessStatusCode)



                {

                    Console.WriteLine(response.StatusCode);

                }

                else

                {

                    Console.WriteLine(response.StatusCode);

                }

            }

        }



        static async Task Delete()

        {

            using (var client = new HttpClient())

            {

                //Send HTTP requests from here. 

                client.BaseAddress = new Uri("https://localhost:44305/");





                int id = 1;

                HttpResponseMessage response = await client.DeleteAsync("api/Product/" + id);

                if (response.IsSuccessStatusCode)

                {

                    Console.WriteLine(response.StatusCode);

                }

                else

                    Console.WriteLine(response.StatusCode);

            }

        }



    }
}
