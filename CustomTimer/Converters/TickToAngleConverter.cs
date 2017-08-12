using System;
using System.Globalization;
using System.Windows.Controls;
using System.Windows.Data;

namespace Timer
{
    public class TickToAngleConverter : IMultiValueConverter
    {
        private const double FULL_DEGREE_ANGLE_WITH_MARGIN_ERROR = 359.98;

        /// <summary>
        /// Converts Ticks to Degree angle
        /// </summary>
        /// <param name="values"></param>
        /// <param name="targetType"></param>
        /// <param name="parameter"></param>
        /// <param name="culture"></param>
        /// <returns></returns>
        public object Convert(object[] values, Type targetType, object parameter, CultureInfo culture)
        {
            double ticks;
            if (double.TryParse(values[0].ToString(), out ticks))
            {
                ProgressBar bar = values[1] as ProgressBar;
                return FULL_DEGREE_ANGLE_WITH_MARGIN_ERROR * (ticks / (bar.Maximum - bar.Minimum)) + 180;
            }
            return null;
        }

        public object[] ConvertBack(object value, Type[] targetTypes, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
