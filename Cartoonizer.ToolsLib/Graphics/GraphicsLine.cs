using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;
using Cartoonizer.ToolsLib.Tools.Properties;



namespace Cartoonizer.ToolsLib
{
    /// <summary>
    ///  Line graphics object.
    /// </summary>
    public class GraphicsLine : GraphicsBase
    {
        #region Class Members

        protected Point lineStart;
        protected Point lineEnd;

        #endregion Class Members

        #region Constructors

        public GraphicsLine(Point start, Point end, double lineWidth, Color objectColor, double actualScale)
        {
            this.lineStart = start;
            this.lineEnd = end;
            this.graphicsLineWidth = lineWidth;
            this.graphicsObjectColor = objectColor;
            this.graphicsActualScale = actualScale;

            //RefreshDrawng();
        }

        public GraphicsLine()
            :
            this(new Point(0.0, 0.0), new Point(100.0, 100.0), 1.0, Colors.Black, 1.0)
        {
        }

        #endregion Constructors

        #region Properties

        public Point Start
        {
            get { return lineStart; }
            set { lineStart = value; }
        }

        public Point End
        {
            get { return lineEnd; }
            set { lineEnd = value; }
        }

        #endregion Properties

        #region Overrides

        /// <summary>
        /// Draw object
        /// </summary>
        public override void Draw(DrawingContext drawingContext)
        {
            if (drawingContext == null)
            {
                throw new ArgumentNullException("drawingContext");
            }

            Pen pin = new Pen(new SolidColorBrush(ObjectColor), ActualLineWidth);
            pin.EndLineCap = PenLineCap.Triangle;

            double headWidth = 5 * ActualLineWidth;
            double headHeight = 4 * ActualLineWidth;

            double theta = Math.Atan2(lineStart.Y - lineEnd.Y, lineStart.X - lineEnd.X);
            double sint = Math.Sin(theta);
            double cost = Math.Cos(theta);

            Point pt3 = new Point(
                lineEnd.X + (headWidth * cost - headHeight * sint),
                lineEnd.Y + (headWidth * sint + headHeight * cost));

            Point pt4 = new Point(
                lineEnd.X + (headWidth * cost + headHeight * sint),
                lineEnd.Y - (headHeight * cost - headWidth * sint));

            drawingContext.DrawLine(pin, lineStart, lineEnd);
            drawingContext.DrawLine(pin, pt3, lineEnd);
            drawingContext.DrawLine(pin, pt4, lineEnd);

            base.Draw(drawingContext);
        }

        /// <summary>
        /// Test whether object contains point
        /// </summary>
        public override bool Contains(Point point)
        {
            LineGeometry g = new LineGeometry(lineStart, lineEnd);

            return g.StrokeContains(new Pen(Brushes.Black, LineHitTestWidth), point);
        }

        /// <summary>
        /// XML serialization support
        /// </summary>
        /// <returns></returns>
        public override PropertiesGraphicsBase CreateSerializedObject()
        {
            return new PropertiesGraphicsLine(this);
        }

        /// <summary>
        /// Get number of handles
        /// </summary>
        public override int HandleCount
        {
            get
            {
                return 2;
            }
        }

        /// <summary>
        /// Get handle point by 1-based number
        /// </summary>
        public override Point GetHandle(int handleNumber)
        {
            if (handleNumber == 1)
                return lineStart;
            else
                return lineEnd;
        }

        /// <summary>
        /// Hit test.
        /// Return value: -1 - no hit
        ///                0 - hit anywhere
        ///                > 1 - handle number
        /// </summary>
        public override int MakeHitTest(Point point)
        {
            if (IsSelected)
            {
                for (int i = 1; i <= HandleCount; i++)
                {
                    if (GetHandleRectangle(i).Contains(point))
                        return i;
                }
            }

            if (Contains(point))
                return 0;

            return -1;
        }

        protected override void TransformPoints(RotateTransform rt)
        {
            lineStart = rt.Transform(lineStart);
            lineEnd = rt.Transform(lineEnd);
        }

        protected override Point GetCenterPoint()
        {
            return new Point((lineStart.X + lineEnd.X) / 2, (lineStart.Y + lineEnd.Y) / 2);
        }


        /// <summary>
        /// Test whether object intersects with rectangle
        /// </summary>
        public override bool IntersectsWith(Rect rectangle)
        {
            RectangleGeometry rg = new RectangleGeometry(rectangle);

            LineGeometry lg = new LineGeometry(lineStart, lineEnd);
            PathGeometry widen = lg.GetWidenedPathGeometry(new Pen(Brushes.Black, LineHitTestWidth));

            PathGeometry p = Geometry.Combine(rg, widen, GeometryCombineMode.Intersect, null);

            return (!p.IsEmpty());
        }

        /// <summary>
        /// Get cursor for the handle
        /// </summary>
        public override Cursor GetHandleCursor(int handleNumber)
        {
            switch (handleNumber)
            {
                case 1:
                case 2:
                    return Cursors.SizeAll;
                default:
                    return HelperFunctions.DefaultCursor;
            }
        }

        /// <summary>
        /// Move handle to new point (resizing)
        /// </summary>
        public override void MoveHandleTo(Point point, int handleNumber)
        {
            if (handleNumber == 1)
                lineStart = point;
            else
                lineEnd = point;

            RefreshDrawing();
        }

        /// <summary>
        /// Move object
        /// </summary>
        public override void Move(double deltaX, double deltaY)
        {
            lineStart.X += deltaX;
            lineStart.Y += deltaY;

            lineEnd.X += deltaX;
            lineEnd.Y += deltaY;

            RefreshDrawing();
        }


        #endregion Overrides


        public override Point? GetRotateHandle()
        {
            return null;
        }
    }
}
