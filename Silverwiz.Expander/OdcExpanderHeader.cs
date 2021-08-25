using System.Windows;
using System.Windows.Controls.Primitives;
using System.Windows.Media;
using System.ComponentModel;

namespace Silverwiz.Expander
{
    /// <summary>
    /// A helper class to specify the header of an OdcExpander.
    /// </summary>
    public class OdcExpanderHeader : ToggleButton, INotifyPropertyChanged
    {
        static OdcExpanderHeader()
        {
            DefaultStyleKeyProperty.OverrideMetadata(typeof(OdcExpanderHeader), new FrameworkPropertyMetadata(typeof(OdcExpanderHeader)));
        }


        /// <summary>
        /// Gets whether the expand geometry is not null.
        /// </summary>
        public bool HasExpandGeometry
        {
            get { return (bool)GetValue(HasExpandGeometryProperty); }
            set { SetValue(HasExpandGeometryProperty, value); }
        }

        public static readonly DependencyProperty HasExpandGeometryProperty =
            DependencyProperty.Register("HasExpandGeometry", typeof(bool), typeof(OdcExpanderHeader), new UIPropertyMetadata(false));



        /// <summary>
        /// Gets or sets the geometry for the collapse symbol.
        /// </summary>
        public Geometry CollapseGeometry
        {
            get { return (Geometry)GetValue(CollapseGeometryProperty); }
            set { SetValue(CollapseGeometryProperty, value); }
        }

        public static readonly DependencyProperty CollapseGeometryProperty =
            DependencyProperty.Register("CollapseGeometry", typeof(Geometry), typeof(OdcExpanderHeader),
            new UIPropertyMetadata(null));


        public static void CollapseGeometryChangedCallback(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            OdcExpanderHeader eh = d as OdcExpanderHeader;
            eh.HasExpandGeometry = e.NewValue != null;
        }


        /// <summary>
        /// Gets or sets the geometry for the expand symbol.
        /// </summary>
        public Geometry ExpandGeometry
        {
            get { return (Geometry)GetValue(ExpandGeometryProperty); }
            set { SetValue(ExpandGeometryProperty, value); }
        }

        public static readonly DependencyProperty ExpandGeometryProperty =
            DependencyProperty.Register("ExpandGeometry", typeof(Geometry), typeof(OdcExpanderHeader), new UIPropertyMetadata(null, CollapseGeometryChangedCallback));



        /// <summary>
        /// Gets or sets the corner radius for the header.
        /// </summary>
        public CornerRadius CornerRadius
        {
            get { return (CornerRadius)GetValue(CornerRadiusProperty); }
            set { SetValue(CornerRadiusProperty, value); }
        }

        // Using a DependencyProperty as the backing store for CornerRadius.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty CornerRadiusProperty =
            DependencyProperty.Register("CornerRadius", typeof(CornerRadius), typeof(OdcExpanderHeader), new UIPropertyMetadata(null));



        /// <summary>
        /// Gets or sets whether to display the ellipse arround the collapse/expand symbol.
        /// </summary>
        public bool ShowEllipse
        {
            get { return (bool)GetValue(ShowEllipseProperty); }
            set { SetValue(ShowEllipseProperty, value); }
        }

        // Using a DependencyProperty as the backing store for ShowEllipse.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty ShowEllipseProperty =
            DependencyProperty.Register("ShowEllipse", typeof(bool), typeof(OdcExpanderHeader), new UIPropertyMetadata(true));



        /// <summary>
        /// Gets or sets the Image to display on the header.
        /// </summary>
        public ImageSource Image
        {
            get { return (ImageSource)GetValue(ImageProperty); }
            set { SetValue(ImageProperty, value); }
        }

        // Using a DependencyProperty as the backing store for Image.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty ImageProperty =
            DependencyProperty.Register("Image", typeof(ImageSource), typeof(OdcExpanderHeader), new UIPropertyMetadata(null));


        /// <summary>
        /// Gets or sets the ImageSource for the image in the header.
        /// </summary>
        public ImageSource FocusImage
        {
            get { return (ImageSource)GetValue(FocusImageProperty); }
            set { SetValue(FocusImageProperty, value); }
        }

        public static readonly DependencyProperty FocusImageProperty =
            DependencyProperty.Register("FocusImage", typeof(ImageSource), typeof(OdcExpanderHeader), new UIPropertyMetadata(null));


        /// <summary>
        /// Gets or sets the Image to display on the header.
        /// </summary>
        public bool CanExpand
        {
            get { return (bool)GetValue(CanExpandProperty); }
            set { SetValue(CanExpandProperty, value); }
        }

        // Using a DependencyProperty as the backing store for Image.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty CanExpandProperty =
            DependencyProperty.Register("CanExpand", typeof(bool), typeof(OdcExpanderHeader), new UIPropertyMetadata(null));


        /// <summary>
        /// Specifies whether to show a elipse with the expanded/collapsed image.
        /// </summary>
        public string NotificationCount
        {
            get { return (string)GetValue(NotificationCountProperty); }
            set
            {
                SetValue(NotificationCountProperty, value);
            }
        }

        public static readonly DependencyProperty NotificationCountProperty =
            DependencyProperty.Register("NotificationCount", typeof(string), typeof(OdcExpanderHeader),
            new FrameworkPropertyMetadata("0", new PropertyChangedCallback(OnFirstPropertyChanged)));


        private static void OnFirstPropertyChanged(DependencyObject sender, DependencyPropertyChangedEventArgs e)
        {
            int nc = 0;
            if (string.IsNullOrEmpty(e.NewValue.ToString()))
                ((OdcExpanderHeader)sender).HasNotificationCount = false;
            else if (!int.TryParse(e.NewValue.ToString(), out nc))
                ((OdcExpanderHeader)sender).HasNotificationCount = false;
            else if (nc == 0)
                ((OdcExpanderHeader)sender).HasNotificationCount = false;
            else
                ((OdcExpanderHeader)sender).HasNotificationCount = true;
        }

        /// <summary>
        /// Specifies whether to show a elipse with the expanded/collapsed image.
        /// </summary>
        public bool HasNotificationCount
        {
            get { return (bool)GetValue(HasNotificationCountProperty); }
            set { SetValue(HasNotificationCountProperty, value); }
        }

        public static readonly DependencyProperty HasNotificationCountProperty =
            DependencyProperty.Register("HasNotificationCount", typeof(bool), typeof(OdcExpanderHeader), new UIPropertyMetadata(false));

        /// <summary>
        /// Specifies whether to show a elipse with the expanded/collapsed image.
        /// </summary>
        public bool IsSelected
        {
            get { return (bool)GetValue(IsSelectedProperty); }
            set { SetValue(IsSelectedProperty, value); }
        }

        public static readonly DependencyProperty IsSelectedProperty =
            DependencyProperty.Register("IsSelected", typeof(bool), typeof(OdcExpanderHeader), new UIPropertyMetadata(false));


        /// <summary>
        /// Specifies whether to show a elipse with the expanded/collapsed image.
        /// </summary>
        public bool IsExpanded
        {
            get { return (bool)GetValue(IsExpandedProperty); }
            set { SetValue(IsExpandedProperty, value); }
        }

        public static readonly DependencyProperty IsExpandedProperty =
            DependencyProperty.Register("IsExpanded", typeof(bool), typeof(OdcExpanderHeader), new UIPropertyMetadata(false));

        public event PropertyChangedEventHandler PropertyChanged;


    }
}
