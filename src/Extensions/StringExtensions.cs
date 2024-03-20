using System;
using System.Globalization;
using System.Linq;
using System.Text.RegularExpressions;

namespace LibraryManagementSystem.Extensions
{
    public static class StringExtensions
    {

        public static string ToTitleCase(this string @this)
        {
            if (@this.Length <= 1)
            {
                return @this;
            }

            string result = "";
            foreach (var word in Regex.Split(@this, @"/\s+/"))
            {
                result += char.ToUpper(word.ElementAt(0)) + word[1..].ToLower();
            }

            return result;
        }

        public static long ToLong(this string @this)
        {
            if (long.TryParse(@this, out long result))
            {
                return result;
            }

            throw new Exceptions.StringUnparsableException("long");
        }

        public static long? ToNullableLong(this string @this)
        {
            if (long.TryParse(@this, out long result))
            {
                return result;
            }

            return null;
        }

        public static int ToInt(this string @this)
        {
            if (int.TryParse(@this, out int result))
            {
                return result;
            }

            throw new Exceptions.StringUnparsableException("int");
        }

        public static int? ToNullableInt(this string @this)
        {
            if (int.TryParse(@this, out int result))
            {
                return result;
            }

            return null;
        }

        public static DateTime ToDateTime(this string @this)
        {
            if (DateTime.TryParse(@this, out DateTime dt))
            {
                return dt;
            }

            throw new Exceptions.StringUnparsableException("DateTime");
        }

        public static DateTime ToDateTime(this string @this, string format)
        {
            if (DateTime.TryParseExact(@this, format, CultureInfo.InvariantCulture, DateTimeStyles.None, out DateTime dt))
            {
                return dt;
            }

            throw new Exceptions.StringUnparsableException("DateTimeWithFormat");
        }

        public static DateTime? ToNullableDateTime(this string @this)
        {
            if (DateTime.TryParse(@this, out DateTime dt))
            {
                return dt;
            }

            return null;
        }

        public static DateTime? ToNullableDateTime(this string @this, string format)
        {
            if (DateTime.TryParseExact(@this, format, CultureInfo.InvariantCulture, DateTimeStyles.None, out DateTime dt))
            {
                return dt;
            }

            return null;
        }

    }
}
