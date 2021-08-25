using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;

namespace Silverwiz.Expander
{
    /// <summary>
    /// An Expander with animation.
    /// </summary>
    public class OdcSummary : HeaderedContentControl
    {
        static OdcSummary()
        {
            //MarginProperty.OverrideMetadata(
            //    typeof(OdcSummary),
            //    new FrameworkPropertyMetadata(new Thickness(10, 10, 10, 2)));

            FocusableProperty.OverrideMetadata(typeof(OdcSummary),
                new FrameworkPropertyMetadata(false));

            DefaultStyleKeyProperty.OverrideMetadata(typeof(OdcSummary),
                new FrameworkPropertyMetadata(typeof(OdcSummary)));

        }

        /// <summary>
        /// Gets or sets the custom skin for the control.
        /// </summary>
        public static string Skin { get; set; }


        protected override void OnInitialized(EventArgs e)
        {
            base.OnInitialized(e);
            ApplySkin();
        }

        public void ApplySkin()
        {
            if (!string.IsNullOrEmpty(Skin))
            {
                Uri uri = new Uri(Skin, UriKind.Absolute);
                ResourceDictionary skin = new ResourceDictionary();
                skin.Source = uri;
                this.Resources = skin;
            }
        }

        public Brush HeaderBorderBrush
        {
            get { return (Brush)GetValue(HeaderBorderBrushProperty); }
            set { SetValue(HeaderBorderBrushProperty, value); }
        }

        public static readonly DependencyProperty HeaderBorderBrushProperty =
            DependencyProperty.Register("HeaderBorderBrush",
            typeof(Brush), typeof(OdcSummary), new UIPropertyMetadata(Brushes.Gray));

        public Brush HeaderBackground
        {
            get { return (Brush)GetValue(HeaderBackgroundProperty); }
            set { SetValue(HeaderBackgroundProperty, value); }
        }

        public static readonly DependencyProperty HeaderBackgroundProperty =
            DependencyProperty.Register("HeaderBackground",
            typeof(Brush), typeof(OdcSummary), new UIPropertyMetadata(Brushes.Silver));


        public bool IsMinimized
        {
            get { return (bool)GetValue(MinimizedProperty); }
            set { SetValue(MinimizedProperty, value); }
        }

        public static readonly DependencyProperty MinimizedProperty =
            DependencyProperty.Register("IsMinimized",
            typeof(bool), typeof(OdcSummary),
            new UIPropertyMetadata(false, IsMinimizedChanged));

        public static void IsMinimizedChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            OdcSummary expander = d as OdcSummary;
            RoutedEventArgs args = new RoutedEventArgs((bool)e.NewValue ? MinimizedEvent : MaximizedEvent);
            expander.RaiseEvent(args);
        }


        /// <summary>
        /// Gets or sets the ImageSource for the image in the header.
        /// </summary>
        public ImageSource Image
        {
            get { return (ImageSource)GetValue(ImageProperty); }
            set { SetValue(ImageProperty, value); }
        }

        public static readonly DependencyProperty ImageProperty =
            DependencyProperty.Register("Image", typeof(ImageSource), typeof(OdcSummary), new UIPropertyMetadata(null));


        /// <summary>
        /// Gets or sets the ImageSource for the image in the header.
        /// </summary>
        public ImageSource FocusImage
        {
            get { return (ImageSource)GetValue(FocusImageProperty); }
            set { SetValue(FocusImageProperty, value); }
        }

        public static readonly DependencyProperty FocusImageProperty =
            DependencyProperty.Register("FocusImage", typeof(ImageSource), typeof(OdcSummary), new UIPropertyMetadata(null));


        public bool IsExpanded
        {
            get { return (bool)GetValue(IsExpandedProperty); }
            set { SetValue(IsExpandedProperty, value); }
        }

        public event RoutedEventHandler Expanded
        {
            add { AddHandler(ExpandedEvent, value); }
            remove { RemoveHandler(ExpandedEvent, value); }
        }

        public event RoutedEventHandler Collapsed
        {
            add { AddHandler(CollapsedEvent, value); }
            remove { RemoveHandler(CollapsedEvent, value); }
        }

        public event RoutedEventHandler Minimized
        {
            add { AddHandler(MinimizedEvent, value); }
            remove { RemoveHandler(MinimizedEvent, value); }
        }

        public event RoutedEventHandler Maximized
        {
            add { AddHandler(MaximizedEvent, value); }
            remove { RemoveHandler(MaximizedEvent, value); }
        }

        #region dependency properties and routed events definition

        public static readonly DependencyProperty IsExpandedProperty =
            DependencyProperty.Register(
            "IsExpanded",
            typeof(bool),
            typeof(OdcSummary),
            new UIPropertyMetadata(true, IsExpandedChanged));


        public static void IsExpandedChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            OdcSummary expander = d as OdcSummary;
            RoutedEventArgs args = new RoutedEventArgs((bool)e.NewValue ? ExpandedEvent : CollapsedEvent);
            expander.RaiseEvent(args);
        }

        public static readonly RoutedEvent ExpandedEvent = EventManager.RegisterRoutedEvent(
            "ExpandedEvent",
            RoutingStrategy.Bubble,
            typeof(RoutedEventHandler),
            typeof(OdcSummary));


        public static readonly RoutedEvent CollapsedEvent = EventManager.RegisterRoutedEvent(
            "CollapsedEvent",
            RoutingStrategy.Bubble,
            typeof(RoutedEventHandler),
            typeof(OdcSummary));

        public static readonly RoutedEvent MinimizedEvent = EventManager.RegisterRoutedEvent(
             "MinimizedEvent",
             RoutingStrategy.Bubble,
             typeof(RoutedEventHandler),
             typeof(OdcSummary));


        public static readonly RoutedEvent MaximizedEvent = EventManager.RegisterRoutedEvent(
            "MaximizedEvent",
            RoutingStrategy.Bubble,
            typeof(RoutedEventHandler),
            typeof(OdcSummary));

        #endregion



        /// <summary>
        /// Gets or sets the corner radius for the header.
        /// </summary>
        public CornerRadius CornerRadius
        {
            get { return (CornerRadius)GetValue(CornerRadiusProperty); }
            set { SetValue(CornerRadiusProperty, value); }
        }

        public static readonly DependencyProperty CornerRadiusProperty =
            DependencyProperty.Register("CornerRadius", typeof(CornerRadius), typeof(OdcSummary), new UIPropertyMetadata(null));



        /// <summary>
        /// Gets or sets the background color of the header on mouse over.
        /// </summary>
        public Brush MouseOverHeaderBackground
        {
            get { return (Brush)GetValue(MouseOverHeaderBackgroundProperty); }
            set { SetValue(MouseOverHeaderBackgroundProperty, value); }
        }

        public static readonly DependencyProperty MouseOverHeaderBackgroundProperty =
            DependencyProperty.Register("MouseOverHeaderBackground", typeof(Brush), typeof(OdcSummary), new UIPropertyMetadata(null));


        /// <summary>
        /// Gets whether the PressedBackground is not null.
        /// </summary>
        public bool HasPressedBackground
        {
            get { return (bool)GetValue(HasPressedBackgroundProperty); }
            set { SetValue(HasPressedBackgroundProperty, value); }
        }

        public static readonly DependencyProperty HasPressedBackgroundProperty =
            DependencyProperty.Register("HasPressedBackground", typeof(bool), typeof(OdcSummary), new UIPropertyMetadata(false));



        /// <summary>
        /// Gets or sets the background color of the header in pressed mode.
        /// </summary>
        public Brush PressedHeaderBackground
        {
            get { return (Brush)GetValue(PressedHeaderBackgroundProperty); }
            set { SetValue(PressedHeaderBackgroundProperty, value); }
        }

        public static readonly DependencyProperty PressedHeaderBackgroundProperty =
            DependencyProperty.Register("PressedHeaderBackground", typeof(Brush), typeof(OdcSummary),
            new UIPropertyMetadata(null, PressedHeaderBackgroundPropertyChangedCallback));


        public static void PressedHeaderBackgroundPropertyChangedCallback(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            OdcSummary expander = (OdcSummary)d;
            expander.HasPressedBackground = e.NewValue != null;
        }

        public Thickness HeaderBorderThickness
        {
            get { return (Thickness)GetValue(HeaderBorderThicknessProperty); }
            set { SetValue(HeaderBorderThicknessProperty, value); }
        }

        // Using a DependencyProperty as the backing store for HeaderBorderThickness.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty HeaderBorderThicknessProperty =
            DependencyProperty.Register("HeaderBorderThickness", typeof(Thickness), typeof(OdcSummary), new UIPropertyMetadata(null));




        /// <summary>
        /// Gets or sets the foreground color of the header on mouse over.
        /// </summary>
        public Brush MouseOverHeaderForeground
        {
            get { return (Brush)GetValue(MouseOverHeaderForegroundProperty); }
            set { SetValue(MouseOverHeaderForegroundProperty, value); }
        }

        public static readonly DependencyProperty MouseOverHeaderForegroundProperty =
            DependencyProperty.Register("MouseOverHeaderForeground", typeof(Brush), typeof(OdcSummary), new UIPropertyMetadata(null));



        /// <summary>
        /// Specifies whether to show a elipse with the expanded/collapsed image.
        /// </summary>
        public bool ShowEllipse
        {
            get { return (bool)GetValue(ShowEllipseProperty); }
            set { SetValue(ShowEllipseProperty, value); }
        }

        public static readonly DependencyProperty ShowEllipseProperty =
            DependencyProperty.Register("ShowEllipse", typeof(bool), typeof(OdcSummary), new UIPropertyMetadata(false));


        /// <summary>
        /// Specifies whether to show a elipse with the expanded/collapsed image.
        /// </summary>
        public double ExpandAreaHeight
        {
            get { return (double)GetValue(ExpandAreaHeightProperty); }
            set { SetValue(ExpandAreaHeightProperty, value); }
        }

        public static readonly DependencyProperty ExpandAreaHeightProperty =
            DependencyProperty.Register("ExpandAreaHeight", typeof(double), typeof(OdcSummary), new UIPropertyMetadata((double)0.0));


        /// <summary>
        /// Specifies whether to show a elipse with the expanded/collapsed image.
        /// </summary>
        public string NotificationCount
        {
            get { return (string)GetValue(NotificationCountProperty); }
            set { SetValue(NotificationCountProperty, value); }
        }

        public static readonly DependencyProperty NotificationCountProperty =
            DependencyProperty.Register("NotificationCount", typeof(string), typeof(OdcSummary), new UIPropertyMetadata("0"));

        public bool OpacityAnimation
        {
            get { return (bool)GetValue(OpacityAnimationProperty); }
            set { SetValue(OpacityAnimationProperty, value); }
        }

        public static readonly DependencyProperty OpacityAnimationProperty =
            DependencyProperty.Register("OpacityAnimation", typeof(bool), typeof(OdcSummary), new UIPropertyMetadata(true));

        
    }
}
