using System;
using System.Data;
using System.IO;
using System.Text;
using ExcelDataReader;
using Parser_Console.Models;

namespace Parser_Console
{
    public class Parser
    {
        public Parser()
        {
            // добавляем кодировку 1252 в Core
            Encoding.RegisterProvider(CodePagesEncodingProvider.Instance);
            //var enc1252 = Encoding.GetEncoding(1252);
        }

        /// <summary>
        /// Парсит файл .xls
        /// </summary>
        /// <param name="path">путь к файлу</param>
        public void ExcelXLS(string pathFilePrice)
        {
            // todo добавить тест

            Console.WriteLine(File.Exists(pathFilePrice) ? "File exists." : "File does not exist.");

            if (File.Exists(pathFilePrice))
            {
                // получили из файла таблицу с данными типа DataSet
                DataSet tableDataSet = XLStoDataSet(pathFilePrice);

                // временно работаем с одной страницей в документе
                var tablesObject = tableDataSet.Tables;
                var table = tablesObject[5];
                Console.WriteLine(table.TableName);
                
                // ТЕСТ вывод на консоль названий столбцов
                foreach (DataColumn column in table.Columns)
                    Console.Write("{0, 25}", column.ColumnName);
                Console.WriteLine();

                Developer developerTable = ConvertToModel(table);

                
            }
            //return result;
        }

        /// <summary>
        /// Получаем модель данных квартир у застройщика
        /// </summary>
        /// <param name="table"></param>
        /// <returns></returns>
        private Developer ConvertToModel(DataTable table)
        {
            Developer developer = new Developer(); 
            object[] cells = null;
            string startPrice = "Прайс-лист на квартиры от";
            string startPrice1 = "Прайс-лист на квартиры на";
            bool startPriceToken = false;
            DateTime dateRelisePrice = default;
            string cellString;
            int numberAddressInDeveloper; // номер адреса(объекта для коллекции) у застройщика 

            // перебор всех строк таблицы
            for (int a = 0; a < table.Rows.Count; a++)
            {
                // получаем все ячейки строки
                cells = table.Rows[a].ItemArray;
                for (int b = 0; b < cells.Length; b++)
                {
                    cellString = cells[b].ToString();
                    // находим строку, после которой начинается прайс с квартирами и присваиваем токену true
                    if (!startPriceToken && cellString.Contains(startPrice))
                    {
                        Console.WriteLine("Найдена строка начала прайса");
                        startPriceToken = true;
                        string dtStr = (cellString.TrimStart(startPrice.ToCharArray())).TrimEnd(' ', 'г', '.');
                        dateRelisePrice = Convert.ToDateTime(dtStr);
                        Console.WriteLine($"Дата прайса: {dateRelisePrice}");
                    }
                    if (!startPriceToken && cellString.Contains(startPrice1))
                    {
                        Console.WriteLine("Найдена строка начала прайса 2");
                        startPriceToken = true;
                        string dtStr = (cellString.TrimStart(startPrice.ToCharArray())).TrimEnd(' ', 'г', '.');
                        dateRelisePrice = Convert.ToDateTime(dtStr);
                        Console.WriteLine($"Дата прайса: {dateRelisePrice}");
                    }

                    if (startPriceToken)
                    {
                        #region Описание логики парсинга

                        // Если первая ячейка длинная строка (больше 20 символов) - это
                        // - Или Адрес
                        // - Или строка где написано Чистовая / Черновая
                        // - Или срок сдачи
                        //
                        // Если короткая строка - то это
                        // - Сдан
                        // - Тип Квартир
                        // - Номер квартиры
                        //
                        // Если первая пустая, но вторая число - это
                        // - квартира, но нужно взять тип квартиры из верхней ячейки

                        #endregion
                        
                        if (b == 0 && cellString.Length > 20)
                        {
                            // либо Адрес, либо Чистовая/Черновая, либо срок сдачи

                            if (cellString.Contains("ул.") && cellString.Contains("д.") && !cellString.Contains(" можно "))
                            {
                                // это адрес, парсим его и вносим в данные

                                int stAddress = cellString.IndexOf("ул. ");
                                int endAddress = cellString.IndexOf("д.");
                                int stHomeNumber = cellString.IndexOf("д.");
                                int endHomeNumber = cellString.IndexOf("(");
                                if (endHomeNumber < 0) endHomeNumber = cellString.IndexOf(", п");

                                string address = cellString.Substring(stAddress + 4, endAddress - 5 - stAddress);
                                string homeNumber = cellString.Substring(stHomeNumber + 2, endHomeNumber - 2 - stHomeNumber);
                            }
                            
                        }
                    }

                    Console.Write("{0, 25}", cellString);
                }

                Console.WriteLine();
            }

            return null;
            //foreach (DataTable dt in result.Tables)
            //{
            //    
            //}
        }

        /// <summary>
        /// Получаем из файла данные в виде DataSet
        /// </summary>
        /// <param name="pathFilePrice"></param>
        /// <returns></returns>
        private DataSet XLStoDataSet(string pathFilePrice)
        {
            // Создаем поток для чтения.
            var stream = File.Open(pathFilePrice, FileMode.Open, FileAccess.Read);
            // Читатель для файлов с расширением *.xlsx.
            var excelReader = ExcelReaderFactory.CreateOpenXmlReader(stream);

            //// Читатель для файлов с расширением *.xls.
            //var excelReader = ExcelReaderFactory.CreateBinaryReader(stream);

            // Читаем, получаем DataSet и работаем с ним как обычно.
            var result = excelReader.AsDataSet();
            // После завершения чтения освобождаем ресурсы.
            excelReader.Close();
            return result;
        }

        /// <summary>
        /// Парсит файл .xlsx
        /// </summary>
        /// <param name="path">путь к файлу</param>
        public void ExcelXLSX(string pathFilePrice)
        {
            // todo добавить тест

            DataSet result = null;
            // тест контроль
            Console.WriteLine(File.Exists(pathFilePrice) ? "File exists." : "File does not exist.");

            if (File.Exists(pathFilePrice))
            {
                // Создаем поток для чтения.
                var stream = File.Open(pathFilePrice, FileMode.Open, FileAccess.Read);
                // Читатель для файлов с расширением *.xlsx.
                var excelReader = ExcelReaderFactory.CreateOpenXmlReader(stream);
                // Читаем, получаем DataSet и работаем с ним как обычно.
                result = excelReader.AsDataSet();
                // После завершения чтения освобождаем ресурсы.
                excelReader.Close();
            }
            //return result;
        }
    }
}
