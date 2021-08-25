using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Controls;
using System.Windows;
using System.Windows.Input;
using System.Windows.Media;
using Cartoonizer.ToolsLib.Tools.Properties;

namespace Cartoonizer.ToolsLib.Desginer
{

    public class DesignerItem : GraphicsRectangleBase
    {
        private ImageSource ImageSrc;

        #region Constructors

        public DesignerItem(ImageSource content, double left, double top, double right, double bottom,
            double lineWidth, Color objectColor, double actualScale)
        {
            this.ImageSrc = content;

            this.rectangleLeft = left;
            this.rectangleTop = top;
            this.rectangleRight = right;
            this.rectangleBottom = bottom;
            this.graphicsLineWidth = lineWidth;
            this.graphicsObjectColor = objectColor;
            this.graphicsActualScale = actualScale;

            //RefreshDrawng();
        }

        public DesignerItem(ImageSource content)
            :
            this(content, 0.0, 0.0, 100.0, 100.0, 1.0, Colors.Black, 1.0)
        {
        }

        #endregion Constructors

        #region Overrides

        /// <summary>
        /// Draw object
        /// </summary>
        public override void Draw(DrawingContext drawingContext)
        {

            double xCenter = (rectangleRight + rectangleLeft) / 2;
            double yCenter = (rectangleBottom + rectangleTop) / 2;

            RotateTransform RT = new RotateTransform();
            RT.Angle = -1 * angle;
            RT.CenterX = xCenter;
            RT.CenterY = yCenter;
            drawingContext.PushTransform(RT);
            
            if (drawingContext == null)
            {
                throw new ArgumentNullException("drawingContext");
            }

            //drawingContext.DrawRectangle(
            //    null,
            //    new Pen(new SolidColorBrush(ObjectColor), ActualLineWidth),
            //    Rectangle);

            drawingContext.DrawImage(ImageSrc, Rectangle);

            base.Draw(drawingContext);
            drawingContext.Pop();
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
            return new PropertiesDesignerItem(this);
        }


        #endregion Overrides

    }
}
