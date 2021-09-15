using OrderingData.Models;
using System;
using System.Threading.Tasks;

namespace PizzaOrdering.Service
{
    public class Autorize
    {
        readonly OrderingData.Repository.User user;
        public Autorize()
        {
            user = new OrderingData.Repository.User();
        }

        public async Task<User> Login()
        {
            Console.WriteLine();
            do
            {
                Console.WriteLine("Enter your username:");
                string email = Console.ReadLine();
                Console.WriteLine("Enter your password:");
                string password = Console.ReadLine();
                var entity = await user.GetUserAsync(email, password);
                if (entity != null)
                {
                    Console.Clear();
                    return entity;
                }
                else
                {
                    _ = new ErrorMessage("--Message: Wrong Login or Password");
                }
            } while (true);
        }

        public async Task<User> Register()
        {
            User newUser = new();
            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine("Welcome to registration form!");
            Console.ForegroundColor = ConsoleColor.White;
            do
            {
                Console.WriteLine();
                Console.WriteLine("Enter your Name (max 20 characters):");
                string name = Console.ReadLine();
                if (name.Length >= 20)
                {
                    _ = new ErrorMessage("--Message: Name must be less then 20 characters");
                    continue;
                }
                Console.WriteLine("Enter your email (max 50 characters):");
                string email = Console.ReadLine();
                if (email.Length >= 50 || email.Trim().Length == 0)
                {
                    _ = new ErrorMessage("--Message: Email must be less then 50 characters and not empty");
                    continue;
                }
                Console.WriteLine("Enter your password (max 20 characters):");
                string password = Console.ReadLine();
                if (password.Length >= 50 || password.Trim().Length == 0)
                {
                    _ = new ErrorMessage("--Message: Password must be less then 20 characters and not empty");
                    continue;
                }
                Console.WriteLine("Enter your phone (max 15 characters):");
                string phone = Console.ReadLine();
                if (phone.Length != 15)
                {
                    _ = new ErrorMessage("--Message: Phone must be 15 characters");
                    continue;
                }
                Console.WriteLine("Enter your address (max 50 characters):");
                string address = Console.ReadLine();
                if (address.Length >= 50 || password.Trim().Length == 0)
                {
                    _ = new ErrorMessage("--Message: Phone must be less then 50 characters and not empty");
                    continue;
                }
                newUser.Name = name;
                newUser.Email = email;
                newUser.Password = password;
                newUser.Phone = phone;
                newUser.Address = address;
                break;
            } while (true);

            return await user.AddUserAsync(newUser);
        }

        public async Task<User> PrintLoginMenu()
        {
            Console.WriteLine("Pizza ordering system");
            Console.WriteLine();
            Console.WriteLine("1 - Login");
            Console.WriteLine("2 - Register");
            Console.WriteLine();
            do
            {
                Console.WriteLine("Enter number of operation");
                _ = int.TryParse(Console.ReadLine(), out int input);
                
                if (input == 1)
                {
                    return await Login();
                }
                else if (input == 2)
                {
                    User newUser = await Register();
                    if (newUser != null)
                    {
                        Console.ForegroundColor = ConsoleColor.Green;
                        Console.WriteLine("Success!");
                        Console.ForegroundColor = ConsoleColor.White;
                        Console.WriteLine();
                    }
                    else
                    {
                        _ = new ErrorMessage("Fail!");
                        Console.WriteLine();
                    }
                }
                else if (input != 1 || input != 2)
                {
                    _ = new ErrorMessage("--Message: Wrong choise. Try again!");
                }
            } while (true);
        }
    }
}
