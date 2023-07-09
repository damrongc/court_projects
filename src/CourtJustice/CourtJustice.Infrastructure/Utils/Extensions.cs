using System.Data;
using System.Globalization;
using System.Reflection;
using System.Xml;
using System.Xml.Serialization;

namespace CourtJustice.Infrastructure.Utils
{
    public static class Extensions
    {
        public static double Power(this double a, double b)
        {
            return System.Math.Pow(a, b);
        }
        public static double ToDouble(this string a)
        {
            return double.Parse(a.Replace(" ", "").Trim());
        }
        public static short ToInt16(this string a)
        {
            return short.Parse(a.Replace(" ", "").Trim());
        }
        public static int ToInt32(this string a)
        {
            return int.Parse(a.Replace(" ", "").Trim());
        }
        public static long ToLong(this string a)
        {
            return long.Parse(a.Replace(" ", "").Trim());
        }
        public static decimal ToDecimal(this string a)
        {
            return decimal.Parse(a.Replace(" ", "").Trim());
        }
        /// <summary>
        /// format double to #.##
        /// </summary>
        /// <param name="a"></param>
        /// <returns></returns>
        public static string ToFormat2Decimal(this decimal a)
        {
            return string.Format("{0:#,##0.00}", a);
        }
        public static string ToFormatNoDecimal(this decimal a)
        {
            return string.Format("{0:#,##0}", a);
        }
        public static string ToFormatNoDouble(this double a)
        {
            return string.Format("{0:#,##0}", a);
        }
        public static string ToFormat2Double(this double a)
        {
            return string.Format("{0:#,##0.00}", a);
        }
        public static string ToFormat2DigitNoComma(this double a)
        {
            return string.Format("{0:###0.00}", a);
        }
        public static string ToComma(this int a)
        {
            return string.Format("{0:#,##0}", a);
        }
        public static string ToDateFormat(this DateTime a)
        {
            return a.ToString("yyyy.MM.dd");
        }

        public static DateOnly ToDateOnly(this DateTime a)
        {
            return DateOnly.FromDateTime(a);
        }

        public static DateTime ConvertDateFormatTHToUS(this string date)
        {
            const string DATE_FORMAT = "dd/MM/yyyy";
      
            CultureInfo culture = new("en-US");
            var convertdDate = DateTime.ParseExact(date.Trim(), DATE_FORMAT, culture);
            if (convertdDate.Year > 2500) convertdDate = convertdDate.AddYears(-543);
            return convertdDate;
        }

        public static long ToUnixTimestamp(this DateTime value)
        {
            return (long)(value.ToUniversalTime().Subtract(new DateTime(1970, 1, 1))).TotalSeconds;
        }

        /// <summary>
        /// Gets a Unix timestamp representing the current moment
        /// </summary>
        /// <param name="ignored">Parameter ignored</param>
        /// <returns>Now expressed as a Unix timestamp</returns>
        public static long UnixTimestamp(this DateTime ignored)
        {
            return (long)(DateTime.UtcNow.Subtract(new DateTime(1970, 1, 1))).TotalSeconds;
        }

        /// <summary>
        /// Returns a local DateTime based on provided unix timestamp
        /// </summary>
        /// <param name="timestamp">Unix/posix timestamp</param>
        /// <returns>Local datetime</returns>
        public static DateTime ParseUnixTimestamp(long timestamp)
        {
            return (new DateTime(1970, 1, 1)).AddSeconds(timestamp).ToLocalTime();
        }

        public static DataTable ToDataTable<T>(this List<T> items)
        {
            DataTable dataTable = new DataTable(typeof(T).Name);

            //Get all the properties  
            PropertyInfo[] Props = typeof(T).GetProperties(BindingFlags.Public | BindingFlags.Instance);
            foreach (PropertyInfo prop in Props)
            {
                //Defining type of data column gives proper data table   
                var type = (prop.PropertyType.IsGenericType && prop.PropertyType.GetGenericTypeDefinition() == typeof(Nullable<>) ? Nullable.GetUnderlyingType(prop.PropertyType) : prop.PropertyType);
                //Setting column names as Property names  
                dataTable.Columns.Add(prop.Name, type);
            }
            foreach (T item in items)
            {
                var values = new object[Props.Length];
                for (int i = 0; i < Props.Length; i++)
                {
                    //inserting property values to datatable rows  
                    values[i] = Props[i].GetValue(item, null);
                }
                dataTable.Rows.Add(values);
            }
            return dataTable;
        }

        public static string Serialize<T>(this T value)
        {
            if (value == null)
            {
                return string.Empty;
            }
            try
            {
                var xmlserializer = new XmlSerializer(typeof(T));
                var stringWriter = new StringWriter();
                using (var writer = XmlWriter.Create(stringWriter))
                {
                    xmlserializer.Serialize(writer, value);
                    return stringWriter.ToString();
                }
            }
            catch (Exception ex)
            {
                throw new Exception("An error occurred", ex);
            }
        }
    }


}
