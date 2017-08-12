using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Media;
using System.Windows.Shapes;

namespace Timer
{
    public class TimerArc : Shape
    {

        #region Dependency Properties

        /// <summary>
        /// Gets or sets the Start Point of the timer's arc.
        /// </summary>
        public double Start
        {
            get { return (double)GetValue(StartProperty); }
            set { SetValue(StartProperty, value); }
        }

        public static readonly DependencyProperty StartProperty =
                      DependencyProperty.Register(nameof(Start), typeof(double), typeof(TimerArc),
                            new FrameworkPropertyMetadata(0d,
                            FrameworkPropertyMetadataOptions.AffectsRender));

        /// <summary>
        /// Gets or sets the End Point of the timer's arc.
        /// </summary>
        public double End
        {
            get { return (double)GetValue(EndProperty); }
            set { SetValue(EndProperty, value); }
        }

        public static readonly DependencyProperty EndProperty =
                        DependencyProperty.Register(nameof(End), typeof(double), typeof(TimerArc),
                            new FrameworkPropertyMetadata(0d,
                            FrameworkPropertyMetadataOptions.AffectsRender));

        private static object OnEndPropertyValueChanged(DependencyObject d, object baseValue)
        {
            var timer = ((TimerArc)d);
            timer.TimerValue = String.Format("{0} : {1} : {2}", 1, 1, timer.End);
            return null;
        }

        /// <summary>
        /// Gets or sets the timer value
        /// </summary>
        public string TimerValue
        {
            get { return (string)GetValue(TimerValueProperty); }
            set { SetValue(TimerValueProperty, value); }
        }

        public static readonly DependencyProperty TimerValueProperty =
                        DependencyProperty.Register(nameof(TimerValue), typeof(string), typeof(TimerArc),
                            new FrameworkPropertyMetadata("0 : 0 : 0",
                            FrameworkPropertyMetadataOptions.AffectsRender));
        #endregion


        protected override System.Windows.Media.Geometry DefiningGeometry
        {
            get { return GetGeometry(); }
        }

        protected override System.Windows.Size MeasureOverride(Size constraint)
        {
            return base.MeasureOverride(constraint);
        }

        protected override System.Windows.Size ArrangeOverride(Size finalSize)
        {
            return base.ArrangeOverride(finalSize);
        }


        /// <summary>
        /// Gets the timer arc's geometry
        /// </summary>
        /// <returns>geometry</returns>
        private Geometry GetGeometry()
        {
            Point startPt = GetPoint(Math.Min(Start, End));
            Point endPt = GetPoint(Math.Max(Start, End));

            /* To draw the arc in perfect way instead of seeing it as Big arc */
            Size arc = new Size(Math.Max(0, (this.RenderSize.Width - this.StrokeThickness) / 2),
                                Math.Max(0, (this.RenderSize.Height - this.StrokeThickness) / 2));

            bool isgreaterthan180 = Math.Abs(End - Start) > 180;

            StreamGeometry geometry = new StreamGeometry();
            using (StreamGeometryContext drawcontext = geometry.Open())
            {
                drawcontext.BeginFigure(startPt, false, false);
                drawcontext.ArcTo(endPt, arc, 0, isgreaterthan180, SweepDirection.Counterclockwise, true, false);
            }

            double translate = this.StrokeThickness / 2;
            geometry.Transform = new TranslateTransform(translate, translate);

            return geometry;
        }

        private Point GetPoint(double angle)
        {
            /* rad = angle * pi/180 */
            double rad = angle * (Math.PI / 180);

            double xrad = (this.RenderSize.Width - this.StrokeThickness) / 2;
            double yrad = (this.RenderSize.Height - this.StrokeThickness) / 2;

            /* Find the point with radius and angle */
            /* X=  centerX + Raidus * Sin(rad) */
            /* Y = centerY + Radius * Cos(rad) */

            double x = xrad + xrad * Math.Sin(rad);
            double y = yrad + yrad * Math.Cos(rad);

            return new Point(x, y);
        }
    }
}
