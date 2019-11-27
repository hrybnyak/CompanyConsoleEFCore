using System;
using BLL;
using BLL.DTO;

namespace EPAM.RD5_Task2
{
    class Program
    {
        static void Main(string[] args)
        {
            //try
            //{
                int key = 1;
                TasksService service = new TasksService();
                do
                {
                    Console.WriteLine("--------------Menu----------------");
                    Console.WriteLine("Press 0 to exit");
                    Console.WriteLine("Press 1 to get all products by category name");
                    Console.WriteLine("Press 2 to get all products by provider name");
                    Console.WriteLine("Press 3 to get all providers by category name");
                    if (Int32.TryParse(Console.ReadLine(), out key))
                    {
                        switch (key)
                        {
                            case 1:
                                {
                                    Console.WriteLine("Please enter category name:");
                                    CategoryDTO category = new CategoryDTO
                                    {
                                        Name = Console.ReadLine()
                                    };
                                    var products = service.GetProductsByCategory(category);
                                    if (products != null)
                                    {
                                        foreach (ProductDTO product in products)
                                        {
                                            Console.WriteLine($"{product.Name}       {product.Price}       {product.CategoryName}       {product.ProviderName}");
                                        }
                                    }
                                    else
                                    {
                                        Console.WriteLine("There is no products under this category");
                                    }
                                    break;
                                }
                            case 2:
                                {
                                    Console.WriteLine("Please enter category name:");
                                    ProviderDTO provider = new ProviderDTO
                                    {
                                        Name = Console.ReadLine()
                                    };
                                    var products = service.GetProductsByProvider(provider);
                                    if (products != null)
                                    {
                                        foreach (ProductDTO product in products)
                                        {
                                            Console.WriteLine($"{product.Name}       {product.Price}       {product.CategoryName}       {product.ProviderName}");
                                        }
                                    }
                                    else
                                    {
                                        Console.WriteLine("There is no products from this provider");
                                    }
                                    break;
                                }
                            case 3:
                                {
                                    Console.WriteLine("Please enter category name:");
                                    CategoryDTO category = new CategoryDTO
                                    {
                                        Name = Console.ReadLine()
                                    };
                                    var providers = service.GetProvidersByCategory(category);
                                    if (providers != null)
                                    {
                                        foreach (ProviderDTO provider in providers)
                                        {
                                            Console.WriteLine($"{provider.Name}       {provider.PhoneNumber}");
                                        }
                                    }
                                    else
                                    {
                                        Console.WriteLine("There is no providers of products of this category");
                                    }
                                    break;
                                }
                            default:
                                {
                                    Console.WriteLine("Please, try again!");
                                    break;
                                }
                        }
                    }
                    else
                    {
                        Console.WriteLine("Try again!");
                    }

                } while (key != 0);
            //}
            //catch(Exception ex)
            //{
            //    Console.WriteLine(ex.Message);
            //}
        }
    }
}
