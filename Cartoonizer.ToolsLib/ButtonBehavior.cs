using System;
using System.Windows;
using System.Windows.Controls.Primitives;
using System.Windows.Media;

namespace Cartoonizer.ToolsLib
{
    public class EyeCandy
    {
        #region Image dependency property

        /// <summary>
        /// An attached dependency property which provides an
        /// <see cref="ImageSource" /> for arbitrary WPF elements.
        /// </summary>
        public static readonly DependencyProperty ImageProperty;

        /// <summary>
        /// Gets the <see cref="ImageProperty"/> for a given
        /// <see cref="DependencyObject"/>, which provides an
        /// <see cref="ImageSource" /> for arbitrary WPF elements.
        /// </summary>
        public static ImageSource GetImage(DependencyObject obj)
        {
            return (ImageSource)obj.GetValue(ImageProperty);
        }

        /// <summary>
        /// Sets the attached <see cref="ImageProperty"/> for a given
        /// <see cref="DependencyObject"/>, which provides an
        /// <see cref="ImageSource" /> for arbitrary WPF elements.
        /// </summary>
        public static void SetImage(DependencyObject obj, ImageSource value)
        {
            obj.SetValue(ImageProperty, value);
        }

        public static readonly DependencyProperty ButtonTextProperty;


        public static String GetButtonText(DependencyObject obj)
        {
            return (String)obj.GetValue(ButtonTextProperty);
        }

        public static void SetButtonText(DependencyObject obj, String value)
        {
            obj.SetValue(ButtonTextProperty, value);
        }


        public static readonly DependencyProperty HorizontalTextImageProperty;


        public static Boolean GetHorizontalTextImage(DependencyObject obj)
        {
            return (Boolean)obj.GetValue(HorizontalTextImageProperty);
        }

        public static void SetHorizontalTextImage(DependencyObject obj, Boolean value)
        {
            obj.SetValue(HorizontalTextImageProperty, value);
        }


        #endregion

        static EyeCandy()
        {
            //register attached dependency property
            var metadata1 = new FrameworkPropertyMetadata((ImageSource)null);
            var metadata2 = new FrameworkPropertyMetadata((String)null);
            var metadata3 = new FrameworkPropertyMetadata(false);
            ImageProperty = DependencyProperty.RegisterAttached("Image",
                                                                typeof(ImageSource),
                                                                typeof(EyeCandy), metadata1);

            ButtonTextProperty = DependencyProperty.RegisterAttached("ButtonText",
                                                                typeof(String),
                                                                typeof(EyeCandy), metadata2);

            HorizontalTextImageProperty = DependencyProperty.RegisterAttached("HorizontalTextImage",
                                                        typeof(Boolean),
                                                        typeof(EyeCandy), metadata3);

        }
    }

}
