using System.Windows;
using System.Windows.Controls;

namespace Silverwiz.Expander
{
    public class ExplorerBar : ItemsControl
    {
        bool progressing = false;
        static ExplorerBar()
        {
            DefaultStyleKeyProperty.OverrideMetadata(typeof(ExplorerBar), new FrameworkPropertyMetadata(typeof(ExplorerBar)));

        }

        protected override void OnItemsChanged(System.Collections.Specialized.NotifyCollectionChangedEventArgs e)
        {
            base.OnItemsChanged(e);
            if (e.Action == System.Collections.Specialized.NotifyCollectionChangedAction.Reset)
            {
                SizeChanged += ExplorerBar_SizeChanged;
                foreach (var item in Items)
                {
                    OdcExpander odcExpander = item as OdcExpander;
                    if (odcExpander != null)
                    {
                        odcExpander.Expanded += odcExpander_Expanded;
                        odcExpander.Collapsed += odcExpander_Collapsed;
                    }

                    OdcButton odcButton = item as OdcButton;
                    if (odcButton != null)
                    {
                        odcButton.Expanded += odcExpander_Expanded;
                        odcButton.Collapsed += odcExpander_Collapsed;
                    }

                    OdcSummary odcSummary = item as OdcSummary;
                    if (odcSummary != null)
                    {
                        odcSummary.Expanded += odcExpander_Expanded;
                        odcSummary.Collapsed += odcExpander_Collapsed;
                    }
                }
            }
        }

        private void ResetItemsHeight()
        {
            foreach (var item in Items)
            {
                OdcExpander odcExpander = item as OdcExpander;
                if (odcExpander != null)
                {
                    odcExpander.ExpandAreaHeight = CalculateExpandAreaheight();
                }
            }
        }

        private double CalculateExpandAreaheight()
        {
            var height = ActualHeight;
            foreach (var item in Items)
            {
                if (item is OdcSummary)
                {
                    if (((OdcSummary)item).IsEnabled)
                    {
                        height -= ((OdcSummary)item).ActualHeight;
                    }
                }
                else if (item is OdcExpander)
                {
                    height -= 35;
                }
            }
            return height;
        }

        private void ExplorerBar_SizeChanged(object sender, SizeChangedEventArgs e)
        {
            ResetItemsHeight();
        }

        private void odcExpander_Collapsed(object sender, RoutedEventArgs e)
        {
            if (!progressing)
            {
                progressing = true;
                OdcButton odcButton = sender as OdcButton;
                if (odcButton != null)
                {
                    odcButton.IsExpanded = true;
                    odcButton.IsSelected = true;
                }

                OdcExpander odcExpander = sender as OdcExpander;
                if (odcExpander != null)
                {
                    odcExpander.IsExpanded = true;
                    odcExpander.IsSelected = true;
                }
                ResetItemsHeight();
                progressing = false;
            }
        }

        private void odcExpander_Expanded(object sender, RoutedEventArgs e)
        {
            if (!(sender is OdcSummary))
            {
                progressing = true;
                foreach (var item in Items)
                {
                    if (sender != item)
                    {
                        OdcExpander odcExpander = item as OdcExpander;
                        if (odcExpander != null)
                        {
                            if (!(sender is OdcButton))
                            {
                                odcExpander.IsExpanded = false;
                            }
                            odcExpander.IsSelected = false;
                        }

                        OdcButton odcButton = item as OdcButton;
                        if (odcButton != null)
                        {
                            odcButton.IsExpanded = false;
                            odcButton.IsSelected = false;
                        }
                    }
                    else
                    {
                        OdcExpander odcExpander = item as OdcExpander;
                        if (odcExpander != null)
                        {
                            odcExpander.IsSelected = true;
                        }

                        OdcButton odcButton = item as OdcButton;
                        if (odcButton != null)
                        {
                            odcButton.IsSelected = true;
                        }
                    }
                }
                ResetItemsHeight();
                progressing = false;
            }
        }

    }
}
