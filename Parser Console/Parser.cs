﻿using System;
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
            const string startPrice = "прайс-лист на квартиры от";
            const string startPrice1 = "прайс-лист на квартиры на";
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
                    cellString = cells[b].ToString()?.ToLower();
                    // находим строку, после которой начинается прайс с квартирами и присваиваем токену true
                    if (cellString != null && !startPriceToken && cellString.Contains(startPrice))
                    {
                        Console.WriteLine("Найдена строка начала прайса");
                        startPriceToken = true;
                        string dtStr = (cellString.TrimStart(startPrice.ToCharArray())).TrimEnd(' ', 'г', '.');
                        dateRelisePrice = Convert.ToDateTime(dtStr);
                        Console.WriteLine($"Дата прайса: {dateRelisePrice}");
                    }
                    if (cellString != null && !startPriceToken && cellString.Contains(startPrice1))
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

                        if (cellString != null && b == 0 && cellString.Length > 20)
                        {
                            // либо Адрес, либо Чистовая/Черновая, либо срок сдачи

                            #region Адрес дома

                            if (cellString.Contains("ул.") && cellString.Contains("д.") && !cellString.Contains(" можно "))
                            {
                                // это адрес, парсим его и вносим в данные

                                int stAddress = cellString.IndexOf("ул. ", StringComparison.Ordinal);
                                int endAddress = cellString.IndexOf("д.", StringComparison.Ordinal);
                                int stHomeNumber = cellString.IndexOf("д.", StringComparison.Ordinal);
                                int endHomeNumber = cellString.IndexOf("(", StringComparison.Ordinal);
                                if (endHomeNumber < 0) endHomeNumber = cellString.IndexOf(", п", StringComparison.Ordinal);

                                string address =
                                    cellString.Substring(stAddress + 4, endAddress - 5 - stAddress)
                                        .Trim(',', '.', ' ');

                                // делаем первую букву большой
                                char[] ch = address.ToCharArray();
                                ch[0] = Convert.ToChar(ch[0].ToString().ToUpper());
                                address = new string(ch);

                                string homeNumber =
                                    cellString.Substring(stHomeNumber + 2, endHomeNumber - 2 - stHomeNumber)
                                        .Trim(',', '.', ' ');
                            }

                            #endregion

                            #region Черновая или чистовая отделка

                            if (cellString.Contains("чистовой"))
                            {
                                // квартиры в чистовой отделке

                            }

                            if (cellString.Contains("черновой"))
                            {
                                // квартиры в черновой отделке

                            }

                            #endregion

                            if ((cellString.Contains("сдачи") || cellString.Contains("квартал"))
                                && (!cellString.Contains("остекление") || !cellString.Contains("лодж")))
                            {
                                // Распарсиваем строку, получаем срок сдачи.
                                // в следующей строке указывается очередь строительства и подъезд

                                #region Срок сдачи дома

                                int stCommissioningPeriod = cellString.IndexOf("сдачи", StringComparison.Ordinal) + 6;
                                string commissioningPeriod = cellString.Substring(stCommissioningPeriod,
                                    cellString.Length - stCommissioningPeriod);
                                // если вместо единицы встретился символ I
                                if (commissioningPeriod.Contains("i"))
                                    commissioningPeriod = commissioningPeriod.Replace('i', '1');

                                #endregion

                                string nextCell = cells[b + 1].ToString()?.ToLower();
                                int endConstructionPhase = nextCell.IndexOf("очере", StringComparison.Ordinal);
                                string constructionPhase = 
                                    nextCell.Substring(0, endConstructionPhase)
                                        .Trim(',', '.', ' ');
                                int stPorchesHouse = nextCell.IndexOf("ства", StringComparison.Ordinal) + 4;
                                int endPorchesHouse = nextCell.IndexOf("подъ", StringComparison.Ordinal);
                                string porchesHouse =
                                    nextCell.Substring(stPorchesHouse, endPorchesHouse - stPorchesHouse)
                                        .Trim(',', '.', ' ');
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
        /// <param name="pathFilePrice">путь к файлу</param>
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

        /// <summary>
        /// Убирает из строки запятые, точки и пробелы перед строкой и после строки
        /// </summary>
        /// <param name="address"></param>
        /// <returns></returns>
        //private string ClearString(string st, char[] ch)
        //{
        //    string st1 = st.Trim(ch);
        //    st1 = 
        //}
    }
}
