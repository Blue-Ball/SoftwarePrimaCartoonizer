using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;
using Cartoonizer.ToolsLib.Tools.Properties;



namespace Cartoonizer.ToolsLib
{
    /// <summary>
    ///  Rectangle graphics object.
    /// </summary>
    public class GraphicsCrop : GraphicsRectangle
    {
        double _canvasWidth;
        double _canvasHeigh;

        #region Constructors

        public GraphicsCrop(double left, double top, double right, double bottom,
            double lineWidth, Color objectColor, double actualScale, double canvasWidth, double canvasHeigh)
        {
            this.rectangleLeft = left;
            this.rectangleTop = top;
            this.rectangleRight = right;
            this.rectangleBottom = bottom;
            this.graphicsLineWidth = lineWidth;
            this.graphicsObjectColor = objectColor;
            this.graphicsActualScale = actualScale;

            this._canvasHeigh = canvasHeigh;
            this._canvasWidth = canvasWidth;

            //RefreshDrawng();
        }

        public GraphicsCrop()
            :
            this(0.0, 0.0, 100.0, 100.0, 1.0, Colors.Black, 1.0, 0.0, 0.0)
        {
        }

        #endregion Constructors

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

            Color c = new Color();
            c.A = 128;

            SolidColorBrush brush = new SolidColorBrush(c);
            double top = Math.Min(this.Top, this.Bottom);
            double bottom = Math.Max(this.Top, this.Bottom);
            double left = Math.Min(this.Right, this.Left);
            double right = Math.Max(this.Right, this.Left);

            top = Math.Max(top, 0);
            bottom = Math.Min(bottom, _canvasHeigh);

            left = Math.Max(left, 0);
            right = Math.Min(right, _canvasWidth);

            drawingContext.DrawRectangle(
                  brush,
                  null,
                  new Rect(0, 0, this._canvasWidth, top));

            drawingContext.DrawRectangle(
                  brush,
                  null,
                  new Rect(0, bottom, this._canvasWidth, this._canvasHeigh));

            drawingContext.DrawRectangle(
                 brush,
                 null,
                 new Rect(0, top, left, bottom - top));

            drawingContext.DrawRectangle(
                 brush,
                 null,
                 new Rect(right, top, Math.Max(this._canvasWidth - right, 0), bottom - top));


            base.Draw(drawingContext);
        }

        /// <summary>
        /// Test whether object contains point
        /// </summary>
        public override bool Contains(Point point)
        {
            return this.Rectangle.Contains(point);
        }

        /// <summary>
        /// Serialization support
        /// </summary>
        public override PropertiesGraphicsBase CreateSerializedObject()
        {
            return new PropertiesGraphicsRectangle(this);
        }


        #endregion Overrides

    }
}
