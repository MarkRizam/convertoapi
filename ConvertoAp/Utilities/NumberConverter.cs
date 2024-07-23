using System;
using System.Globalization;
using System.Linq;

namespace ConvertoApi.Utilities
{
    /// <summary>
    /// ----------------------------------------------------------------------------------------------------------------
    /// * Method Description : Utility class for converting numbers to words.
    /// ----------------------------------------------------------------------------------------------------------------
    /// * Change Summary
    /// * --------------
    ///   ID                Changed By         Changed On      Change Description
    ///*=======             ==========         ==========      ==================
    ///*RZM-127                Rizam          19-July-2024     Number to word conversion without NuGet
    ///
    /// </summary>
    public static class NumberConverter
    {
        private static readonly string[] UnitsMap = { "ZERO", "ONE", "TWO", "THREE", "FOUR", "FIVE", "SIX", "SEVEN", "EIGHT", "NINE", "TEN", "ELEVEN", "TWELVE", "THIRTEEN", "FOURTEEN", "FIFTEEN", "SIXTEEN", "SEVENTEEN", "EIGHTEEN", "NINETEEN" };
        private static readonly string[] TensMap = { "ZERO", "TEN", "TWENTY", "THIRTY", "FORTY", "FIFTY", "SIXTY", "SEVENTY", "EIGHTY", "NINETY" };

        /// <summary>
        /// Converts a decimal number to its words representation.
        /// </summary>
        /// <param name="number">The decimal number to convert.</param>
        /// <returns>The words representation of the decimal number.</returns>
        public static string Convert(decimal number)
        {
            if (number == 0)
                return "ZERO DOLLARS AND ZERO CENTS";

            var parts = number.ToString("F2", CultureInfo.InvariantCulture).Split('.');
            long dollars = long.Parse(parts[0]);
            long cents = long.Parse(parts[1]);

            string dollarWords = ConvertLongToWords(dollars) + (dollars == 1 ? " DOLLAR" : " DOLLARS");
            string centWords = ConvertLongToWords(cents) + (cents == 1 ? " CENT" : " CENTS");

            return dollarWords + " AND " + centWords;
        }

        private static string ConvertLongToWords(long number)
        {
            if (number == 0)
                return "ZERO";

            if (number < 0)
                return "MINUS " + ConvertLongToWords(Math.Abs(number));

            string words = "";

            if ((number / 1000000000000) > 0) 
            {
                words +=  ConvertLongToWords(number / 1000000000000) + " TRILLION ";
                number %= 1000000000000;
            }

            if ((number / 1000000000) > 0) 
            {
                words +=  ConvertLongToWords(number / 1000000000) + " BILLION ";
                number %= 1000000000;
            }

            if ((number / 1000000) > 0)
            {
                words += ConvertLongToWords(number / 1000000) + " MILLION ";
                number %= 1000000;
            }

            if ((number / 1000) > 0)
            {
                words += ConvertLongToWords(number / 1000) + " THOUSAND ";
                number %= 1000;
            }

            if ((number / 100) > 0)
            {
                words += ConvertLongToWords(number / 100) + " HUNDRED ";
                number %= 100;
            }

            if (number > 0)
            {
                if (words != "")
                    words += "AND ";

                if (number < 20)
                    words += UnitsMap[number];
                else
                {
                    words += TensMap[number / 10];
                    if ((number % 10) > 0)
                        words += "-" + UnitsMap[number % 10];
                }
            }

            return words.Trim();
        }
    }
}
