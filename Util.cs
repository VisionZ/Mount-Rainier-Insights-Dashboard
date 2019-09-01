using System;
using System.ComponentModel;
using System.Globalization;

namespace MountRainerInsights
{

    public class Util
    {

        public static readonly string[] DEFAULT_DATE_FORMATS = { "MM dd yyyy", "M d yyyy", "yyyy MM dd", "yyyy M d" };

        public static readonly string[] DEFAULT_DATE_DELIMITERS = { "/", "-", "." };

        public static string ParseDate(string[] dateFormats, string[] dateDelimiters, string possibleDate)
        {
            string FormatDateField(int dateField)
            {
                return dateField < 10 ? "0" + dateField.ToString() : dateField.ToString();
            }

            foreach (string dateFormat in dateFormats)
            {
                foreach (string dateDelimiter in dateDelimiters)
                {
                    string correctedDateFormat = dateFormat.Replace(" ", dateDelimiter);
                    if (DateTime.TryParseExact(possibleDate, correctedDateFormat, null, DateTimeStyles.None, out DateTime parsedDate))
                    {
                        return parsedDate.Year + "-" + FormatDateField(parsedDate.Month) + "-" + FormatDateField(parsedDate.Day);
                    }
                }
            }

            return null;
        }

        public static void PrintProperties(object obj)
        {
            foreach (PropertyDescriptor descriptor in TypeDescriptor.GetProperties(obj))
            {
                string name = descriptor.Name;
                object value = descriptor.GetValue(obj);
                Console.WriteLine("{0}: {1}", name, value);
            }
        }

        public static void PrintExceptionInfo(Exception ex)
        {
            Console.WriteLine("Message: " + ex.Message);
            Console.WriteLine("Source: " + ex.Source);
            Console.WriteLine("Help Link: " + ex.HelpLink);
            Console.WriteLine("Stack Trace: " + ex.StackTrace);
        }
    }
}