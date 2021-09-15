using OrderingData.Models;
using PizzaOrdering.Service;
using System;
using System.Threading.Tasks;

namespace PizzaOrderingSystem
{
    static class Program
    {
        static async Task Main(string[] args)
        {
            Autorize autorize = new();
            Ordering ordering;
            User user = await autorize.PrintLoginMenu();
            ordering = new Ordering(user);

            await ordering.CreateOrder();

            do
            {
                var item = await ordering.GetPizzaOrderDetail();
                Console.WriteLine("Do u want extra toppings?(y/n)");
                if (Console.ReadLine().ToUpper() == "Y")
                {
                    await ordering.GetToppingOrderItemDetail(item);

                    do
                    {
                        Console.WriteLine("Do u wnat one more toppings?y/n");
                        if (Console.ReadLine().ToUpper() != "Y")
                        {
                            break;
                        }
                        else await ordering.GetToppingOrderItemDetail(item);
                    } while (true);
                }

                Console.WriteLine();

                Console.WriteLine("Do you want to select another pizza for this order?y/n");
                if (Console.ReadLine().ToUpper() != "Y")
                {
                    break;
                }
            } while (true);
            await ordering.PrintDetailedOrder();
            Console.ReadKey();
        }
    }
}
