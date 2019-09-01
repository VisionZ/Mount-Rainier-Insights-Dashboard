using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Nest;
using NotVisualBasic.FileIO;

namespace MountRainerInsights
{

    public class Parser
    {
        public Parser()
        {

        }

        public string FilePath
        {
            get; set;
        }

        public static object CreateInstance(Type t)
        {
            try
            {
                return t.GetConstructor(new Type[] { }).Invoke(new object[] { });
            }
            catch
            {
                return null;
            }
        }

        public LinkedList<CsvSerializable> ParseFromFile(Type exactType)
        {
            if (!typeof(CsvSerializable).IsAssignableFrom(exactType))
            {
                Console.WriteLine(exactType + " does not implement " + typeof(CsvSerializable) + ".");
                return new LinkedList<CsvSerializable>();
            }

            LinkedList<CsvSerializable> parsedList = new LinkedList<CsvSerializable>();

            using (CsvTextFieldParser parser = new CsvTextFieldParser(FilePath, Encoding.UTF8))
            {
                parser.TrimWhiteSpace = true;

                if (!parser.EndOfData)
                {
                    try
                    {
                        string[] fields = parser.ReadFields();
                        Console.WriteLine("There are " + fields.Length + " column fields.");
                        foreach (string field in fields)
                        {
                            Console.WriteLine(field);
                        }
                        Console.WriteLine();
                    }
                    catch (Exception ex)
                    {
                        Util.PrintExceptionInfo(ex);
                    }
                }

                int linesRead = 0;
                int errors = 0;

                while (!parser.EndOfData)
                {
                    ++linesRead;
                    try
                    {
                        string[] fieldValues = parser.ReadFields();
                        object created = CreateInstance(exactType);
                        if (created is CsvSerializable)
                        {
                            CsvSerializable instance = (CsvSerializable)created;
                            if (instance.Import(fieldValues))
                            {
                                parsedList.AddLast(instance);
                            }
                            else
                            {
                                ++errors;
                            }
                        }
                        else
                        {
                            Console.WriteLine("Could not create an instance of " + exactType + ".");
                            return new LinkedList<CsvSerializable>();
                        }
                    }
                    catch (Exception ex)
                    {
                        Util.PrintExceptionInfo(ex);
                    }
                }

                Console.WriteLine("Successfully parsed " + parsedList.Count + " documents from " + linesRead + " lines.");
                Console.WriteLine("Found " + errors + " errors.");

                return parsedList;
            }
        }
    }

    public class Indexer<T>
    {
        public Indexer(List<T> list)
        {
            int size = list.Count;
            Entries = new List<object>(size);
            for (int index = 0; index < size; ++index)
            {
                Entries.Add(list.ElementAt(index));
            }
        }

        public Indexer(LinkedList<T> list)
        {
            Entries = new List<object>(list.Count);
            foreach (object entry in list)
            {
                Entries.Add(entry);
            }
        }

        private List<object> Entries;

        public string ServerAddress
        {
            get; set;
        }

        public string PrimaryIndex
        {
            get; set;
        }

        public bool IndexToServer()
        {
            var settings = new ConnectionSettings(new Uri(ServerAddress)).DefaultIndex(PrimaryIndex);
            var client = new ElasticClient(settings);

            List<Task<IndexResponse>> taskList = new List<Task<IndexResponse>>(Entries.Count);

            foreach (object entry in Entries)
            {
                Task<IndexResponse> task = client.IndexDocumentAsync(entry);
                taskList.Add(task);
            }

            int failedCount = 0;

            foreach (Task<IndexResponse> task in taskList)
            {
                IndexResponse response = task.Result;
                if (!response.IsValid)
                {
                    ++failedCount;
                    break;
                }
            }

            Console.WriteLine(failedCount == 0 ? "Succesfully indexed " + Entries.Count + " entries." : "Failed to index " + failedCount + " entries.");

            return failedCount == 0;
        }
    }
}