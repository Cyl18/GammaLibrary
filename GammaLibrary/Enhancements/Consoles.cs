using System;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using System.Security;
using System.Text;

namespace GammaLibrary.Enhancements
{
    public static class Consoles
    {
        public static string ReadLine(string str)
        {
            Console.WriteLine(str);
            return Console.ReadLine();
        }
        
        public static string ReadPassword()
        {
            var password = string.Empty;
            while (true)
            {
                var key = Console.ReadKey(true);
                switch (key.Key)
                {
                    case ConsoleKey.Escape:
                        return null;
                    case ConsoleKey.Enter:
                        return password;
                    case ConsoleKey.Backspace:
                        password = password.Substring(0, (password.Length - 1));
                        Console.Write("\b \b");
                        break;
                    default:
                        password += key.KeyChar;
                        Console.Write("*");
                        break;
                }
            }
        }
    }
}
