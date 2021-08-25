using System;
using System.Linq;
using System.IO;
using System.Windows.Input;
using System.Windows;

namespace Cartoonizer.ToolsLib.Tools
{
    class ToolCrop : ToolRectangleBase
    {
        public ToolCrop()
        {
            MemoryStream stream = new MemoryStream(Cartoonizer.ToolsLib.Properties.Resources.Rectangle);
            ToolCursor = new Cursor(stream);
        }

        /// <summary>
        /// Create new rectangle
        /// </summary>
        public override void OnMouseDown(DrawingCanvas drawingCanvas, MouseButtonEventArgs e)
        {
            Point p = e.GetPosition(drawingCanvas);

            AddNewObject(drawingCanvas,
                new GraphicsCrop(
                p.X,
                p.Y,
                p.X + 1,
                p.Y + 1,
                1,
                System.Windows.Media.Colors.Black,
                drawingCanvas.ActualScale,
                drawingCanvas.Width,
                drawingCanvas.Height));
        }

        public override void OnMouseUp(DrawingCanvas drawingCanvas, MouseButtonEventArgs e)
        {
            base.OnMouseUp(drawingCanvas, e);

            if (drawingCanvas.Count > 0)
            {

                var cropTool = drawingCanvas[drawingCanvas.Count - 1];
                if (cropTool is GraphicsCrop)
                {
                    drawingCanvas.ApplyCrop(cropTool as GraphicsCrop);
                }
            }
        }
    }
}
