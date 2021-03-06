using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;

namespace Silverwiz.Expander
{
    /// <summary>
    /// An Expander with animation.
    /// </summary>
    public class OdcButton : HeaderedContentControl
    {
        static OdcButton()
        {
            MarginProperty.OverrideMetadata(
             typeof(OdcButton),
             new FrameworkPropertyMetadata(new Thickness(0)));


            FocusableProperty.OverrideMetadata(typeof(OdcButton),
                new FrameworkPropertyMetadata(false));

            DefaultStyleKeyProperty.OverrideMetadata(typeof(OdcButton),
                new FrameworkPropertyMetadata(typeof(OdcButton)));

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
            typeof(Brush), typeof(OdcButton), new UIPropertyMetadata(Brushes.Gray));

        public Brush HeaderBackground
        {
            get { return (Brush)GetValue(HeaderBackgroundProperty); }
            set { SetValue(HeaderBackgroundProperty, value); }
        }

        public static readonly DependencyProperty HeaderBackgroundProperty =
            DependencyProperty.Register("HeaderBackground",
            typeof(Brush), typeof(OdcButton), new UIPropertyMetadata(Brushes.Silver));


        public bool IsMinimized
        {
            get { return (bool)GetValue(MinimizedProperty); }
            set { SetValue(MinimizedProperty, value); }
        }

        public static readonly DependencyProperty MinimizedProperty =
            DependencyProperty.Register("IsMinimized",
            typeof(bool), typeof(OdcButton),
            new UIPropertyMetadata(false, IsMinimizedChanged));

        public static void IsMinimizedChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            OdcButton expander = d as OdcButton;
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
            DependencyProperty.Register("Image", typeof(ImageSource), typeof(OdcButton), new UIPropertyMetadata(null));


        /// <summary>
        /// Gets or sets the ImageSource for the image in the header.
        /// </summary>
        public ImageSource FocusImage
        {
            get { return (ImageSource)GetValue(FocusImageProperty); }
            set { SetValue(FocusImageProperty, value); }
        }

        public static readonly DependencyProperty FocusImageProperty =
            DependencyProperty.Register("FocusImage", typeof(ImageSource), typeof(OdcButton), new UIPropertyMetadata(null));


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
            typeof(OdcButton),
            new UIPropertyMetadata(true, IsExpandedChanged));


        public static void IsExpandedChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            OdcButton expander = d as OdcButton;
            RoutedEventArgs args = new RoutedEventArgs((bool)e.NewValue ? ExpandedEvent : CollapsedEvent);
            expander.RaiseEvent(args);
        }

        public static readonly RoutedEvent ExpandedEvent = EventManager.RegisterRoutedEvent(
            "ExpandedEvent",
            RoutingStrategy.Bubble,
            typeof(RoutedEventHandler),
            typeof(OdcButton));


        public static readonly RoutedEvent CollapsedEvent = EventManager.RegisterRoutedEvent(
            "CollapsedEvent",
            RoutingStrategy.Bubble,
            typeof(RoutedEventHandler),
            typeof(OdcButton));

        public static readonly RoutedEvent MinimizedEvent = EventManager.RegisterRoutedEvent(
             "MinimizedEvent",
             RoutingStrategy.Bubble,
             typeof(RoutedEventHandler),
             typeof(OdcButton));


        public static readonly RoutedEvent MaximizedEvent = EventManager.RegisterRoutedEvent(
            "MaximizedEvent",
            RoutingStrategy.Bubble,
            typeof(RoutedEventHandler),
            typeof(OdcButton));

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
            DependencyProperty.Register("CornerRadius", typeof(CornerRadius), typeof(OdcButton), new UIPropertyMetadata(null));



        /// <summary>
        /// Gets or sets the background color of the header on mouse over.
        /// </summary>
        public Brush MouseOverHeaderBackground
        {
            get { return (Brush)GetValue(MouseOverHeaderBackgroundProperty); }
            set { SetValue(MouseOverHeaderBackgroundProperty, value); }
        }

        public static readonly DependencyProperty MouseOverHeaderBackgroundProperty =
            DependencyProperty.Register("MouseOverHeaderBackground", typeof(Brush), typeof(OdcButton), new UIPropertyMetadata(null));


        /// <summary>
        /// Gets whether the PressedBackground is not null.
        /// </summary>
        public bool HasPressedBackground
        {
            get { return (bool)GetValue(HasPressedBackgroundProperty); }
            set { SetValue(HasPressedBackgroundProperty, value); }
        }

        public static readonly DependencyProperty HasPressedBackgroundProperty =
            DependencyProperty.Register("HasPressedBackground", typeof(bool), typeof(OdcButton), new UIPropertyMetadata(false));



        /// <summary>
        /// Gets or sets the background color of the header in pressed mode.
        /// </summary>
        public Brush PressedHeaderBackground
        {
            get { return (Brush)GetValue(PressedHeaderBackgroundProperty); }
            set { SetValue(PressedHeaderBackgroundProperty, value); }
        }

        public static readonly DependencyProperty PressedHeaderBackgroundProperty =
            DependencyProperty.Register("PressedHeaderBackground", typeof(Brush), typeof(OdcButton),
            new UIPropertyMetadata(null, PressedHeaderBackgroundPropertyChangedCallback));


        public static void PressedHeaderBackgroundPropertyChangedCallback(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            OdcButton expander = (OdcButton)d;
            expander.HasPressedBackground = e.NewValue != null;
        }

        public Thickness HeaderBorderThickness
        {
            get { return (Thickness)GetValue(HeaderBorderThicknessProperty); }
            set { SetValue(HeaderBorderThicknessProperty, value); }
        }

        // Using a DependencyProperty as the backing store for HeaderBorderThickness.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty HeaderBorderThicknessProperty =
            DependencyProperty.Register("HeaderBorderThickness", typeof(Thickness), typeof(OdcButton), new UIPropertyMetadata(null));


        /// <summary>
        /// Gets or sets the foreground color of the header on mouse over.
        /// </summary>
        public Brush MouseOverHeaderForeground
        {
            get { return (Brush)GetValue(MouseOverHeaderForegroundProperty); }
            set { SetValue(MouseOverHeaderForegroundProperty, value); }
        }

        public static readonly DependencyProperty MouseOverHeaderForegroundProperty =
            DependencyProperty.Register("MouseOverHeaderForeground", typeof(Brush), typeof(OdcButton), new UIPropertyMetadata(null));



        /// <summary>
        /// Specifies whether to show a elipse with the expanded/collapsed image.
        /// </summary>
        public bool ShowEllipse
        {
            get { return (bool)GetValue(ShowEllipseProperty); }
            set { SetValue(ShowEllipseProperty, value); }
        }

        public static readonly DependencyProperty ShowEllipseProperty =
            DependencyProperty.Register("ShowEllipse", typeof(bool), typeof(OdcButton), new UIPropertyMetadata(false));

        /// <summary>
        /// Specifies whether to show a elipse with the expanded/collapsed image.
        /// </summary>
        public string NotificationCount
        {
            get { return (string)GetValue(NotificationCountProperty); }
            set { SetValue(NotificationCountProperty, value); }
        }

        public static readonly DependencyProperty NotificationCountProperty =
            DependencyProperty.Register("NotificationCount", typeof(string), typeof(OdcButton), new UIPropertyMetadata("0"));



        /// <summary>
        /// Specifies whether to show a elipse with the expanded/collapsed image.
        /// </summary>
        public bool IsSelected
        {
            get { return (bool)GetValue(IsSelectedProperty); }
            set { SetValue(IsSelectedProperty, value); }
        }

        public static readonly DependencyProperty IsSelectedProperty =
            DependencyProperty.Register("IsSelected", typeof(bool), typeof(OdcButton), new UIPropertyMetadata(false));

    }
}
