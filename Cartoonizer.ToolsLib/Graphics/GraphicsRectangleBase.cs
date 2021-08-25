using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;


namespace Cartoonizer.ToolsLib
{
    /// <summary>
    /// Base class for rectangle-based graphics:
    /// rectangle and ellipse.
    /// </summary>
    public abstract class GraphicsRectangleBase : GraphicsBase
    {
        #region Class Members

        protected double rectangleLeft;
        protected double rectangleTop;
        protected double rectangleRight;
        protected double rectangleBottom;

        protected double angle;

        #endregion Class Members

        #region Properties

        /// <summary>
        /// Read-only property, returns Rect calculated on the fly from four points.
        /// Points can make inverted rectangle, fix this.
        /// </summary>
        public Rect Rectangle
        {
            get
            {
                double l, t, w, h;

                if (rectangleLeft <= rectangleRight)
                {
                    l = rectangleLeft;
                    w = rectangleRight - rectangleLeft;
                }
                else
                {
                    l = rectangleRight;
                    w = rectangleLeft - rectangleRight;
                }

                if (rectangleTop <= rectangleBottom)
                {
                    t = rectangleTop;
                    h = rectangleBottom - rectangleTop;
                }
                else
                {
                    t = rectangleBottom;
                    h = rectangleTop - rectangleBottom;
                }

                return new Rect(l, t, w, h);
            }
        }

        public double Left
        {
            get { return rectangleLeft; }
            set { rectangleLeft = value; }
        }

        public double Top
        {
            get { return rectangleTop; }
            set { rectangleTop = value; }
        }

        public double Right
        {
            get { return rectangleRight; }
            set { rectangleRight = value; }
        }

        public double Bottom
        {
            get { return rectangleBottom; }
            set { rectangleBottom = value; }
        }

        #endregion Properties

        #region Overrides

        /// <summary>
        /// Get number of handles
        /// </summary>
        public override int HandleCount
        {
            get
            {
                return 8;
            }
        }

        /// <summary>
        /// Get handle point by 1-based number
        /// </summary>
        public override Point GetHandle(int handleNumber)
        {
            double x, y, xCenter, yCenter;

            xCenter = (rectangleRight + rectangleLeft) / 2;
            yCenter = (rectangleBottom + rectangleTop) / 2;
            x = rectangleLeft;
            y = rectangleTop;

            switch (handleNumber)
            {
                case 1:
                    x = rectangleLeft;
                    y = rectangleTop;
                    break;
                case 2:
                    x = xCenter;
                    y = rectangleTop;
                    break;
                case 3:
                    x = rectangleRight;
                    y = rectangleTop;
                    break;
                case 4:
                    x = rectangleRight;
                    y = yCenter;
                    break;
                case 5:
                    x = rectangleRight;
                    y = rectangleBottom;
                    break;
                case 6:
                    x = xCenter;
                    y = rectangleBottom;
                    break;
                case 7:
                    x = rectangleLeft;
                    y = rectangleBottom;
                    break;
                case 8:
                    x = rectangleLeft;
                    y = yCenter;
                    break;
            }

            return new Point(x, y);
        }

        public override Point? GetRotateHandle()
        {
            double xCenter = (rectangleRight + rectangleLeft) / 2;

            return new Point(xCenter, rectangleTop - 15);
        }

        /// <summary>
        /// Hit test.
        /// Return value: -1 - no hit
        ///                0 - hit anywhere
        ///                > 1 - handle number
        /// </summary>
        public override int MakeHitTest(Point point)
        {
            double xCenter = (rectangleRight + rectangleLeft) / 2;
            double yCenter = (rectangleBottom + rectangleTop) / 2;

            Point point2 = RotatePoint(point, new Point(xCenter, yCenter), angle);
            if (IsSelected)
            {
                for (int i = 1; i <= HandleCount; i++)
                {
                    if (GetHandleRectangle(i).Contains(point2))
                    {

                        return i;
                    }
                }

                if (IsOverRotation(point2))
                    return 9;
            }

            if (Contains(point2))
                return 0;

            return -1;
        }

        //private Point TransformPoints(Point point)
        //{

        //    double xCenter = (rectangleRight + rectangleLeft) / 2;
        //    double yCenter = (rectangleBottom + rectangleTop) / 2;

        //    double dx = point.X - xCenter;
        //    double dy = point.Y - yCenter;

        //    double m = Math.Sqrt(Math.Pow(dx, 2) + Math.Pow(dy, 2));

        //    double a = Math.Tanh(dy / dx) * 180;

        //    a = a - angle;

        //    double x = xCenter + (m * Math.Sin(a));
        //    double y = yCenter + (m * Math.Cos(a));


        //    return new Point(x, y);
        //}


        /// <summary>
        /// Rotates one point arount another one
        /// </summary>
        /// <param name="pointToRotate">the point to rotate</param>
        /// <param name="centerPoint">the centre point of rotation</param>
        /// <param name="angleInDegrees">the rotation angle in degrees </param>
        /// <returns>Rotated point</returns>
        static Point RotatePoint(Point pointToRotate, Point centerPoint, double angleInDegrees)
        {
            double angleInRadians = angleInDegrees * (Math.PI / 180);
            double cosTheta = Math.Cos(angleInRadians);
            double sinTheta = Math.Sin(angleInRadians);
            return new Point
            {
                X =
                    (int)
                    (cosTheta * (pointToRotate.X - centerPoint.X) -
                    sinTheta * (pointToRotate.Y - centerPoint.Y) + centerPoint.X),
                Y =
                    (int)
                    (sinTheta * (pointToRotate.X - centerPoint.X) +
                    cosTheta * (pointToRotate.Y - centerPoint.Y) + centerPoint.Y)
            };
        }

        private bool IsOverRotation(Point point)
        {
            bool isOver = false;

            Point? p = GetRotateHandle();
            if ((p.HasValue))
            {
                double dx = point.X - p.Value.X;
                double dy = point.Y - p.Value.Y;

                double r = Math.Pow(dx, 2) + Math.Pow(dy, 2);
                if ((r <= 25))
                    isOver = true;
            }
            return isOver;
        }



        /// <summary>
        /// Get cursor for the handle
        /// </summary>
        public override Cursor GetHandleCursor(int handleNumber)
        {
            switch (handleNumber)
            {
                case 1:
                    return Cursors.SizeNWSE;
                case 2:
                    return Cursors.SizeNS;
                case 3:
                    return Cursors.SizeNESW;
                case 4:
                    return Cursors.SizeWE;
                case 5:
                    return Cursors.SizeNWSE;
                case 6:
                    return Cursors.SizeNS;
                case 7:
                    return Cursors.SizeNESW;
                case 8:
                    return Cursors.SizeWE;
                case 9:
                    return Cursors.Cross;
                default:
                    return HelperFunctions.DefaultCursor;
            }
        }

        /// <summary>
        /// Move handle to new point (resizing)
        /// </summary>
        public override void MoveHandleTo(Point point, int handleNumber)
        {
            switch (handleNumber)
            {
                case 1:
                    rectangleLeft = point.X;
                    rectangleTop = point.Y;
                    break;
                case 2:
                    rectangleTop = point.Y;
                    break;
                case 3:
                    rectangleRight = point.X;
                    rectangleTop = point.Y;
                    break;
                case 4:
                    rectangleRight = point.X;
                    break;
                case 5:
                    rectangleRight = point.X;
                    rectangleBottom = point.Y;
                    break;
                case 6:
                    rectangleBottom = point.Y;
                    break;
                case 7:
                    rectangleLeft = point.X;
                    rectangleBottom = point.Y;
                    break;
                case 8:
                    rectangleLeft = point.X;
                    break;
                case 9:
                    UpdateRotationRectangle(point);
                    break;
            }

            RefreshDrawing();
        }

        private void UpdateRotationRectangle(Point point)
        {
            double xCenter = (rectangleRight + rectangleLeft) / 2;
            double yCenter = (rectangleBottom + rectangleTop) / 2;

            double dx = xCenter - point.X;
            double dy = yCenter - point.Y;

            if (dy == 0)
            {
                angle = (dx > 0 ? 90 : -90);
            }
            else
            {
                angle = Math.Atan(dx / dy) * (180 / Math.PI);
                if (dy < 0)
                {
                    angle += 180;
                }
            }
        }

        /// <summary>
        /// Test whether object intersects with rectangle
        /// </summary>
        public override bool IntersectsWith(Rect rectangle)
        {
            return Rectangle.IntersectsWith(rectangle);
        }

        /// <summary>
        /// Move object
        /// </summary>
        public override void Move(double deltaX, double deltaY)
        {
            rectangleLeft += deltaX;
            rectangleRight += deltaX;

            rectangleTop += deltaY;
            rectangleBottom += deltaY;

            RefreshDrawing();
        }

        /// <summary>
        /// Normalize rectangle
        /// </summary>
        public override void Normalize()
        {
            if (rectangleLeft > rectangleRight)
            {
                double tmp = rectangleLeft;
                rectangleLeft = rectangleRight;
                rectangleRight = tmp;
            }

            if (rectangleTop > rectangleBottom)
            {
                double tmp = rectangleTop;
                rectangleTop = rectangleBottom;
                rectangleBottom = tmp;
            }
        }


        #endregion Overrides

        public override bool Contains(Point point)
        {
            throw new NotImplementedException();
        }

        public override Tools.Properties.PropertiesGraphicsBase CreateSerializedObject()
        {
            throw new NotImplementedException();
        }


    }
}
