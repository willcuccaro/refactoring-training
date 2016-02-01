using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Refactoring
{
    class PrintingCommands
    {
        public static void PrintWelcomeMessage()
        {
            // Write welcome message test
            Console.WriteLine("Welcome to TUSC");
            Console.WriteLine("---------------");
        }

        public static void PrintUserNameInput()
        {
            // Prompt for user input
            Console.WriteLine();
            Console.WriteLine("Enter Username:");

        }

        public static void PrintInvalidUser()
        {
            // Invalid User
            Console.Clear();
            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine();
            Console.WriteLine("You entered an invalid user.");
            Console.ResetColor();
        }

        public static void PrintInvalidPassword()
        {
            // Invalid Password
            Console.Clear();
            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine();
            Console.WriteLine("You entered an invalid password.");
            Console.ResetColor();
        }

        public static void PrintQuanityIsLessThanZero()
        {
            // Quantity is less than zero
            Console.Clear();
            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.WriteLine();
            Console.WriteLine("Purchase cancelled");
            Console.ResetColor();
        }

        public static void PrintExitMessage()
        {
            // Prevent console from closing
            Console.WriteLine();
            Console.WriteLine("Press Enter key to exit");
            Console.ReadLine();
        }

        public static void PrintWelcomeMessage(string inputedUsername)
        {
            // Show welcome message
            Console.Clear();
            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine();
            Console.WriteLine("Login successful! Welcome " + inputedUsername + "!");
            Console.ResetColor();
        }
    }
}
