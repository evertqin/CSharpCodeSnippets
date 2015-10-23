using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CsvHelper;
using CSharpCodeSnippets.Array;
using CsvReader = LumenWorks.Framework.IO.Csv.CsvReader;

namespace CSharpCodeSnippets.Csv
{
    class CsvUtils
    {
        public static T[] ReadCsvFile<T>(string filepath)
        {
            using (CsvReader csv = new CsvReader(new StreamReader(filepath), false))
            {
                TypeConverter converter = TypeDescriptor.GetConverter(typeof(T));
                return
                    csv.SelectMany(
                        v => v.Select(u => (T)converter.ConvertFromString(null, CultureInfo.InvariantCulture, u)))
                        .ToArray();
            }
        }

        [Description("This is a helper function, and is not supposed to appear anywhere in the production code")]
        public static void DumpArrayToCsv<T>(T[,] array, string fileName,
            string filePath = @"Output")
        {
            using (CsvWriter csv = new CsvWriter(new StreamWriter(Path.Combine(filePath, fileName))))
            {
                    for (int i = 0; i < array.GetLength(0); ++i)
                    {
                        for (int j = 0; j < array.GetLength(1); ++j)
                        {
                            csv.WriteField(array[i, j]);
                        }
                        csv.NextRecord();
                    }
            }
        }

        [Description("This is a helper function, and is not supposed to appear anywhere in the production code")]
        public static void DumpListOfListToCsv<T>(IList<IList<T>> inList, string filename, string filePath = @"Output")
        {
            using (CsvWriter csv = new CsvWriter(new StreamWriter(Path.Combine(filePath, filename))))
            {
                foreach (var list in inList)
                {
                    foreach (var element in list)
                    {
                        csv.WriteField(element);
                    }
                    csv.NextRecord();
                }
            }
        }

        public static IList<IList<T>> GetListOfListFromCsv<T>(string filename)
        {
            using (var parser = new CsvParser(new StreamReader(filename)))
            {
                TypeConverter converter = TypeDescriptor.GetConverter(typeof(T));

                IList<IList<T>> ret = new List<IList<T>>();

                while (true)
                {
                    var row = parser.Read();
                    if (row == null)
                    {
                        break;
                    }

                    ret.Add(row.Where(u => u != null).Select(u => (T)converter.ConvertFromInvariantString(u)).ToList());

                }
                return ret;
            }

        }

    }
}
