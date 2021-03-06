using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;
using System.IO;


namespace Cartoonizer.ToolsLib.Tools
{
    /// <summary>
    /// Line tool
    /// </summary>
    class ToolLine : ToolObject
    {
        public ToolLine()
        {
            MemoryStream stream = new MemoryStream(Cartoonizer.ToolsLib.Properties.Resources.Line);
            ToolCursor = new Cursor(stream);
        }

        /// <summary>
        /// Create new object
        /// </summary>
        public override void OnMouseDown(DrawingCanvas drawingCanvas, MouseButtonEventArgs e)
        {
            Point p = e.GetPosition(drawingCanvas);

            AddNewObject(drawingCanvas,
                new GraphicsLine(
                p,
                new Point(p.X + 1, p.Y + 1),
                drawingCanvas.LineWidth,
                drawingCanvas.ObjectColor,
                drawingCanvas.ActualScale));

        }

        /// <summary>
        /// Set cursor and resize new object.
        /// </summary>
        public override void OnMouseMove(DrawingCanvas drawingCanvas, MouseEventArgs e)
        {
            drawingCanvas.Cursor = ToolCursor;

            if (e.LeftButton == MouseButtonState.Pressed)
            {
                drawingCanvas[drawingCanvas.Count - 1].MoveHandleTo(
                    e.GetPosition(drawingCanvas), 2);
            }
        }
    }
}
