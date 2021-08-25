using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;
using System.IO;
using System.Xml.Serialization;
using System.Collections.Generic;
using System.Windows.Media.Imaging;
using System.Reflection;
using System.Windows.Interop;
using Cartoonizer.ToolsLib.Tools.Properties;
using Cartoonizer.ToolsLib.Tools;
using System.Globalization;
using System.Windows.Markup;
using System.Xml;
using Cartoonizer.ToolsLib.Desginer;
using System.Linq;

namespace Cartoonizer.ToolsLib
{
    /// <summary>
    /// Canvas used as host for DrawingVisual objects.
    /// Allows to draw graphics objects using mouse.
    /// </summary>
    public class DrawingCanvas : Canvas
    {
        #region Class Members

        // Collection contains instances of GraphicsBase-derived classes.
        private VisualCollection graphicsList;


        // Dependency properties
        public static readonly DependencyProperty ToolProperty;
        public static readonly DependencyProperty ActualScaleProperty;
        public static readonly DependencyProperty IsDirtyProperty;

        public static readonly DependencyProperty LineWidthProperty;
        public static readonly DependencyProperty ObjectColorProperty;

        public static readonly DependencyProperty TextFontFamilyNameProperty;
        public static readonly DependencyProperty TextFontStyleProperty;
        public static readonly DependencyProperty TextFontWeightProperty;
        public static readonly DependencyProperty TextFontStretchProperty;
        public static readonly DependencyProperty TextFontSizeProperty;

        public static readonly DependencyProperty CanUndoProperty;
        public static readonly DependencyProperty CanRedoProperty;

        public static readonly DependencyProperty CanSelectAllProperty;
        public static readonly DependencyProperty CanUnselectAllProperty;
        public static readonly DependencyProperty CanDeleteProperty;
        public static readonly DependencyProperty CanDeleteAllProperty;
        public static readonly DependencyProperty CanMoveToFrontProperty;
        public static readonly DependencyProperty CanMoveToBackProperty;
        public static readonly DependencyProperty CanSetPropertiesProperty;

        public static readonly RoutedEvent IsDirtyChangedEvent;
                

        private Tool[] tools;                   // Array of tools

        //ToolText toolText;
        ToolPointer toolPointer;

        private ContextMenu contextMenu;

        private UndoManager undoManager;


        #endregion Class Members

        #region Constructors

        public DrawingCanvas()
            : base()
        {
            graphicsList = new VisualCollection(this);

            CreateContextMenu();

            // create array of drawing tools
            tools = new Tool[(int)ToolType.Max];

            toolPointer = new ToolPointer();
            tools[(int)ToolType.Pointer] = toolPointer;

            tools[(int)ToolType.Rectangle] = new ToolRectangle();
            tools[(int)ToolType.Ellipse] = new ToolEllipse();
            tools[(int)ToolType.Line] = new ToolLine();
            tools[(int)ToolType.PolyLine] = new ToolPolyLine();

            //toolText = new ToolText(this);
            //tools[(int)ToolType.Text] = toolText;   // kept as class member for in-place editing

            tools[(int)ToolType.Marker] = new ToolMarker();
            tools[(int)ToolType.Arrow] = new ToolLine();
            tools[(int)ToolType.WaterMark] = new ToolRectangle();
            tools[(int)ToolType.Crop] = new ToolCrop();

            // Create undo manager
            undoManager = new UndoManager(this);
            undoManager.StateChanged += new EventHandler(undoManager_StateChanged);


            this.FocusVisualStyle = null;

            this.Loaded += new RoutedEventHandler(DrawingCanvas_Loaded);
            this.MouseDown += new MouseButtonEventHandler(DrawingCanvas_MouseDown);
            this.MouseMove += new MouseEventHandler(DrawingCanvas_MouseMove);
            this.MouseUp += new MouseButtonEventHandler(DrawingCanvas_MouseUp);

            this.KeyDown += new KeyEventHandler(DrawingCanvas_KeyDown);
            this.LostMouseCapture += new MouseEventHandler(DrawingCanvas_LostMouseCapture);
        }


        static DrawingCanvas()
        {
            // **********************************************************
            // Create dependency properties
            // **********************************************************

            PropertyMetadata metaData;

            // Tool
            metaData = new PropertyMetadata(ToolType.Pointer);

            ToolProperty = DependencyProperty.Register(
                "Tool", typeof(ToolType), typeof(DrawingCanvas),
                metaData);


            // ActualScale
            metaData = new PropertyMetadata(
                1.0,                                                        // default value
                new PropertyChangedCallback(ActualScaleChanged));           // change callback

            ActualScaleProperty = DependencyProperty.Register(
                "ActualScale", typeof(double), typeof(DrawingCanvas),
                metaData);

            // IsDirty
            metaData = new PropertyMetadata(false);

            IsDirtyProperty = DependencyProperty.Register(
                "IsDirty", typeof(bool), typeof(DrawingCanvas),
                metaData);

            // LineWidth
            metaData = new PropertyMetadata(
                1.0,
                new PropertyChangedCallback(LineWidthChanged));

            LineWidthProperty = DependencyProperty.Register(
                "LineWidth", typeof(double), typeof(DrawingCanvas),
                metaData);

            // ObjectColor
            metaData = new PropertyMetadata(
                Colors.Black,
                new PropertyChangedCallback(ObjectColorChanged));

            ObjectColorProperty = DependencyProperty.Register(
                "ObjectColor", typeof(Color), typeof(DrawingCanvas),
                metaData);


            // TextFontFamilyName
            metaData = new PropertyMetadata(
                Properties.Settings.Default.DefaultFontFamily,
                new PropertyChangedCallback(TextFontFamilyNameChanged));

            TextFontFamilyNameProperty = DependencyProperty.Register(
                "TextFontFamilyName", typeof(string), typeof(DrawingCanvas),
                metaData);

            // TextFontStyle
            metaData = new PropertyMetadata(
                FontStyles.Normal,
                new PropertyChangedCallback(TextFontStyleChanged));

            TextFontStyleProperty = DependencyProperty.Register(
                "TextFontStyle", typeof(FontStyle), typeof(DrawingCanvas),
                metaData);

            // TextFontWeight
            metaData = new PropertyMetadata(
                FontWeights.Normal,
                new PropertyChangedCallback(TextFontWeightChanged));

            TextFontWeightProperty = DependencyProperty.Register(
                "TextFontWeight", typeof(FontWeight), typeof(DrawingCanvas),
                metaData);

            // TextFontStretch
            metaData = new PropertyMetadata(
                FontStretches.Normal,
                new PropertyChangedCallback(TextFontStretchChanged));

            TextFontStretchProperty = DependencyProperty.Register(
                "TextFontStretch", typeof(FontStretch), typeof(DrawingCanvas),
                metaData);

            // TextFontSize
            metaData = new PropertyMetadata(
                12.0,
                new PropertyChangedCallback(TextFontSizeChanged));

            TextFontSizeProperty = DependencyProperty.Register(
                "TextFontSize", typeof(double), typeof(DrawingCanvas),
                metaData);

            // CanUndo
            metaData = new PropertyMetadata(false);

            CanUndoProperty = DependencyProperty.Register(
                "CanUndo", typeof(bool), typeof(DrawingCanvas),
                metaData);

            // CanRedo
            metaData = new PropertyMetadata(false);

            CanRedoProperty = DependencyProperty.Register(
                "CanRedo", typeof(bool), typeof(DrawingCanvas),
                metaData);

            // CanSelectAll
            metaData = new PropertyMetadata(false);

            CanSelectAllProperty = DependencyProperty.Register(
                "CanSelectAll", typeof(bool), typeof(DrawingCanvas),
                metaData);

            // CanUnselectAll
            metaData = new PropertyMetadata(false);

            CanUnselectAllProperty = DependencyProperty.Register(
                "CanUnselectAll", typeof(bool), typeof(DrawingCanvas),
                metaData);

            // CanDelete
            metaData = new PropertyMetadata(false);

            CanDeleteProperty = DependencyProperty.Register(
                "CanDelete", typeof(bool), typeof(DrawingCanvas),
                metaData);

            // CanDeleteAll
            metaData = new PropertyMetadata(false);

            CanDeleteAllProperty = DependencyProperty.Register(
                "CanDeleteAll", typeof(bool), typeof(DrawingCanvas),
                metaData);

            // CanMoveToFront
            metaData = new PropertyMetadata(false);

            CanMoveToFrontProperty = DependencyProperty.Register(
                "CanMoveToFront", typeof(bool), typeof(DrawingCanvas),
                metaData);

            // CanMoveToBack
            metaData = new PropertyMetadata(false);

            CanMoveToBackProperty = DependencyProperty.Register(
                "CanMoveToBack", typeof(bool), typeof(DrawingCanvas),
                metaData);

            // CanSetProperties
            metaData = new PropertyMetadata(false);

            CanSetPropertiesProperty = DependencyProperty.Register(
                "CanSetProperties", typeof(bool), typeof(DrawingCanvas),
                metaData);

            // **********************************************************
            // Create routed events
            // **********************************************************

            // IsDirtyChanged

            IsDirtyChangedEvent = EventManager.RegisterRoutedEvent("IsDirtyChangedChanged",
                RoutingStrategy.Bubble, typeof(DependencyPropertyChangedEventHandler), typeof(DrawingCanvas));

        }

        #endregion Constructor

        #region Dependency Properties

        #region Tool

        /// <summary>
        /// Currently active drawing tool
        /// </summary>
        public ToolType Tool
        {
            get
            {
                return (ToolType)GetValue(ToolProperty);
            }
            set
            {
                ToolType oldValue = (ToolType)GetValue(ToolProperty);
                if ((int)value >= 0 && (int)value < (int)ToolType.Max)
                {
                    if (value != ToolType.Pointer)
                    {
                        foreach (var g in this.GraphicsList)
                        {
                            if (g is GraphicsCrop)
                            {
                                MessageBoxResult result = MessageBox.Show("Crop the Image ?", "Cartoonizer",
                                    MessageBoxButton.YesNoCancel, MessageBoxImage.Question);

                                if (result == MessageBoxResult.Cancel)
                                    return;
                                else if (result == MessageBoxResult.No)
                                {
                                    this.GraphicsList.Remove(g);
                                    return;
                                }
                                else if (result == MessageBoxResult.Yes)
                                {
                                    ApplyCrop((GraphicsCrop)g);
                                    break;
                                }
                            }

                        }
                    }
                    SetValue(ToolProperty, value);

                    // Set cursor immediately - important when tool is selected from the menu
                    tools[(int)Tool].SetCursor(this);

                    RoutedPropertyChangedEventArgs<ToolType> newEventArgs =
                        new RoutedPropertyChangedEventArgs<ToolType>(oldValue, value);
                    newEventArgs.RoutedEvent = SelectedToolChangedEvent;


                    this.RaiseEvent(newEventArgs);// SelectedToolChanged.Invoke(this, newEventArgs);

                }
            }
        }

        public static readonly RoutedEvent SelectedToolChangedEvent = EventManager.RegisterRoutedEvent(
            "SelectedToolChanged",
            RoutingStrategy.Bubble,
            typeof(RoutedPropertyChangedEventHandler<ToolType>),
            typeof(DrawingCanvas)
        );

        public event RoutedPropertyChangedEventHandler<ToolType> SelectedToolChanged
        {
            add { AddHandler(SelectedToolChangedEvent, value); }
            remove { RemoveHandler(SelectedToolChangedEvent, value); }
        }

        #endregion Tool

        #region ActualScale

        /// <summary>
        /// Dependency property ActualScale.
        /// </summary>
        public double ActualScale
        {
            get
            {
                return (double)GetValue(ActualScaleProperty);
            }
            set
            {
                SetValue(ActualScaleProperty, value);
                this.LayoutTransform = new ScaleTransform(value, value);

            }
        }

        /// <summary>
        /// Callback function called when ActualScale dependency property is changed.
        /// </summary>
        static void ActualScaleChanged(DependencyObject property, DependencyPropertyChangedEventArgs args)
        {
            DrawingCanvas d = property as DrawingCanvas;

            double scale = d.ActualScale;

            foreach (GraphicsBase b in d.GraphicsList)
            {
                b.ActualScale = scale;
            }
        }

        #endregion ActualScale

        #region IsDirty

        /// <summary>
        /// Returns true if document is changed
        /// </summary>
        public bool IsDirty
        {
            get
            {
                return (bool)GetValue(IsDirtyProperty);
            }
            internal set
            {
                SetValue(IsDirtyProperty, value);

                // Raise IsDirtyChanged event.
                RoutedEventArgs newargs = new RoutedEventArgs(IsDirtyChangedEvent);
                RaiseEvent(newargs);
            }
        }

        #endregion IsDirty

        #region CanUndo

        /// <summary>
        /// Return True if Undo operation is possible
        /// </summary>
        public bool CanUndo
        {
            get
            {
                return (bool)GetValue(CanUndoProperty);
            }
            internal set
            {
                SetValue(CanUndoProperty, value);
            }
        }

        #endregion CanUndo

        #region CanRedo

        /// <summary>
        /// Return True if Redo operation is possible
        /// </summary>
        public bool CanRedo
        {
            get
            {
                return (bool)GetValue(CanRedoProperty);
            }
            internal set
            {
                SetValue(CanRedoProperty, value);
            }
        }
                
        #endregion CanRedo

        #region CanSelectAll

        /// <summary>
        /// Return true if Select All function is available
        /// </summary>
        public bool CanSelectAll
        {
            get
            {
                return (bool)GetValue(CanSelectAllProperty);
            }
            internal set
            {
                SetValue(CanSelectAllProperty, value);
            }
        }

        #endregion CanSelectAll

        #region CanUnselectAll

        /// <summary>
        /// Return true if Unselect All function is available
        /// </summary>
        public bool CanUnselectAll
        {
            get
            {
                return (bool)GetValue(CanUnselectAllProperty);
            }
            internal set
            {
                SetValue(CanUnselectAllProperty, value);
            }
        }

        #endregion CanUnselectAll

        #region CanDelete

        /// <summary>
        /// Return true if Delete function is available
        /// </summary>
        public bool CanDelete
        {
            get
            {
                return (bool)GetValue(CanDeleteProperty);
            }
            internal set
            {
                SetValue(CanDeleteProperty, value);
            }
        }

        #endregion CanDelete

        #region CanDeleteAll

        /// <summary>
        /// Return true if Delete All function is available
        /// </summary>
        public bool CanDeleteAll
        {
            get
            {
                return (bool)GetValue(CanDeleteAllProperty);
            }
            internal set
            {
                SetValue(CanDeleteAllProperty, value);
            }
        }

        #endregion CanDeleteAll

        #region CanMoveToFront

        /// <summary>
        /// Return true if Move to Front function is available
        /// </summary>
        public bool CanMoveToFront
        {
            get
            {
                return (bool)GetValue(CanMoveToFrontProperty);
            }
            internal set
            {
                SetValue(CanMoveToFrontProperty, value);
            }
        }

        #endregion CanMoveToFront

        #region CanMoveToBack

        /// <summary>
        /// Return true if Move to Back function is available
        /// </summary>
        public bool CanMoveToBack
        {
            get
            {
                return (bool)GetValue(CanMoveToBackProperty);
            }
            internal set
            {
                SetValue(CanMoveToBackProperty, value);
            }
        }

        #endregion CanMoveToBack

        #region CanSetProperties

        /// <summary>
        /// Return true if currently active properties (line width, color etc.)
        /// can be applied to selected objects.
        /// </summary>
        public bool CanSetProperties
        {
            get
            {
                return (bool)GetValue(CanSetPropertiesProperty);
            }
            internal set
            {
                SetValue(CanSetPropertiesProperty, value);
            }
        }

        #endregion CanSetProperties

        #region LineWidth

        /// <summary>
        /// Line width of new graphics object.
        /// Setting this property is also applied to current selection.
        /// </summary>
        public double LineWidth
        {
            get
            {
                return (double)GetValue(LineWidthProperty);
            }
            set
            {
                SetValue(LineWidthProperty, value);

            }
        }

        /// <summary>
        /// Callback function called when LineWidth dependency property is changed
        /// </summary>
        static void LineWidthChanged(DependencyObject property, DependencyPropertyChangedEventArgs args)
        {
            DrawingCanvas d = property as DrawingCanvas;

            HelperFunctions.ApplyLineWidth(d, d.LineWidth, true);
        }

        #endregion LineWidth

        #region ObjectColor

        /// <summary>
        /// Color of new graphics object.
        /// Setting this property is also applied to current selection.
        /// </summary>
        public Color ObjectColor
        {
            get
            {
                return (Color)GetValue(ObjectColorProperty);
            }
            set
            {
                SetValue(ObjectColorProperty, value);

            }
        }

        /// <summary>
        /// Callback function called when ObjectColor dependency property is changed
        /// </summary>
        static void ObjectColorChanged(DependencyObject property, DependencyPropertyChangedEventArgs args)
        {
            DrawingCanvas d = property as DrawingCanvas;

            HelperFunctions.ApplyColor(d, d.ObjectColor, true);
        }

        #endregion ObjectColor

        #region TextFontFamilyName

        /// <summary>
        /// Font Family name of new graphics object.
        /// Setting this property is also applied to current selection.
        /// </summary>
        public string TextFontFamilyName
        {
            get
            {
                return (string)GetValue(TextFontFamilyNameProperty);
            }
            set
            {
                SetValue(TextFontFamilyNameProperty, value);

            }
        }

        /// <summary>
        /// Callback function called when TextFontFamilyName dependency property is changed
        /// </summary>
        static void TextFontFamilyNameChanged(DependencyObject property, DependencyPropertyChangedEventArgs args)
        {
            DrawingCanvas d = property as DrawingCanvas;

            HelperFunctions.ApplyFontFamily(d, d.TextFontFamilyName, true);
        }

        #endregion TextFontFamilyName

        #region TextFontStyle

        /// <summary>
        /// Font style of new graphics object.
        /// Setting this property is also applied to current selection.
        /// </summary>
        public FontStyle TextFontStyle
        {
            get
            {
                return (FontStyle)GetValue(TextFontStyleProperty);
            }
            set
            {
                SetValue(TextFontStyleProperty, value);

            }
        }

        /// <summary>
        /// Callback function called when TextFontStyle dependency property is changed
        /// </summary>
        static void TextFontStyleChanged(DependencyObject property, DependencyPropertyChangedEventArgs args)
        {
            DrawingCanvas d = property as DrawingCanvas;

            HelperFunctions.ApplyFontStyle(d, d.TextFontStyle, true);
        }

        #endregion TextFontStyle

        #region TextFontWeight

        /// <summary>
        /// Font weight of new graphics object.
        /// Setting this property is also applied to current selection.
        /// </summary>
        public FontWeight TextFontWeight
        {
            get
            {
                return (FontWeight)GetValue(TextFontWeightProperty);
            }
            set
            {
                SetValue(TextFontWeightProperty, value);

            }
        }

        /// <summary>
        /// Callback function called when TextFontWeight dependency property is changed
        /// </summary>
        static void TextFontWeightChanged(DependencyObject property, DependencyPropertyChangedEventArgs args)
        {
            DrawingCanvas d = property as DrawingCanvas;

            HelperFunctions.ApplyFontWeight(d, d.TextFontWeight, true);
        }

        #endregion TextFontWeight

        #region TextFontStretch

        /// <summary>
        /// Font stretch of new graphics object.
        /// Setting this property is also applied to current selection.
        /// </summary>
        public FontStretch TextFontStretch
        {
            get
            {
                return (FontStretch)GetValue(TextFontStretchProperty);
            }
            set
            {
                SetValue(TextFontStretchProperty, value);

            }
        }

        /// <summary>
        /// Callback function called when TextFontStretch dependency property is changed
        /// </summary>
        static void TextFontStretchChanged(DependencyObject property, DependencyPropertyChangedEventArgs args)
        {
            DrawingCanvas d = property as DrawingCanvas;

            HelperFunctions.ApplyFontStretch(d, d.TextFontStretch, true);
        }

        #endregion TextFontStretch

        #region TextFontSize

        /// <summary>
        /// Font size of new graphics object.
        /// Setting this property is also applied to current selection.
        /// </summary>
        public double TextFontSize
        {
            get
            {
                return (double)GetValue(TextFontSizeProperty);
            }
            set
            {
                SetValue(TextFontSizeProperty, value);

            }
        }

        /// <summary>
        /// Callback function called when TextFontSize dependency property is changed
        /// </summary>
        static void TextFontSizeChanged(DependencyObject property, DependencyPropertyChangedEventArgs args)
        {
            DrawingCanvas d = property as DrawingCanvas;

            HelperFunctions.ApplyFontSize(d, d.TextFontSize, true);
        }

        #endregion TextFontSize

        public Action<System.Drawing.Rectangle> CroppedImageCompleted { get; set; }

        #endregion Dependency Properties

        #region Routed Events

        /// <summary>
        /// IsDirtyChanged event.
        /// 
        /// If client binds to IsDirty property, this event is not required.
        /// But if client knows when IsDirty changed without binding, 
        /// IsDirtyChanged is needed.
        /// </summary>
        public event RoutedEventHandler IsDirtyChanged
        {
            add { AddHandler(IsDirtyChangedEvent, value); }
            remove { RemoveHandler(IsDirtyChangedEvent, value); }
        }

        #endregion Routed Events

        #region Public Functions

        /// <summary>
        /// Return list of graphic objects.
        /// Used if client program needs to make its own usage of
        /// graphics objects, like save them in some persistent storage.
        /// </summary>
        public PropertiesGraphicsBase[] GetListOfGraphicObjects()
        {
            PropertiesGraphicsBase[] result = new PropertiesGraphicsBase[graphicsList.Count];

            int i = 0;

            foreach (GraphicsBase g in graphicsList)
            {
                result[i++] = g.CreateSerializedObject();
            }

            return result;
        }

        /// <summary>
        /// Draw all graphics objects to DrawingContext supplied by client.
        /// Can be used for printing or saving image together with graphics
        /// as single bitmap.
        /// 
        /// Selection tracker is not drawn.
        /// </summary>
        public void Draw(DrawingContext drawingContext)
        {
            Draw(drawingContext, false);
        }

        protected override void OnRender(DrawingContext dc)
        {
            base.OnRender(dc);
        }
        /// <summary>
        /// Draw all graphics objects to DrawingContext supplied by client.
        /// Can be used for printing or saving image together with graphics
        /// as single bitmap.
        /// 
        /// withSelection = true - draw selected objects with tracker.
        /// </summary>
        public void Draw(DrawingContext drawingContext, bool withSelection)
        {
            bool oldSelection = false;

            drawingContext.PushTransform(new ScaleTransform(ActualScale, ActualScale));

            foreach (GraphicsBase b in graphicsList)
            {
                if (!withSelection)
                {
                    // Keep selection state and unselect
                    oldSelection = b.IsSelected;
                    b.IsSelected = false;
                }

                b.Draw(drawingContext);

                if (!withSelection)
                {
                    // Restore selection state
                    b.IsSelected = oldSelection;
                }
            }
        }


        /// <summary>
        /// Clear graphics list
        /// </summary>
        public void Clear()
        {
            graphicsList.Clear();
            ClearHistory();
            UpdateState();
        }

        /// <summary>
        /// Save graphics to XML file.
        /// Throws: DrawingCanvasException.
        /// </summary>
        public void Save(string fileName)
        {
            try
            {
                SerializationHelper helper = new SerializationHelper(graphicsList);

                XmlSerializer xml = new XmlSerializer(typeof(SerializationHelper));

                using (Stream stream = new FileStream(fileName,
                    FileMode.Create, FileAccess.Write, FileShare.None))
                {
                    xml.Serialize(stream, helper);
                    ClearHistory();
                    UpdateState();
                }
            }
            catch (IOException e)
            {
                throw new DrawingCanvasException(e.Message, e);
            }
            catch (InvalidOperationException e)
            {
                throw new DrawingCanvasException(e.Message, e);
            }
        }

        /// <summary>
        /// Load graphics from XML file.
        /// Throws: DrawingCanvasException.
        /// </summary>
        public void Load(string fileName)
        {
            try
            {
                SerializationHelper helper;

                XmlSerializer xml = new XmlSerializer(typeof(SerializationHelper));

                using (Stream stream = new FileStream(fileName,
                    FileMode.Open, FileAccess.Read, FileShare.Read))
                {
                    helper = (SerializationHelper)xml.Deserialize(stream);
                }

                if (helper.Graphics == null)
                {
                    throw new DrawingCanvasException(Properties.Settings.Default.NoInfoInXMLFile);
                }

                graphicsList.Clear();

                foreach (PropertiesGraphicsBase g in helper.Graphics)
                {
                    graphicsList.Add(g.CreateGraphics());
                }

                // Update clip for all loaded objects.
                RefreshClip();

                ClearHistory();
                UpdateState();
            }
            catch (IOException e)
            {
                throw new DrawingCanvasException(e.Message, e);
            }
            catch (InvalidOperationException e)
            {
                throw new DrawingCanvasException(e.Message, e);
            }
            catch (ArgumentNullException e)
            {
                throw new DrawingCanvasException(e.Message, e);
            }
        }


        /// <summary>
        /// Select all
        /// </summary>
        public void SelectAll()
        {
            HelperFunctions.SelectAll(this);
            UpdateState();
        }

        /// <summary>
        /// Unselect all
        /// </summary>
        public void UnselectAll()
        {
            HelperFunctions.UnselectAll(this);
            UpdateState();
        }

        /// <summary>
        /// Delete selection
        /// </summary>
        public void Delete()
        {
            HelperFunctions.DeleteSelection(this);
            UpdateState();
        }

        /// <summary>
        /// Delete all
        /// </summary>
        public void DeleteAll()
        {
            HelperFunctions.DeleteAll(this);
            UpdateState();
        }

        /// <summary>
        /// Move selection to the front of Z-order
        /// </summary>
        public void MoveToFront()
        {
            HelperFunctions.MoveSelectionToFront(this);
            UpdateState();
        }

        /// <summary>
        /// Move selection to the end of Z-order
        /// </summary>
        public void MoveToBack()
        {
            HelperFunctions.MoveSelectionToBack(this);
            UpdateState();
        }

        /// <summary>
        /// Apply currently active properties to selected objects
        /// </summary>
        public void SetProperties()
        {
            HelperFunctions.ApplyProperties(this);
            UpdateState();
        }


        /// <summary>
        /// Set clip for all graphics objects.
        /// </summary>
        public void RefreshClip()
        {
            foreach (GraphicsBase b in graphicsList)
            {
                b.Clip = new RectangleGeometry(new Rect(0, 0, this.ActualWidth, this.ActualHeight));

                // Good chance to refresh actual scale
                b.ActualScale = this.ActualScale;
            }
        }

        /// <summary>
        /// Remove clip for all graphics objects.
        /// </summary>
        public void RemoveClip()
        {
            foreach (GraphicsBase b in graphicsList)
            {
                b.Clip = null;
            }
        }

        /// <summary>
        /// Undo
        /// </summary>
        public void Undo()
        {
            undoManager.Undo();
            UpdateState();
        }

        /// <summary>
        /// Redo
        /// </summary>
        public void Redo()
        {
            undoManager.Redo();
            UpdateState();
        }

        #endregion Public Functions

        #region Internal Properties

        /// <summary>
        /// Get graphic object by index
        /// </summary>
        internal GraphicsBase this[int index]
        {
            get
            {
                if (index >= 0 && index < Count)
                {
                    return (GraphicsBase)graphicsList[index];
                }

                return null;
            }
        }

        /// <summary>
        /// Get number of graphic objects
        /// </summary>
        internal int Count
        {
            get
            {
                return graphicsList.Count;
            }
        }

        /// <summary>
        /// Get number of selected graphic objects
        /// </summary>
        internal int SelectionCount
        {
            get
            {
                int n = 0;

                foreach (GraphicsBase g in this.graphicsList)
                {
                    if (g.IsSelected)
                    {
                        n++;
                    }
                }

                return n;
            }
        }

        /// <summary>
        /// Return list of graphics
        /// </summary>
        public VisualCollection GraphicsList
        {
            get
            {
                return graphicsList;
            }
        }

        /// <summary>
        /// Returns INumerable which may be used for enumeration
        /// of selected objects.
        /// </summary>
        internal IEnumerable<GraphicsBase> Selection
        {
            get
            {
                foreach (GraphicsBase o in graphicsList)
                {
                    if (o.IsSelected)
                    {
                        yield return o;
                    }
                }
            }

        }

        #endregion Internal Properties

        #region Visual Children Overrides

        /// <summary>
        /// Get number of children: VisualCollection count.
        /// If in-place editing textbox is active, add 1.
        /// </summary>
        protected override int VisualChildrenCount
        {
            get
            {
                int n = graphicsList.Count;

                //if (toolText.TextBox != null)
                //{
                //    n++;
                //}

                return n;
            }
        }

        /// <summary>
        /// Get visual child - one of GraphicsBase objects
        /// or in-place editing textbox, if it is active.
        /// </summary>
        protected override Visual GetVisualChild(int index)
        {
            if (index < 0 || index >= graphicsList.Count)
            {
                //if (toolText.TextBox != null && index == graphicsList.Count)
                //{
                //    return toolText.TextBox;
                //}

                throw new ArgumentOutOfRangeException("index");
            }

            return graphicsList[index];
        }

        #endregion Visual Children Overrides

        #region Mouse Event Handlers

        /// <summary>
        /// Mouse down.
        /// Left button down event is passed to active tool.
        /// Right button down event is handled in this class.
        /// </summary>
        void DrawingCanvas_MouseDown(object sender, MouseButtonEventArgs e)
        {
            if (tools[(int)Tool] == null)
            {
                return;
            }


            this.Focus();


            if (e.ChangedButton == MouseButton.Left)
            {
                if (e.ClickCount == 2)
                {
                    HandleDoubleClick(e);        // special case for GraphicsText
                }
                else
                {
                    tools[(int)Tool].OnMouseDown(this, e);
                }

                UpdateState();
            }
            else if (e.ChangedButton == MouseButton.Right)
            {
                ShowContextMenu(e);
            }
        }

        /// <summary>
        /// Mouse move.
        /// Moving without button pressed or with left button pressed
        /// is passed to active tool.
        /// </summary>
        void DrawingCanvas_MouseMove(object sender, MouseEventArgs e)
        {
            if (tools[(int)Tool] == null)
            {
                return;
            }

            if (e.MiddleButton == MouseButtonState.Released && e.RightButton == MouseButtonState.Released)
            {
                tools[(int)Tool].OnMouseMove(this, e);

                UpdateState();
            }
            else
            {
                this.Cursor = HelperFunctions.DefaultCursor;
            }
        }

        /// <summary>
        /// Mouse up event.
        /// Left button up event is passed to active tool.
        /// </summary>
        void DrawingCanvas_MouseUp(object sender, MouseButtonEventArgs e)
        {
            if (tools[(int)Tool] == null)
            {
                return;
            }


            if (e.ChangedButton == MouseButton.Left)
            {
                tools[(int)Tool].OnMouseUp(this, e);

                UpdateState();
            }
        }

        #endregion Mouse Event Handlers

        #region Other Event Handlers

        /// <summary>
        /// Initialization after control is loaded
        /// </summary>
        void DrawingCanvas_Loaded(object sender, RoutedEventArgs e)
        {
            this.Focusable = true;      // to handle keyboard messages
        }

        /// <summary>
        /// Context menu item is clicked
        /// </summary>
        void contextMenuItem_Click(object sender, RoutedEventArgs e)
        {
            MenuItem item = sender as MenuItem;

            if (item == null)
            {
                return;
            }

            ContextMenuCommand command = (ContextMenuCommand)item.Tag;

            switch (command)
            {
                case ContextMenuCommand.SelectAll:
                    SelectAll();
                    break;
                case ContextMenuCommand.UnselectAll:
                    UnselectAll();
                    break;
                case ContextMenuCommand.Delete:
                    Delete();
                    break;
                case ContextMenuCommand.DeleteAll:
                    DeleteAll();
                    break;
                case ContextMenuCommand.MoveToFront:
                    MoveToFront();
                    break;
                case ContextMenuCommand.MoveToBack:
                    MoveToBack();
                    break;
                case ContextMenuCommand.Undo:
                    Undo();
                    break;
                case ContextMenuCommand.Redo:
                    Redo();
                    break;
                case ContextMenuCommand.SerProperties:
                    SetProperties();
                    break;
            }
        }


        /// <summary>
        /// Mouse capture is lost
        /// </summary>
        void DrawingCanvas_LostMouseCapture(object sender, MouseEventArgs e)
        {
            if (this.IsMouseCaptured)
            {
                CancelCurrentOperation();
                UpdateState();
            }
        }

        /// <summary>
        /// Handle keyboard input
        /// </summary>
        void DrawingCanvas_KeyDown(object sender, KeyEventArgs e)
        {
            // Esc key stops currently active operation
            if (e.Key == Key.Escape)
            {
                if (this.IsMouseCaptured)
                {
                    CancelCurrentOperation();
                    UpdateState();
                }
            }
        }

        /// <summary>
        /// UndoManager state is changed.
        /// Refresh CanUndo, CanRedo and IsDirty properties.
        /// </summary>
        void undoManager_StateChanged(object sender, EventArgs e)
        {
            this.CanUndo = undoManager.CanUndo;
            this.CanRedo = undoManager.CanRedo;

            // Set IsDirty only if it is actually changed.
            // Setting IsDirty raises event for client.
            if (undoManager.IsDirty != this.IsDirty)
            {
                this.IsDirty = undoManager.IsDirty;
            }
        }


        #endregion Other Event Handlers

        #region Other Functions

        /// <summary>
        /// Create context menu
        /// </summary>
        void CreateContextMenu()
        {
            MenuItem menuItem;

            contextMenu = new ContextMenu();

            menuItem = new MenuItem();
            menuItem.Header = "Select All";
            menuItem.Tag = ContextMenuCommand.SelectAll;
            menuItem.Click += new RoutedEventHandler(contextMenuItem_Click);
            contextMenu.Items.Add(menuItem);

            menuItem = new MenuItem();
            menuItem.Header = "Unselect All";
            menuItem.Tag = ContextMenuCommand.UnselectAll;
            menuItem.Click += new RoutedEventHandler(contextMenuItem_Click);
            contextMenu.Items.Add(menuItem);

            menuItem = new MenuItem();
            menuItem.Header = "Delete";
            menuItem.Tag = ContextMenuCommand.Delete;
            menuItem.Click += new RoutedEventHandler(contextMenuItem_Click);
            contextMenu.Items.Add(menuItem);

            menuItem = new MenuItem();
            menuItem.Header = "Delete All";
            menuItem.Tag = ContextMenuCommand.DeleteAll;
            menuItem.Click += new RoutedEventHandler(contextMenuItem_Click);
            contextMenu.Items.Add(menuItem);

            contextMenu.Items.Add(new Separator());

            menuItem = new MenuItem();
            menuItem.Header = "Move to Front";
            menuItem.Tag = ContextMenuCommand.MoveToFront;
            menuItem.Click += new RoutedEventHandler(contextMenuItem_Click);
            contextMenu.Items.Add(menuItem);

            menuItem = new MenuItem();
            menuItem.Header = "Move to Back";
            menuItem.Tag = ContextMenuCommand.MoveToBack;
            menuItem.Click += new RoutedEventHandler(contextMenuItem_Click);
            contextMenu.Items.Add(menuItem);

            contextMenu.Items.Add(new Separator());

            menuItem = new MenuItem();
            menuItem.Header = "Undo";
            menuItem.Tag = ContextMenuCommand.Undo;
            menuItem.Click += new RoutedEventHandler(contextMenuItem_Click);
            contextMenu.Items.Add(menuItem);

            menuItem = new MenuItem();
            menuItem.Header = "Redo";
            menuItem.Tag = ContextMenuCommand.Redo;
            menuItem.Click += new RoutedEventHandler(contextMenuItem_Click);
            contextMenu.Items.Add(menuItem);

            menuItem = new MenuItem();
            menuItem.Header = "Set Properties";
            menuItem.Tag = ContextMenuCommand.SerProperties;
            menuItem.Click += new RoutedEventHandler(contextMenuItem_Click);
            contextMenu.Items.Add(menuItem);
        }

        /// <summary>
        /// Show context menu.
        /// </summary>
        void ShowContextMenu(MouseButtonEventArgs e)
        {
            // Change current selection if necessary

            Point point = e.GetPosition(this);

            GraphicsBase o = null;

            for (int i = graphicsList.Count - 1; i >= 0; i--)
            {
                if (((GraphicsBase)graphicsList[i]).MakeHitTest(point) == 0)
                {
                    o = (GraphicsBase)graphicsList[i];
                    break;
                }
            }

            if (o != null)
            {
                if (!o.IsSelected)
                {
                    UnselectAll();
                }

                // Select clicked object
                o.IsSelected = true;
            }
            else
            {
                UnselectAll();
            }

            UpdateState();

            MenuItem item;

            /// Enable/disable menu items.
            foreach (object obj in contextMenu.Items)
            {
                item = obj as MenuItem;

                if (item != null)
                {
                    ContextMenuCommand command = (ContextMenuCommand)item.Tag;

                    switch (command)
                    {
                        case ContextMenuCommand.SelectAll:
                            item.IsEnabled = CanSelectAll;
                            break;
                        case ContextMenuCommand.UnselectAll:
                            item.IsEnabled = CanUnselectAll;
                            break;
                        case ContextMenuCommand.Delete:
                            item.IsEnabled = CanDelete;
                            break;
                        case ContextMenuCommand.DeleteAll:
                            item.IsEnabled = CanDeleteAll;
                            break;
                        case ContextMenuCommand.MoveToFront:
                            item.IsEnabled = CanMoveToFront;
                            break;
                        case ContextMenuCommand.MoveToBack:
                            item.IsEnabled = CanMoveToBack;
                            break;
                        case ContextMenuCommand.Undo:
                            item.IsEnabled = CanUndo;
                            break;
                        case ContextMenuCommand.Redo:
                            item.IsEnabled = CanRedo;
                            break;
                        case ContextMenuCommand.SerProperties:
                            item.IsEnabled = CanSetProperties;
                            break;
                    }
                }
            }

            contextMenu.IsOpen = true;
        }

        /// <summary>
        /// Cancel currently executed operation:
        /// add new object or group selection.
        /// 
        /// Called when mouse capture is lost or Esc is pressed.
        /// </summary>
        void CancelCurrentOperation()
        {
            if (Tool == ToolType.Pointer)
            {
                if (graphicsList.Count > 0)
                {
                    if (graphicsList[graphicsList.Count - 1] is GraphicsSelectionRectangle)
                    {
                        // Delete selection rectangle if it exists
                        graphicsList.RemoveAt(graphicsList.Count - 1);
                    }
                    else
                    {
                        // Pointer tool moved or resized graphics object.
                        // Add this action to the history
                        toolPointer.AddChangeToHistory(this);
                    }
                }
            }
            else if (Tool > ToolType.Pointer && Tool < ToolType.Max)
            {
                // Delete last graphics object which is currently drawn
                if (graphicsList.Count > 0)
                {
                    graphicsList.RemoveAt(graphicsList.Count - 1);
                }
            }

            Tool = ToolType.Pointer;

            this.ReleaseMouseCapture();
            this.Cursor = HelperFunctions.DefaultCursor;
        }

        /// <summary>
        /// Hide in-place editing textbox.
        /// Called from TextTool, when user pressed Enter or Esc,
        /// or from this class, when user clicks on the canvas.
        /// 
        /// graphicsText passed to this function can be new text added by
        /// ToolText, or existing text opened for editing.
        /// If ToolText.OldText is empty, this is new object.
        /// If not, this is existing object.
        /// </summary>
        internal void HideTextbox(GraphicsText graphicsText)
        {
            ////if (toolText.TextBox == null)
            ////{
            ////    return;
            ////}

            //graphicsText.IsSelected = true;   // restore selection which was removed for better textbox appearance


            //if (toolText.TextBox.Text.Trim().Length == 0)
            //{
            //    // Textbox is empty: remove text object.

            //    if (!String.IsNullOrEmpty(toolText.OldText))  // existing text was edited
            //    {
            //        // Since text object is removed now,
            //        // Add Delete command to the history
            //        undoManager.AddCommandToHistory(new CommandDelete(this));
            //    }

            //    // Remove empty text object
            //    graphicsList.Remove(graphicsText);
            //}
            //else
            //{
            //    if (!String.IsNullOrEmpty(toolText.OldText))  // existing text was edited
            //    {
            //    //    if (toolText.TextBox.Text.Trim() != toolText.OldText)     // text was really changed
            //    //    {
            //    //        // Create command
            //    //        CommandChangeState command = new CommandChangeState(this);

            //    //        // Make change in the text object
            //    //        graphicsText.Text = toolText.TextBox.Text.Trim();
            //    //        graphicsText.UpdateRectangle();

            //    //        // Keep state after change and add command to the history
            //    //        command.NewState(this);
            //    //        undoManager.AddCommandToHistory(command);
            //    //    }
            //    }
            //    else                                          // new text was added
            //    {
            //        // Make change in the text object
            //        //graphicsText.Text = toolText.TextBox.Text.Trim();
            //        graphicsText.UpdateRectangle();

            //        // Add command to the history
            //        undoManager.AddCommandToHistory(new CommandAdd(graphicsText));
            //    }
            //}

            //// Remove textbox and set it to null.
            ////this.Children.Remove(toolText.TextBox);
            ////toolText.TextBox = null;

            //// This enables back all ApplicationCommands,
            //// which are disabled while textbox is active.
            //this.Focus();
        }

        private static System.Drawing.Image CropImage(System.Drawing.Image img, System.Drawing.Rectangle cropArea)
        {
            System.Drawing.Bitmap bmpImage = new System.Drawing.Bitmap(img);
            System.Drawing.Bitmap bmpCrop = bmpImage.Clone(cropArea,
            bmpImage.PixelFormat);
            return (System.Drawing.Image)(bmpCrop);
        }

        /// <summary>
        /// Open in-place edit box if GraphicsText is clicked
        /// </summary>
        void HandleDoubleClick(MouseButtonEventArgs e)
        {
            Point point = e.GetPosition(this);

            // Enumerate all text objects
            for (int i = graphicsList.Count - 1; i >= 0; i--)
            {
                GraphicsCrop t = graphicsList[i] as GraphicsCrop;

                if (t != null)
                {
                    if (t.Contains(point))
                    {
                        ApplyCrop(t);
                        break;
                    }
                }
            }

            // Enumerate all text objects
            for (int i = graphicsList.Count - 1; i >= 0; i--)
            {
                GraphicsText t = graphicsList[i] as GraphicsText;

                if (t != null)
                {
                    if (t.Contains(point))
                    {
                        //toolText.CreateTextBox(t, this);
                        return;
                    }
                }
            }
        }

        public void ApplyCrop(GraphicsCrop t)
        {
            int top = (int)Math.Min(t.Top, t.Bottom);
            int bottom = (int)Math.Max(t.Top, t.Bottom);
            int left = (int)Math.Min(t.Right, t.Left);
            int right = (int)Math.Max(t.Right, t.Left);

            top = Math.Max(top, 0);
            bottom = Math.Min(bottom, (int)this.Height);

            left = Math.Max(left, 0);
            right = Math.Min(right, (int)this.Width);

            this.GraphicsList.Remove(t);

            var croppedRect = new System.Drawing.Rectangle(left, top, right - left, bottom - top);
            System.Drawing.Image cImage = CropImage(
                  this.ExportToImage(), croppedRect);


            string tempFile = Path.GetTempFileName();
            DeleteAll();
            SetBackground(cImage);
            CroppedImageCompleted?.Invoke(croppedRect);
        }

        /// <summary>
        /// Add command to history.
        /// </summary>
        internal void AddCommandToHistory(CommandBase command)
        {
            undoManager.AddCommandToHistory(command);
        }

        /// <summary>
        /// Clear Undo history.
        /// </summary>
        void ClearHistory()
        {
            undoManager.ClearHistory();
        }

        /// <summary>
        /// Update state of Can* dependency properties
        /// used for Edit commands.
        /// This function calls after any change in drawing canvas state,
        /// caused by user commands.
        /// Helps to keep client controls state up-to-date, in the case
        /// if Can* properties are used for binding.
        /// </summary>
        void UpdateState()
        {
            bool hasObjects = (this.Count > 0);
            bool hasSelectedObjects = (this.SelectionCount > 0);

            CanSelectAll = hasObjects;
            CanUnselectAll = hasObjects;
            CanDelete = hasSelectedObjects;
            CanDeleteAll = hasObjects;
            CanMoveToFront = hasSelectedObjects;
            CanMoveToBack = hasSelectedObjects;

            CanSetProperties = HelperFunctions.CanApplyProperties(this);
        }


        #endregion Other Functions

        public System.Drawing.Bitmap ExportToImage()
        {
            Dictionary<object, bool> status = new Dictionary<object, bool>();

            foreach (var item in this.GraphicsList)
            {
                GraphicsBase itemBase = item as GraphicsBase;
                if (itemBase != null)
                {
                    status.Add(itemBase, itemBase.IsSelected);
                    itemBase.IsSelected = false;
                }
            }

            System.Drawing.Bitmap i = null;
            // Save current canvas transform
            Transform transform = this.LayoutTransform;
            // reset current transform (in case it is scaled or rotated)
            this.LayoutTransform = null;

            if (double.IsNaN(this.Width) || double.IsNaN(this.Height))
                return null;
            // Get the size of canvas
            Size size = new Size(this.Width, this.Height);
            // Measure and arrange the surface
            // VERY IMPORTANT
            this.Measure(size);
            this.Arrange(new Rect(size));

            // Create a render bitmap and push the surface to it
            RenderTargetBitmap renderBitmap =
              new RenderTargetBitmap(
                (int)size.Width,
                (int)size.Height,
                96d,
                96d,
                PixelFormats.Pbgra32);
            renderBitmap.Render(this);

            // Create a file stream for saving image
            using (MemoryStream outStream = new MemoryStream())
            {
                // Use png encoder for our data
                PngBitmapEncoder encoder = new PngBitmapEncoder();
                // push the rendered bitmap to it
                encoder.Frames.Add(BitmapFrame.Create(renderBitmap));
                // save the data to the stream
                encoder.Save(outStream);

                i = (System.Drawing.Bitmap)System.Drawing.Bitmap.FromStream(outStream);
            }

            // Restore previously saved layout
            this.LayoutTransform = transform;

            foreach (var item in this.GraphicsList)
            {
                GraphicsBase itemBase = item as GraphicsBase;
                bool selected = false;
                if (status.TryGetValue(itemBase, out  selected))
                {
                    itemBase.IsSelected = selected;
                }
            }


            return i;
        }

        public string _fileName;
        
        public void SetBackground(string fileName)
        {
            if (File.Exists(fileName))
            {
                _fileName = fileName;
                System.Drawing.Image image = System.Drawing.Image.FromFile(fileName);


                SetBackground(image);
            }
        }

        private void SetBackground(System.Drawing.Image cImage)
        {
            BitmapSource bmImage = ConvertToBitmapSource(new System.Drawing.Bitmap(cImage));
            SetBackground(bmImage);

        }
        private void SetBackground(ImageSource image)
        {
            this.Background = new ImageBrush(image);
            this.Width = image.Width;
            this.Height = image.Height;
            BmsEngine.Init(GetImageSource());
            BmsEngine.Init(ExportToImage());

        }


        public static BitmapSource ConvertToBitmapSource(System.Drawing.Bitmap gdiPlusBitmap)
        {

            IntPtr hBitmap = gdiPlusBitmap.GetHbitmap();

            return Imaging.CreateBitmapSourceFromHBitmap(hBitmap, IntPtr.Zero, Int32Rect.Empty, BitmapSizeOptions.FromEmptyOptions());

        }

        public BitmapSource GetImageSource()
        {
            BitmapSource bmImage = null;
            var ima = ExportToImage();
            if (ima != null)
            {
                bmImage = ConvertToBitmapSource(new System.Drawing.Bitmap(ima));
            }
            return bmImage;
        }

        public void AddWatermark(Color color, double fontSize, double angle, string watermark)
        {
            //Set transperency to 50%
            color.A = 128;

            //get the orginal Image
            ImageBrush brush = this.Background as ImageBrush;

            // Create a DrawingGroup
            DrawingGroup dGroup = new DrawingGroup();

            // Obtain a DrawingContext from 
            // the DrawingGroup.
            using (DrawingContext dc = dGroup.Open())
            {
                double imageWidth = brush.ImageSource.Width;
                double imageHeigh = brush.ImageSource.Height;
                dc.DrawImage(brush.ImageSource, new Rect(0, 0,
                    imageWidth,
                    imageHeigh));

                FormattedText formatedText = new FormattedText(watermark, CultureInfo.CurrentCulture,
                      FlowDirection.LeftToRight,
                      new Typeface(new FontFamily(this.TextFontFamilyName),
                                  new FontStyle(), new FontWeight(), new FontStretch()), fontSize,
                                  new SolidColorBrush(color));

                double textLocX = imageWidth / 2;
                double textLocY = imageHeigh / 2;

                dc.PushTransform(new TranslateTransform(textLocX, textLocY));
                dc.PushTransform(new RotateTransform(-1 * angle));

                dc.DrawText(formatedText, new Point(formatedText.Width / -2, formatedText.Height / -2));

                dc.PushTransform(new RotateTransform(angle));
                dc.PushTransform(new TranslateTransform(-1 * textLocX, -1 * textLocY));

            }

            DrawingImage dImageSource = new DrawingImage(dGroup);
            SetBackground(dImageSource);
            this.Tool = ToolType.Pointer;
        }
        public enum RGB { Blue, Green, Red, BRIGTNESS };
        //public void AdjustColor(double rV, double gV, double bV)
        //{
        //    rV = (100.0 + rV) / 100.0;
        //    gV = (100.0 + gV) / 100.0;
        //    bV = (100.0 + bV) / 100.0;


        //    //get the orginal Image
        //    ImageBrush brush = this.Background as ImageBrush;

        //    byte[] orginal = BmsEngine.GetRgbData();
        //    if (orginal.Length == 0)
        //        return;
        //    byte[] newJpegBytes = new byte[orginal.Length];


        //    for (int i = 0; i < BmsEngine.dataLength; i += 4)
        //    {
        //        double pR = orginal[i + (int)RGB.Red] / 255.0;
        //        pR *= rV;
        //        pR *= 255;
        //        if (pR < 0) pR = 0;
        //        if (pR > 255) pR = 255;
        //        newJpegBytes[i + (int)RGB.Red] = (byte)pR;


        //        pR = orginal[i + (int)RGB.Green] / 255.0;
        //        pR *= gV;
        //        pR *= 255;
        //        if (pR < 0) pR = 0;
        //        if (pR > 255) pR = 255;
        //        newJpegBytes[i + (int)RGB.Green] = (byte)pR;

        //        pR = orginal[i + (int)RGB.Blue] / 255.0;
        //        pR *= bV;
        //        pR *= 255;
        //        if (pR < 0) pR = 0;
        //        if (pR > 255) pR = 255;
        //        newJpegBytes[i + (int)RGB.Blue] = (byte)pR;

        //        newJpegBytes[i + (int)RGB.BRIGTNESS] = orginal[i + (int)RGB.BRIGTNESS];


        //    }
        //    if (newJpegBytes.Length > 0)
        //    {
        //        BitmapSource redBms = BmsEngine.CloneBms(newJpegBytes);
        //        BmsEngine.SetDestination(newJpegBytes);
        //        brush.ImageSource = redBms;
        //    }
        //}

        public void AdjustBrightness(double brightness, double contrast, double intensity, double rV, double gV, double bV)
        {
            rV = (100.0 + rV) / 100.0;
            gV = (100.0 + gV) / 100.0;
            bV = (100.0 + bV) / 100.0;

            brightness = (100.0 + brightness) / 100.0;

            contrast = (100.0 + contrast) / 100.0;
            contrast *= contrast;

            //get the orginal Image
            ImageBrush brush = this.Background as ImageBrush;

            byte[] orginal = BmsEngine.GetRgbData();
            byte[] newJpegBytes = new byte[orginal.Length];


            for (int i = 0; i < BmsEngine.dataLength; i += 4)
            {
                double pR = orginal[i + (int)RGB.Red] / 255.0;
                //color
                pR *= rV;
                //brightness
                pR *= brightness;
                //contrast
                pR -= 0.5;
                pR *= contrast;
                pR += 0.5;
                pR *= 255;
                if (pR < 0) pR = 0;
                if (pR > 255) pR = 255;
                //save
                newJpegBytes[i + (int)RGB.Red] = (byte)pR;

                double pG = orginal[i + (int)RGB.Green] / 255.0;
                //color
                pG *= gV;
                //brightness
                pG *= brightness;
                //contrast
                pG -= 0.5;
                pG *= contrast;
                pG += 0.5;
                pG *= 255;
                if (pG < 0) pG = 0;
                if (pG > 255) pG = 255;
                //save
                newJpegBytes[i + (int)RGB.Green] = (byte)pG;

                double pB = orginal[i + (int)RGB.Blue] / 255.0;
                //color
                pB *= bV;
                //brightness
                pB *= brightness;
                //contrast
                pB -= 0.5;
                pB *= contrast;
                pB += 0.5;
                pB *= 255;
                if (pB < 0) pB = 0;
                if (pB > 255) pB = 255;
                //save
                newJpegBytes[i + (int)RGB.Blue] = (byte)pB;

                //double pI = orginal[i + (int)RGB.BRIGTNESS] / 255.0;
                //pI *= intensity;
                newJpegBytes[i + (int)RGB.BRIGTNESS] = (byte)intensity;


            }
            if (newJpegBytes.Length > 0)
            {
                BitmapSource redBms = BmsEngine.CloneBms(newJpegBytes);
                BmsEngine.SetDestination(newJpegBytes);
                brush.ImageSource = redBms;
            }
        }


        //public void AdjustContrast(double contrast, int lastSlide)
        //{
        //    contrast = (100.0 + contrast) / 100.0;
        //    contrast *= contrast;

        //    //get the orginal Image
        //    ImageBrush brush = this.Background as ImageBrush;

        //    byte[] orginal = BmsEngine.GetRgbData();
        //    byte[] newJpegBytes = new byte[orginal.Length];


        //    for (int i = 0; i < BmsEngine.dataLength; i += 4)
        //    {
        //        double pR = orginal[i + (int)RGB.Red] / 255.0;
        //        pR -= 0.5;
        //        pR *= contrast;
        //        pR += 0.5;
        //        pR *= 255;
        //        if (pR < 0) pR = 0;
        //        if (pR > 255) pR = 255;
        //        newJpegBytes[i + (int)RGB.Red] = (byte)pR;

        //        double pG = orginal[i + (int)RGB.Green] / 255.0;
        //        pG -= 0.5;
        //        pG *= contrast;
        //        pG += 0.5;
        //        pG *= 255;
        //        if (pG < 0) pG = 0;
        //        if (pG > 255) pG = 255;
        //        newJpegBytes[i + (int)RGB.Green] = (byte)pG;

        //        double pB = orginal[i + (int)RGB.Blue] / 255.0;
        //        pB -= 0.5;
        //        pB *= contrast;
        //        pB += 0.5;
        //        pB *= 255;
        //        if (pB < 0) pB = 0;
        //        if (pB > 255) pB = 255;
        //        newJpegBytes[i + (int)RGB.Blue] = (byte)pB;

        //        newJpegBytes[i + (int)RGB.BRIGTNESS] = orginal[i + (int)RGB.BRIGTNESS];

        //    }

        //    if (newJpegBytes.Length > 0)
        //    {
        //        BitmapSource redBms = BmsEngine.CloneBms(newJpegBytes);
        //        BmsEngine.SetDestination(newJpegBytes);
        //        brush.ImageSource = redBms;
        //    }
        //}

        public void UpdateBackgroundToCurrent()
        {
            var source = GetImageSource();
            if (source != null)
                SetBackground(source);
        }


        public void ExportToImage(Uri path, string ext)
        {
            if (path == null) return;

            // Save current canvas transform
            Transform transform = this.LayoutTransform;
            // reset current transform (in case it is scaled or rotated)
            this.LayoutTransform = null;

            // Get the size of canvas
            Size size = new Size(this.Width, this.Height);
            // Measure and arrange the surface
            // VERY IMPORTANT
            this.Measure(size);
            this.Arrange(new Rect(size));

            // Create a render bitmap and push the surface to it
            RenderTargetBitmap renderBitmap =
              new RenderTargetBitmap(
                (int)size.Width,
                (int)size.Height,
                96d,
                96d,
                PixelFormats.Pbgra32);
            renderBitmap.Render(this);

            // Create a file stream for saving image
            using (FileStream outStream = new FileStream(path.LocalPath, FileMode.Create))
            {
                BitmapEncoder encoder = CreateEncoder(ext);

                // push the rendered bitmap to it
                encoder.Frames.Add(BitmapFrame.Create(renderBitmap));
                // save the data to the stream
                encoder.Save(outStream);
            }

            // Restore previously saved layout
            this.LayoutTransform = transform;
        }

        private static BitmapEncoder CreateEncoder(string ext)
        {
            // Use png encoder for our data
            BitmapEncoder encoder = null;

            if (ext == ".png")
            {
                encoder = new PngBitmapEncoder();
            }

            else if (ext == ".bmp")
            {
                encoder = new BmpBitmapEncoder();
            }
            else if (ext == ".gif")
            {
                encoder = new GifBitmapEncoder();
            }
            else if (ext == ".jpg")
            {
                encoder = new JpegBitmapEncoder();
            }
            else
            {
                encoder = new JpegBitmapEncoder();
            }
            return encoder;
        }


        protected override void OnDrop(DragEventArgs e)
        {
            base.OnDrop(e);
            string xamlString = e.Data.GetData("DESIGNER_ITEM") as string;
            if (!String.IsNullOrEmpty(xamlString))
            {
                DesignerItem newItem = null;
                FrameworkElement content = XamlReader.Load(XmlReader.Create(new StringReader(xamlString))) as FrameworkElement;

                if (content != null)
                {
                    Point position = e.GetPosition(this);
                    double w, h, l, t;
                    if (content.MinHeight != 0 && content.MinWidth != 0)
                    {
                        w = content.MinWidth * 2; ;
                        h = content.MinHeight * 2;
                    }
                    else
                    {
                        w = 65;
                        h = 65;
                    }
                    l = Math.Max(0, position.X - w / 2);
                    t = Math.Max(0, position.Y - h / 2);

                    DesignerItem item = new DesignerItem(((System.Windows.Controls.Image)content).Source, l, t, l + w, t + h, 0, Colors.Black, 1);

                    ToolObject.AddNewObject(this, item);

                }

                e.Handled = true;
            }
        }

        public IEnumerable<DesignerItem> SelectedItems
        {
            get
            {
                var selectedItems = from item in this.Children.OfType<DesignerItem>()
                                    where item.IsSelected == true
                                    select item;

                return selectedItems;
            }
        }

        public void DeselectAll()
        {
            foreach (DesignerItem item in this.SelectedItems)
            {
                item.IsSelected = false;
            }
        }
    }
}
