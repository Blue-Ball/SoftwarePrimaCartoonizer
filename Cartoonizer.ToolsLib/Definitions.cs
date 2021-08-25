using System;
using System.Collections.Generic;
using System.Text;

namespace Cartoonizer.ToolsLib
{
    /// <summary>
    /// Defines drawing tool
    /// </summary>
    public enum ToolType
    {
        None,
        Pointer,
        Rectangle,
        Ellipse,
        Line,
        PolyLine,
        Text,
        Marker,
        Arrow,
        WaterMark,
        Crop,
        Max
    };

    /// <summary>
    /// Context menu command types
    /// </summary>
    internal enum ContextMenuCommand
    {
        SelectAll,
        UnselectAll,
        Delete, 
        DeleteAll,
        MoveToFront,
        MoveToBack,
        Undo,
        Redo,
        SerProperties
    };
}
