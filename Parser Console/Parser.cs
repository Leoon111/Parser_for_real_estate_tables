﻿using System;
using System.Data;
using System.IO;
using System.Text;
using ExcelDataReader;

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
        /// <returns>таблицы данных, полученные из файла, тиа DataSet</returns>
        public DataSet ExcelXLS(string pathFilePrice)
        {
            // todo добавить тест

            DataSet result = null;

            // тест контроль
            Console.WriteLine(File.Exists(pathFilePrice) ? "File exists." : "File does not exist.");

            if (File.Exists(pathFilePrice))
            {
                // Создаем поток для чтения.
                var stream = File.Open(pathFilePrice, FileMode.Open, FileAccess.Read);
                // Читатель для файлов с расширением *.xls.
                var excelReader = ExcelReaderFactory.CreateBinaryReader(stream);
                // Читаем, получаем DataSet и работаем с ним как обычно.
                result = excelReader.AsDataSet();
                // После завершения чтения освобождаем ресурсы.
                excelReader.Close();

                var tablesObject = result.Tables;
                var table = tablesObject[5];
                Console.WriteLine(table.TableName);

                foreach (DataColumn column in table.Columns)
                    Console.Write("{0, 25}", column.ColumnName);
                //Console.Write("\t{0}", column.ColumnName);
                Console.WriteLine();
                // перебор всех строк таблицы
                foreach (DataRow row in table.Rows)
                {
                    // получаем все ячейки строки
                    var cells = row.ItemArray;
                    foreach (object cell in cells)
                        Console.Write("{0, 25}", cell);
                    Console.WriteLine();
                }

                //foreach (DataTable dt in result.Tables)
                //{
                //    //Console.WriteLine(dt.TableName); // название таблицы
                //    //// перебор всех столбцов
                //    //foreach (DataColumn column in dt.Columns)
                //    //    Console.Write("\t{0}", column.ColumnName);
                //    //Console.WriteLine();
                //    //// перебор всех строк таблицы
                //    //foreach (DataRow row in dt.Rows)
                //    //{
                //    //    // получаем все ячейки строки
                //    //    var cells = row.ItemArray;
                //    //    foreach (object cell in cells)
                //    //        Console.Write("\t{0}", cell);
                //    //    Console.WriteLine();
                //    //}
                //}
            }
            return result;
        }
        /// <summary>
        /// Парсит файл .xlsx
        /// </summary>
        /// <param name="path">путь к файлу</param>
        /// <returns>таблицы данных, полученные из файла, тиа DataSet</returns>
        public DataSet ExcelXLSX(string pathFilePrice)
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
            return result;
        }
    }
}
