using System;

namespace Parser_Console
{
    class Program
    {
        static void Main(string[] args)
        {
            // открывает файл // todo добавить автоматически выбор класса парсера по расширению файла
            Parser parser = new Parser();
            parser.ExcelXLS(@"E:\OneDrive\РАБОТА\Риэлтор\Застройщики\ССК Прайс 2020.10.23.xls");
            //parser.ExcelXLS(@"E:\OneDrive\РАБОТА\Риэлтор\Застройщики\Железно, квартиры 2020.10.23.xlsx");

            // распарсивает по заданным алгоритмам

            // возвращает данные

            Console.ReadLine();
        }
    }
}
