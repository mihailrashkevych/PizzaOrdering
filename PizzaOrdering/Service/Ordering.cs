using ConsoleTables;
using OrderingData.Models;
using PizzaOrdering.Service;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PizzaOrdering.Service
{
    public class Ordering
    {
        readonly OrderingData.Repository.Order orderOperator;
        private Order order;
        private readonly OrderItemDetail orderItemDetail;
        private readonly OrderingData.Repository.OrderDetail orderDetailOperator;
        private readonly OrderingData.Repository.OrderItemDetails orderItemDetailOperator;
        private readonly User user;
        public Ordering(User user)
        {
            orderOperator = new OrderingData.Repository.Order();
            orderItemDetail = new OrderItemDetail();
            orderDetailOperator = new OrderingData.Repository.OrderDetail();
            orderItemDetailOperator = new OrderingData.Repository.OrderItemDetails();
            this.user = user;
        }

        public async Task CreateOrder()
        {
            order = await orderOperator.GetNewOrderAsync();
            order.UEmail = user.Email;
            order.Total = 0;
            order.DeliveryCharge = 0;
        }

        public async Task<OrderDetail> GetPizzaOrderDetail()
        {
            Console.ForegroundColor = ConsoleColor.DarkCyan;
            Console.WriteLine($"Hey, {user.Name}! Welcome to the XYZ Pizza");
            Console.ForegroundColor = ConsoleColor.White;
            
            Console.WriteLine("The following are the pizza that are available for ordering");

            OrderingData.Repository.Pizza pizza = new();

            List<Pizza> pizzas = (List<Pizza>)await pizza.GetAll();

            var table = new ConsoleTable("Number", "Name", "Price", "Type");
            table.Options.EnableCount = false;
            var i = 1;
            foreach (var item in pizzas)
            {
                table.AddRow(i, item.Name, "$" + item.Price, item.PType);
                i++;
            }
            table.Write();
            Console.WriteLine();

            do
            {
                Console.WriteLine("Enter the Pizza of your choice");
                if (!int.TryParse(Console.ReadLine(), out int choise))
                {

                    _ = new ErrorMessage("--Message: Please enter pizza number. Try again!");
                }
                else if (choise > pizzas.Count)
                {
                    _ = new ErrorMessage("--Message: Wrong pizza number. Try again!");
                }
                else
                {
                    var orderedPizza = await pizza.GetByName(pizzas[choise - 1].Name);
                    Console.WriteLine($"You have selected {orderedPizza.Name} for ${orderedPizza.Price}");
                    order.Total += orderedPizza.Price;
                    Console.WriteLine($"Total: {order.Total}");
                    return await orderDetailOperator.Add(new OrderDetail() { PizzaId = orderedPizza.Id, OrderId = order.Id });
                }
            } while (true);
        }

        public async Task GetToppingOrderItemDetail(OrderDetail orderDetail)
        {
            Console.WriteLine("The folowing are the toppings");
            OrderingData.Repository.Topping topping = new();

            List<Topping> toppings = (List<Topping>)await topping.GetAll();

            var table = new ConsoleTable("Number", "Name", "Price");
            var i = 1;
            foreach (var item in toppings)
            {
                table.AddRow(i, item.Name, "$" + item.Price);
                i++;
            }
            table.Write();

            Console.WriteLine("Select the topping");
            if (!int.TryParse(Console.ReadLine(), out int choise))
            {
                _ = new ErrorMessage("--Message: Please enter topping number. Try again!");
            }
            else if (choise > toppings.Count)
            {
                _ = new ErrorMessage("--Message: Wrong topping number. Try again!");
            }
            else
            {
                var orderedTopping = await topping.GetByName(toppings[choise - 1].Name);

                orderItemDetail.ToppingId = orderedTopping.Id;
                orderItemDetail.OrderDetailsId = orderDetail.Id;
                if (!await orderItemDetailOperator.IsExist(orderItemDetail))
                {
                    await orderItemDetailOperator.Add(orderItemDetail);
                    order.Total += orderedTopping.Price;
                    Console.WriteLine($"You have selected {orderedTopping.Name} for ${orderedTopping.Price} so total ${order.Total}");
                }
                else
                {
                    Console.ForegroundColor = ConsoleColor.Yellow;
                    Console.WriteLine("Already added!");
                    Console.ForegroundColor = ConsoleColor.White;
                }
                Console.WriteLine();
            }
        }

        public async Task PrintDetailedOrder()
        {
            Console.Clear();
            Console.WriteLine("Your order summary:");
            List<OrderDetail> orderDetails = (List<OrderDetail>)await new OrderingData.Repository.OrderDetail().GetByOrder(order.Id);

            int i = 1;
            foreach (var item in orderDetails)
            {

                Console.WriteLine($"Pizza {i}");
                Console.WriteLine(i + "   " + item.Pizza.Name + "   $" + item.Pizza.Price + "   " + item.Pizza.PType);
                var table = new ConsoleTable("#", "Name", "Price");
                table.Options.EnableCount = false;

                Console.WriteLine("Toppings: ");
                int k = 1;
                if (!item.OrderItemDetails.Any())
                {
                    Console.Write("Nothing");
                }
                else
                {
                    foreach (var item1 in item.OrderItemDetails)
                    {
                        table.AddRow(k, item1.Topping.Name, "$" + item1.Topping.Price);
                        k++;
                    }
                    table.Write();
                }

                Console.WriteLine();
                i++;
            }

            if (order.Total < 25)
            {
                order.Total += 5;
                order.DeliveryCharge = 5;
            }
            Console.WriteLine($"Total price: ${order.Total}");
            Console.WriteLine($"Delivery cost: ${order.DeliveryCharge}");
            Console.WriteLine("Note- delivery cost of $5 will be added for order less than $25");

            Console.WriteLine("Please confirm your order (y/n)?");
            if (Console.ReadLine().ToUpper() == "Y")
            {
                order.Status = "Confirmed";
                Console.WriteLine("The order will be delivered to address:");
                Console.WriteLine(user.Address);
            }
            else
                order.Status = "Not confirmed";
        }
    }
}
