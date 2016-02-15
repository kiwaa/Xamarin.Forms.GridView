using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Xamarin.Forms;

namespace GridView
{
    public class GridView : Layout<View>
    {
        public static readonly BindableProperty ItemsSourceProperty = BindableProperty.Create<GridView, IEnumerable>(o => o.ItemsSource, default(IEnumerable), propertyChanged: OnItemsSourceChanged);
        public IEnumerable ItemsSource
        {
            get { return (IEnumerable)GetValue(ItemsSourceProperty); }
            set { SetValue(ItemsSourceProperty, value); }
        }

        private static void OnItemsSourceChanged(BindableObject bindable, IEnumerable oldvalue, IEnumerable newvalue)
        {
            var view = (GridView)bindable;
            view.ReCreateChildrens();
        }

        public static readonly BindableProperty ItemTemplateProperty = BindableProperty.Create<GridView, DataTemplate>(o => o.ItemTemplate, default(DataTemplate), propertyChanged: OnItemsTemplateChanged);

        public DataTemplate ItemTemplate
        {
            get { return (DataTemplate)GetValue(ItemTemplateProperty); }
            set { SetValue(ItemTemplateProperty, value); }
        }
        private static void OnItemsTemplateChanged(BindableObject bindable, DataTemplate oldvalue, DataTemplate newvalue)
        {
            var view = (GridView)bindable;
            view.ReCreateChildrens();
        }

        public int MaxItemsPerRow { get; set; }
        public int ItemHeight { get; set; }
        public double RowSpacing { get; set; }
        public double ColumnSpacing { get; set; }


        public GridView()
        {
            BackgroundColor = Color.Red;
        }

        private void ReCreateChildrens()
        {
            if (ItemsSource == null || ItemTemplate == null)
                return;

            foreach (var item in ItemsSource)
            {
                var view = ItemTemplate.CreateContent() as View;
                view.BindingContext = item;
                Children.Add(view);
            }
        }


        protected override void LayoutChildren(double x, double y, double width, double height)
        {
            var colWidth = width / MaxItemsPerRow;
            for (int i = 0; i < Children.Count; i++)
            {
                var child = Children[i];
                if (!child.IsVisible)
                    continue;

                var virtualColumn = i % MaxItemsPerRow;
                var virtualRow = i / MaxItemsPerRow;

                var rowSpacing = (virtualRow != 0) ? RowSpacing : 0;
                var colSpacing = (virtualColumn != 0) ? ColumnSpacing : 0;

                var childX = x + (colWidth + colSpacing) * virtualColumn;
                var childY = y + (ItemHeight + rowSpacing) * virtualRow;
                LayoutChildIntoBoundingRegion(child, new Rectangle(childX, childY, colWidth, ItemHeight));
            }
        }

        protected override SizeRequest OnSizeRequest(double widthConstraint, double heightConstraint)
        {

            // Check our cache for existing results
            SizeRequest cachedResult;
            var constraintSize = new Size(widthConstraint, heightConstraint);
            if (_measureCache.TryGetValue(constraintSize, out cachedResult))
            {
                return cachedResult;
            }


            var height = 0.0;
            var minHeight = 0.0;
            var width = 0.0;
            var minWidth = 0.0;

            var visibleChildrensCount = (double)Children.Count(c => c.IsVisible);
            var rowsCount = Math.Ceiling(visibleChildrensCount / MaxItemsPerRow);
            height = minHeight = (ItemHeight + RowSpacing) * rowsCount - RowSpacing;
            width = minWidth = widthConstraint;

            // store our result in the cache for next time
            var result = new SizeRequest(new Size(width, height), new Size(minWidth, minHeight));
            _measureCache[constraintSize] = result;
            return result;
        }

        protected override void InvalidateMeasure()
        {
            _measureCache.Clear();
            base.InvalidateMeasure();
        }

        readonly Dictionary<Size, SizeRequest> _measureCache = new Dictionary<Size, SizeRequest>();

    }
}
