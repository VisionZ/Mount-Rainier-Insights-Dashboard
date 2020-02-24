using System;
using System.Globalization;
using Microsoft.VisualBasic;
using Nest;

namespace MountRainerInsights
{
    public class ElasticIndexer
    {
        public static void Main(string[] args)
        {
            Type[] types = { typeof(ClimbInfo), typeof(WeatherInfo) };
            string[] files = { "Climbing Data.csv", "Weather Data.csv" };
            string[] indexNames = { "climbinfo", "weatherinfo" };

            for (int index = 0, length = types.Length; index < length; ++index)
            {
                var parser = new Parser();
                parser.FilePath = "data\\" + files[index];

                var list = parser.ParseFromFile(types[index]);

                var indexer = new Indexer<CsvSerializable>(list);
                indexer.PrimaryIndex = indexNames[index];
                indexer.ServerAddress = "http://localhost:9200";
                indexer.IndexToServer();
            }
        }
    }
}
