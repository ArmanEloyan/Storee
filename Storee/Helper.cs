using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Store
{
    internal static class Helper
    {
        public static void ErrorMessage(string message)
        {
            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine(message);
            Console.ResetColor();
        }

        public static T TryConvert<T>(string text, bool isNewLine)
        {
            T number;

            while (true)
            {
                if (isNewLine)
                    Console.WriteLine(text);
                else
                    Console.Write(text);

                try
                {
                    number = (T)Convert.ChangeType(Console.ReadLine(), typeof(T));
                    return number;
                }
                catch (Exception)
                {
                    ErrorMessage("Invalid number");
                    continue;
                }
            }
        }
    }
}
