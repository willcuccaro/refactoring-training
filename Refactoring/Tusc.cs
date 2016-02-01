using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Refactoring
{
    public class Tusc
    {
        public static void Start(List<User> users, List<Product> products)
        {
            PrintingCommands.PrintWelcomeMessage();
            
            // Login
            Login:
            bool loggedIn = false;

            string inputedUsername = GetInputUserName();

            // Validate Username
            bool validatedUser = false; // Is valid user?
            if (!string.IsNullOrEmpty(inputedUsername))
            {
                for (int i = 0; i < 5; i++)
                {
                    User user = users[i];
                    // Check that name matches
                    if (user.UserName == inputedUsername)
                    {
                        validatedUser = true;
                    }
                }

                // if valid user
                if (validatedUser)
                {
                    // Prompt for user input
                    Console.WriteLine("Enter Password:");
                    string inputPassword = Console.ReadLine();

                    // Validate Password
                    bool validatedPassword = false; // Is valid password?
                    for (int i = 0; i < 5; i++)
                    {
                        User user = users[i];

                        // Check that name and password match
                        if (user.UserName == inputedUsername && user.Password == inputPassword)
                        {
                            validatedPassword = true;
                        }
                    }

                    // if valid password
                    if (validatedPassword == true)
                    {
                        loggedIn = true;

                        PrintingCommands.PrintWelcomeMessage(inputedUsername);
                        
                        // Show remaining balance
                        double outstandingBalance = 0;
                        for (int i = 0; i < 5; i++)
                        {
                            User user = users[i];

                            // Check that name and password match
                            if (user.UserName == inputedUsername && user.Password == inputPassword)
                            {
                                outstandingBalance = user.Balance;

                                // Show balance 
                                Console.WriteLine();
                                Console.WriteLine("Your balance is " + user.Balance.ToString("C"));
                            }
                        }

                        // Show product list
                        while (true)
                        {
                            GetInputItemToPurchase(products); 
                            string productSelectionResponse = Console.ReadLine();
                            int productSelctionResponseNumber = Convert.ToInt32(productSelectionResponse);
                            productSelctionResponseNumber = productSelctionResponseNumber - 1; /* Subtract 1 from number
                            num = num + 1 // Add 1 to number */

                            // Check if user entered number that equals product count
                            if (productSelctionResponseNumber == 7)
                            {
                                // Update balance
                                foreach (var user in users)
                                {
                                    // Check that name and password match
                                    if (user.UserName == inputedUsername && user.Password == inputPassword)
                                    {
                                        user.Balance = outstandingBalance;
                                    }
                                }

                                // Write out new balance
                                string json = JsonConvert.SerializeObject(users, Formatting.Indented);
                                File.WriteAllText(@"Data/Users.json", json);

                                // Write out new quantities
                                string json2 = JsonConvert.SerializeObject(products, Formatting.Indented);
                                File.WriteAllText(@"Data/Products.json", json2);


                                PrintingCommands.PrintExitMessage();
                                return;
                            }
                            else
                            {
                                Console.WriteLine();
                                Console.WriteLine("You want to buy: " + products[productSelctionResponseNumber].ProductName);
                                Console.WriteLine("Your balance is " + outstandingBalance.ToString("C"));

                                int requestedQuantity = GetInputRequestedQuantity(ref productSelectionResponse);

                                // Check if balance - quantity * price is less than 0
                                if (outstandingBalance - products[productSelctionResponseNumber].RegularPrice * requestedQuantity < 0)
                                {
                                    Console.Clear();
                                    Console.ForegroundColor = ConsoleColor.Red;
                                    Console.WriteLine();
                                    Console.WriteLine("You do not have enough money to buy that.");
                                    Console.ResetColor();
                                    continue;
                                }

                                // Check if quantity is less than quantity
                                if (products[productSelctionResponseNumber].RemainingQuantity <= requestedQuantity)
                                {
                                    Console.Clear();
                                    Console.ForegroundColor = ConsoleColor.Red;
                                    Console.WriteLine();
                                    Console.WriteLine("Sorry, " + products[productSelctionResponseNumber].ProductName + " is out of stock");
                                    Console.ResetColor();
                                    continue;
                                }

                                // Check if quantity is greater than zero
                                if (requestedQuantity > 0)
                                {
                                    // Balance = Balance - Price * Quantity
                                    outstandingBalance = outstandingBalance - products[productSelctionResponseNumber].RegularPrice * requestedQuantity;

                                    // Quanity = Quantity - Quantity
                                    products[productSelctionResponseNumber].RemainingQuantity = products[productSelctionResponseNumber].RemainingQuantity - requestedQuantity;

                                    Console.Clear();
                                    Console.ForegroundColor = ConsoleColor.Green;
                                    Console.WriteLine("You bought " + requestedQuantity + " " + products[productSelctionResponseNumber].ProductName);
                                    Console.WriteLine("Your new balance is " + outstandingBalance.ToString("C"));
                                    Console.ResetColor();
                                }
                                else
                                {
                                    PrintingCommands.PrintQuanityIsLessThanZero();
                                }
                            }
                        }
                    }
                    else
                    {
                        PrintingCommands.PrintInvalidPassword();

                        goto Login;
                    }
                }
                else
                {
                    PrintingCommands.PrintInvalidUser();

                    goto Login;
                }
            }

            // Prevent console from closing
            PrintingCommands.PrintExitMessage();
        }

        private static void GetInputItemToPurchase(List<Product> products)
        {
            // Prompt for user input
            Console.WriteLine();
            Console.WriteLine("What would you like to buy?");
            for (int i = 0; i < 7; i++)
            {
                Product product = products[i];
                Console.WriteLine(i + 1 + ": " + product.ProductName + " (" + product.RegularPrice.ToString("C") + ")");
            }
            Console.WriteLine(products.Count + 1 + ": Exit");

            // Prompt for user input
            Console.WriteLine("Enter a number:");

        }

       

        private static int GetInputRequestedQuantity(ref string productSelectionResponse)
        {
            // Prompt for user input
            Console.WriteLine("Enter amount to purchase:");
            productSelectionResponse = Console.ReadLine();
            int qty = Convert.ToInt32(productSelectionResponse);
            return qty;
        }

        private static string GetInputUserName()
        {
            PrintingCommands.PrintUserNameInput(); 
            string inputedUsername = Console.ReadLine();
            return inputedUsername;
        }

       

        
    }
}
