/* (C) 2016 Premysl Fara */

namespace ArtMananager.Core
{
    using System;
    using System.Globalization;
    using System.Threading;


    /// <summary>
    /// Static helper class for Culture
    /// </summary>
    public static class Culture
    {
        #region consts

        /// <summary>
        /// yyyyMMdd
        /// </summary>
        /// <remarks>Dint format (date stored as int)</remarks>
        public const string DintFormat = "yyyyMMdd";

        /// <summary>
        /// dd.MM.yyyy
        /// </summary>
        public const string DateFormat = "dd.MM.yyyy";

        /// <summary>
        /// dd.MM.yyyy HH:mm:ss.fff
        /// </summary>
        public const string DateTimeFormat = "dd.MM.yyyy HH:mm:ss.fff";

        /// <summary>
        /// dd.MM.yyyy HH:mm:ss
        /// </summary>
        public const string DateTimeFormatShort = "dd.MM.yyyy HH:mm:ss";

        /// <summary>
        /// dd.MM.yyyy HH:mm
        /// </summary>
        public const string DateTimeFormatMinutes = "dd.MM.yyyy HH:mm";

        /// <summary>
        /// HH:mm:ss.fff
        /// </summary>
        public const string TimeFormat = "HH:mm:ss.fff";

        /// <summary>
        /// d-MMM-yy
        /// </summary>
        public const string ReportingDateFormat = "d-MMM-yy";

        /// <summary>
        /// #,###.000;-#,###.000;0
        /// </summary>
        public const string NumberFormat = "#,###.000;-#,###.000;0";

        /// <summary>
        /// 0.##
        /// </summary>
        public const string ShortNumberFormat = "0.##";

        /// <summary>
        /// 0.###
        /// </summary>
        public const string MediumNumberFormat = "0.###";

        /// <summary>
        /// 0.####
        /// </summary>
        public const string NotSoLongZeroesNumberFormat = "0.0000";

        /// <summary>
        /// 0.00
        /// </summary>
        public const string ShortZeroesNumberFormat = "0.00";

        /// <summary>
        /// 0.000
        /// </summary>
        public const string MediumZeroesNumberFormat = "0.000";

        /// <summary>
        /// #,###,###.##
        /// </summary>
        public const string GroupedShortNumberFormat = "#,###,###.##";

        /// <summary>
        /// #,###,###.###
        /// </summary>
        public const string GroupedMediumNumberFormat = "#,###,###.###";

        /// <summary>
        /// #,###,##0.00;-#,###,##0.00;0.00
        /// </summary>
        public const string GroupedZeroesNumberFormat = "#,###,##0.00;-#,###,##0.00;0.00";

        /// <summary>
        /// #,###,##0.0000;-#,###,##0.0000;#
        /// </summary>
        public const string GroupedLongZeroesNumberFormat = "#,###,##0.0000;-#,###,##0.0000;#";

        /// <summary>
        /// #,###,###
        /// </summary>
        public const string GroupedWholeNumberFormat = "#,###,###;-#,###,###;0";

        /// <summary>
        /// 0.#############################
        /// </summary>
        /// <remarks>Number format equivalent to G29, i.e. trails unnecessary zeros (but G29 would display 0,0000000000001 as 1E-13)</remarks>
        public const string LongNumberFormat = "0.#############################";

        /// <summary>
        /// 0.00000000
        /// </summary>
        /// <remarks>Number format for MWh column in Future monthly position report</remarks>
        public const string FuturesMwhNumberFormat = "0.00000000";

        /// <summary>
        /// 0;-0;\"\"
        /// </summary>
        /// <remarks>Non-zero number only, i.e. empty string for zero</remarks>
        public const string IdFormat = "0;-0;\"\"";

        /// <summary>
        /// Default time in DB (1.1.1900)
        /// </summary>
        public static readonly DateTime DefaultTime = new DateTime(1900, 1, 1);

        /// <summary>
        /// Default maximum time in DB (1.1.1900)
        /// </summary>
        public static readonly DateTime DefaultMaxTime = new DateTime(2050, 1, 1);

        #endregion


        #region public methods

        /// <summary>
        /// Returns this application thread's CultureInfo.
        /// </summary>
        /// <returns>This application thread's CultureInfo.</returns>
        public static CultureInfo GetCurrentApplicationCultureInfo()
        {
            return Thread.CurrentThread.CurrentCulture;
        }

        /// <summary>
        /// Returns a CultureInfo, based on the this thread's CultureInfo, configured to expected values. 
        /// </summary>
        /// <returns>A configured CultureInfo.</returns>
        public static CultureInfo GetDefaultApplicationCultureInfo()
        {
            // Thread.CurrentThread.CurrentCulture = System.Globalization.CultureInfo.InvariantCulture;

            return new CultureInfo(Thread.CurrentThread.CurrentCulture.Name)
            {
                DateTimeFormat =
                {
                    ShortDatePattern = "yyyy-MM-dd", 
                    DateSeparator = "-",
                    FirstDayOfWeek = DayOfWeek.Monday  // for a proper week number display
                },

                NumberFormat =
                {
                    CurrencyDecimalSeparator = ".", 
                    NumberDecimalSeparator = ".",
                    CurrencyGroupSeparator = " ",
                    NumberGroupSeparator = " "
                }
            };
        }

        /// <summary>
        /// Extension method to convert DateTime to dint format
        /// </summary>
        /// <param name="value">DateTime</param>
        /// <returns>Dint (int)</returns>
        public static int ToDint(this DateTime value)
        {
            return int.Parse(value.ToString(DintFormat, CultureInfo.InvariantCulture));
        }

        /// <summary>
        /// Extension method to convert nullable DateTime to dint format
        /// </summary>
        /// <param name="value">Nullable DateTime</param>
        /// <returns>Dint (int)</returns>
        public static int ToDint(this DateTime? value)
        {
            return value.HasValue == false
                ? 0
                : int.Parse(value.Value.ToString(DintFormat, CultureInfo.InvariantCulture));
        }

        /// <summary>
        /// Extension method to convert dint format to DateTime
        /// </summary>
        /// <param name="dint">Dint (int)</param>
        /// <returns>DateTime</returns>
        public static DateTime ToDateTime(this int dint)
        {
            return DateTime.ParseExact(dint.ToString(CultureInfo.InvariantCulture), DintFormat, CultureInfo.InvariantCulture);
        }


        /// <summary>
        /// Checks, if a string contains a text. Hendles empty strings too.
        /// </summary>
        /// <param name="s">A string.</param>
        /// <param name="text">A text.</param>
        /// <returns>True, if a string contains a text.</returns>
        public static bool StringContainsText(string s, string text)
        {

            return String.IsNullOrEmpty(s)
                ? String.IsNullOrEmpty(text)
                : (CultureInfo.InvariantCulture.CompareInfo.IndexOf(s, text ?? String.Empty, CompareOptions.IgnoreCase) >= 0);
        }

        #endregion
    }
}
