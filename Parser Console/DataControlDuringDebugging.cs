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
        /// Вывод на консоль данную строку красным цветом
        /// </summary>
        /// <param name="str">Строка для вывода</param>
        /// <param name="carriageReturn">переход каретки после вывода данной строки</param>
        /// <param name="stBreakLine">линия разделения перед текстом</param>
        /// <param name="endBreakLine">линия разделения после текста</param>
        public static void PrintConsoleColor(string str, bool carriageReturn, bool stBreakLine, bool endBreakLine)
        {
            PrintConsoleColor(str, ConsoleColor.Red, carriageReturn, stBreakLine, endBreakLine);
        }

        /// <summary>
        /// Выводит на консоль данную строку заданным цветом с пустыми строками до и после.
        /// </summary>
        /// <param name="str">Строка для вывода</param>
        /// <param name="consoleColor">Цвет консоли</param>
        /// <param name="carriageReturn">переход каретки после вывода данной строки (так же добавляет линии разделения сразу перед и после текста)</param>
        /// <param name="stBreakLine">линия разделения перед текстом</param>
        /// <param name="endBreakLine">линия разделения после текста</param>
        public static void PrintConsoleColor(
            string str, ConsoleColor consoleColor, bool carriageReturn = true, bool stBreakLine = true, bool endBreakLine = true)
        {
            // строка разрыва вывода
            string breakLine = " - - ";

            if (carriageReturn)
            {
                Console.ForegroundColor = consoleColor;
                Console.WriteLine(breakLine);
                Console.WriteLine(str);
                Console.WriteLine(breakLine);
                Console.ResetColor();
            }
            else
            {
                Console.ForegroundColor = consoleColor;
                if(stBreakLine) Console.WriteLine(breakLine);
                Console.Write(stBreakLine ? $"{str}" : $" {str}");
                if(endBreakLine) Console.WriteLine($" \n{breakLine}");
                Console.ResetColor();
            }



        }
    }
}
