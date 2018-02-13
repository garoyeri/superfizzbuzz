using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace SuperFizzBuzz.Core.Tests
{
    /// <summary>
    /// Test Data File Reader
    /// </summary>
    public static class FizzBuzzCsvFile
    {
        /// <summary>
        /// Read a test data file from the specified path.
        /// </summary>
        /// <typeparam name="T">The data record type to read from the file.</typeparam>
        /// <param name="filename">The path to the data file, both absolute and relative paths are allowed.</param>
        /// <returns>A sequence of messages from the "Messages" column of the data file.</returns>
        public static IEnumerable<string> Read<T>(string filename)
            where T: IDataRecord
        {
            // make path relative to appdomain directory instead of Environment.CurrentDirectory
            if (!Path.IsPathRooted(filename))
            {
                filename = Path.Combine(
                    AppDomain.CurrentDomain.BaseDirectory,
                    filename);
            }

            using (var inputFile = new StreamReader(filename))
            using (var reader = new CsvHelper.CsvReader(inputFile))
            {
                var records = reader.GetRecords<T>();

                return records.Select(r => r.Message).ToArray();
            }
        }

        /// <summary>
        /// A data record for FizzBuzz test data files. At least the "Message" column is required.
        /// </summary>
        public interface IDataRecord
        {
            string Message { get; set; }
        }

        /// <summary>
        /// Data record for the classic FizzBuzz test data.
        /// </summary>
        public class FizzBuzzDataRecord : IDataRecord
        {
            public string Id { get; set; }
            public string Mult3 { get; set; }
            public string Mult5 { get; set; }
            public string Message { get; set; }
        }

        /// <summary>
        /// Data record for the Super Fizz Buzz test data.
        /// </summary>
        public class SuperFizzBizzDataRecord : IDataRecord
        {
            public string Id { get; set; }
            public string Mult3 { get; set; }
            public string Mult7 { get; set; }
            public string Mult38 { get; set; }
            public string Message { get; set; }
        }
    }
}
