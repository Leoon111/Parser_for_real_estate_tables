using System;
using System.Collections.Generic;
using System.Text;

namespace Parser_Console
{
    /// <summary>
    /// Класс, который предоставляет вспомогательные классы на время отладки приложения
    /// </summary>
    public static class DataControlDuringDebugging
    {
        /// <summary>
        /// Выводит на консоль данную строку красным цветом с пустыми строками до и после.
        /// </summary>
        /// <param name="str">Строка для вывода</param>
        public static void PrintConsoleColor(string str)
        {
            PrintConsoleColor(str, ConsoleColor.Red);
        }

        /// <summary>
        /// Выводит на консоль данную строку заданным цветом с пустыми строками до и после.
        /// </summary>
        /// <param name="str">Строка для вывода</param>
        /// <param name="consoleColor">Цвет консоли</param>
        public static void PrintConsoleColor(string str, ConsoleColor consoleColor)
        {

            Console.ForegroundColor = consoleColor;
            Console.WriteLine(" - - ");
            Console.WriteLine(str);
            Console.WriteLine(" - - ");
            Console.ResetColor();


        }
    }
}
