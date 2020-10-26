using System;

namespace Parser_Console
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.SetWindowSize(150, 50);
            // открывает файл // todo добавить автоматически выбор класса парсера по расширению файла
            Parser parser = new Parser();
            parser.ExcelXLS(@"..\..\..\Test Resources\ССК Прайс 2020.10.23.xls");
            //parser.ExcelXLS(@"..\..\..\Test Resources\Железно, квартиры 2020.10.26.xlsx");

            // распарсивает по заданным алгоритмам

            // возвращает данные

        }
    }
}
