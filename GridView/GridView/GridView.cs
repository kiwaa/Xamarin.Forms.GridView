using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using Xamarin.Forms;

namespace GridView
{
    public class GridView : Layout<View>
    {
        public static readonly BindableProperty ItemsSourceProperty = BindableProperty.Create("ItemsSource", typeof(IEnumerable), typeof(GridView), null, BindingMode.OneWay, null, null, null, null);

        public static readonly BindableProperty ItemTemplateProperty = BindableProperty.Create("ItemTemplate", typeof(DataTemplate), typeof(GridView), null, BindingMode.OneWay, null, null, null, null);

        public static readonly BindableProperty ItemHeightProperty = BindableProperty.Create("ItemHeight", typeof(double), typeof(GridView), (double)100, BindingMode.OneWay, null, null, null, null);

        public static readonly BindableProperty MaxItemsPerRowProperty = BindableProperty.Create("MaxItemsPerRow", typeof(int), typeof(GridView), -1, BindingMode.OneWay, null, null, null, null);
        
        public IEnumerable ItemsSource
        {
            get
            {
                return (IEnumerable)base.GetValue(GridView.ItemsSourceProperty);
            }
            set
            {
                base.SetValue(GridView.ItemsSourceProperty, value);
            }
        }

        public DataTemplate ItemTemplate
        {
            get
            {
                return (DataTemplate)base.GetValue(GridView.ItemTemplateProperty);
            }
            set
            {
                base.SetValue(GridView.ItemTemplateProperty, value);
            }
        }

        public double ItemHeight
        {
            get
            {
                return (double)base.GetValue(GridView.ItemHeightProperty);
            }
            set
            {
                base.SetValue(GridView.ItemHeightProperty, value);
            }
        }

        public int MaxItemsPerRow
        {
            get
            {
                return (int)base.GetValue(GridView.MaxItemsPerRowProperty);
            }
            set
            {
                base.SetValue(GridView.MaxItemsPerRowProperty, value);
            }
        }

        private bool HasVisibileChildren()
        {
            return Children.Any(t => t.IsVisible);
        }

        public GridView()
        {
            BackgroundColor = Color.Red;
        }

        protected override void LayoutChildren(double x, double y, double width, double height)
        {
            for (int i = 0; i < Children.Count; i++)
            {
                var child = (View)Children[i];
                // skip invisible children

                if (!child.IsVisible)
                    continue;
                var childSizeRequest = child.GetSizeRequest(double.PositiveInfinity, height);
                var childWidth = childSizeRequest.Request.Width;
                LayoutChildIntoBoundingRegion(child, new Rectangle(x, y, childWidth, height));
                x += childWidth;
            }

            //if (!HasVisibileChildren())
            //    return;
            ////if (width == this.layoutInformation.Constraint.Width && height == this.layoutInformation.Constraint.Height)
            ////{
            ////    StackOrientation orientation = this.Orientation;
            ////    this.AlignOffAxis(this.layoutInformation, orientation, width, height);
            ////    this.ProcessExpanders(this.layoutInformation, orientation, x, y, width, height);
            ////}
            ////else
            //CalculateLayout(layoutInformation, x, y, width, height, true);
            //for (int index = 0; index < this.LogicalChildren.Count; ++index)
            //{
            //    View view = (View)this.LogicalChildren[index];
            //    if (view.IsVisible)
            //        LayoutChildIntoBoundingRegion(view, layoutInformation.Plots[index], layoutInformation.Requests[index]);
            //}
        }

        private void CalculateLayout(LayoutInformation layout, double x, double y, double widthConstraint, double heightConstraint, bool processExpanders)
        {
            layout.Constraint = new Size(widthConstraint, heightConstraint);
            layout.Expanders = 0;
            layout.CompressionSpace = 0.0;
            layout.Plots = new Rectangle[Children.Count];
            layout.Requests = new SizeRequest[Children.Count];
            //StackOrientation orientation = this.Orientation;
            //this.CalculateNaiveLayout(layout, orientation, x, y, widthConstraint, heightConstraint);
            //this.CompressNaiveLayout(layout, orientation, widthConstraint, heightConstraint);
            CalculateNaiveLayout(layout, x, y, widthConstraint, heightConstraint);
            CompressNaiveLayout(layout, widthConstraint, heightConstraint);
            //if (!processExpanders)
            //    return;
            //this.AlignOffAxis(layout, orientation, widthConstraint, heightConstraint);
            //this.ProcessExpanders(layout, orientation, x, y, widthConstraint, heightConstraint);
        }

        internal static void LayoutChildIntoBoundingRegion(VisualElement child, Rectangle region, SizeRequest childSizeRequest)
        {
            View view = child as View;
            if (view != null && region.Size != childSizeRequest.Request)
            {
                double width1 = region.Width;
                Size request = childSizeRequest.Request;
                double width2 = request.Width;
                int num1;
                if (width1 >= width2)
                {
                    double height1 = region.Height;
                    request = childSizeRequest.Request;
                    double height2 = request.Height;
                    num1 = height1 >= height2 ? 1 : 0;
                }
                else
                    num1 = 0;
                bool flag = num1 != 0;
                LayoutOptions layoutOptions = view.HorizontalOptions;
                if (layoutOptions.Alignment != LayoutAlignment.Fill)
                {
                    SizeRequest sizeRequest = flag ? childSizeRequest : child.GetSizeRequest(region.Width, region.Height);
                    double val1 = 0.0;
                    double width3 = region.Width;
                    request = sizeRequest.Request;
                    double width4 = request.Width;
                    double val2 = width3 - width4;
                    double num2 = Math.Max(val1, val2);
                    // ISSUE: explicit reference operation
                    // ISSUE: variable of a reference type
                    Rectangle local = @region;
                    // ISSUE: explicit reference operation
                    double x = local.X;
                    double num3 = num2;
                    layoutOptions = view.HorizontalOptions;
                    double num4 = layoutOptions.Alignment.ToDouble();
                    double num5 = (double)(int)(num3 * num4);
                    double num6 = x + num5;
                    // ISSUE: explicit reference operation
                    local.X = num6;
                    region.Width -= num2;
                }
                layoutOptions = view.VerticalOptions;
                if (layoutOptions.Alignment != LayoutAlignment.Fill)
                {
                    SizeRequest sizeRequest = flag ? childSizeRequest : child.GetSizeRequest(region.Width, region.Height);
                    double val1 = 0.0;
                    double height1 = region.Height;
                    request = sizeRequest.Request;
                    double height2 = request.Height;
                    double val2 = height1 - height2;
                    double num2 = Math.Max(val1, val2);
                    // ISSUE: explicit reference operation
                    // ISSUE: variable of a reference type
                    Rectangle local = @region;
                    // ISSUE: explicit reference operation
                    double y = local.Y;
                    double num3 = num2;
                    layoutOptions = view.VerticalOptions;
                    double num4 = layoutOptions.Alignment.ToDouble();
                    double num5 = (double)(int)(num3 * num4);
                    double num6 = y + num5;
                    // ISSUE: explicit reference operation
                    local.Y = num6;
                    region.Height -= num2;
                }
            }
            child.Layout(region);
        }

        private void CalculateNaiveLayout(LayoutInformation layout, double x, double y, double widthConstraint, double heightConstraint)
        {
            layout.CompressionSpace = 0.0;
            double num1 = x;
            double num2 = y;
            double width1 = 0.0;
            double height1 = 0.0;
            double width2 = 0.0;
            double height2 = 0.0;
            //double spacing = this.Spacing;
            //if (orientation == StackOrientation.Vertical)

            View view1 = null;
            for (int index = 0; index < LogicalChildren.Count; ++index)
            {
                View view2 = (View) LogicalChildren[index];
                if (view2.IsVisible)
                {
                    if (view2.VerticalOptions.Expands)
                    {
                        ++layout.Expanders;
                        if (view1 != null)
                            ComputeConstraintForView(view2, false);
                        view1 = view2;
                    }
                    SizeRequest sizeRequest = view2.GetSizeRequest(widthConstraint, double.PositiveInfinity);
                    Rectangle rectangle;
                    // ISSUE: explicit reference operation
                    // ISSUE: variable of a reference type
                    //Rectangle local = @rectangle;
                    double x1 = x;
                    double y1 = num2;
                    Size size = sizeRequest.Request;
                    double width3 = size.Width;
                    size = sizeRequest.Request;
                    double height3 = size.Height;
                    // ISSUE: explicit reference operation
                    rectangle = new Rectangle(x1, y1, width3, height3);
                    layout.Plots[index] = rectangle;
                    layout.Requests[index] = sizeRequest;
                    LayoutInformation layoutInformation = layout;
                    double num3 = layoutInformation.CompressionSpace;
                    double val1_1 = 0.0;
                    size = sizeRequest.Request;
                    double height4 = size.Height;
                    size = sizeRequest.Minimum;
                    double height5 = size.Height;
                    double val2 = height4 - height5;
                    double num4 = Math.Max(val1_1, val2);
                    double num5 = num3 + num4;
                    layoutInformation.CompressionSpace = num5;
                    //num2 = rectangle.Bottom + spacing;
                    double val1_2 = width1;
                    size = sizeRequest.Request;
                    double width4 = size.Width;
                    width1 = Math.Max(val1_2, width4);
                    height1 = rectangle.Bottom - y;
                    double num6 = height2;
                    size = sizeRequest.Minimum;
                    //double num7 = size.Height + spacing;
                    //height2 = num6 + num7;
                    double val1_3 = width2;
                    size = sizeRequest.Minimum;
                    double width5 = size.Width;
                    width2 = Math.Max(val1_3, width5);
                }
            }
            //height2 -= spacing;
            if (view1 != null)
                this.ComputeConstraintForView(view1, layout.Expanders == 1);

            layout.Bounds = new Rectangle(x, y, width1, height1);
            layout.MinimumSize = new Size(width2, height2);
        }

        private void CompressNaiveLayout(LayoutInformation layout, double widthConstraint, double heightConstraint)
        {
            if (layout.CompressionSpace <= 0.0)
                return;
            //if (orientation == StackOrientation.Vertical)
                this.CompressVerticalLayout(layout, widthConstraint, heightConstraint);
            //else
            //    this.CompressHorizontalLayout(layout, widthConstraint, heightConstraint);
        }
        private void CompressVerticalLayout(LayoutInformation layout, double widthConstraint, double heightConstraint)
        {
            double num1 = 0.0;
            if (heightConstraint >= layout.Bounds.Height)
                return;
            double num2 = layout.Bounds.Height - heightConstraint;
            double num3 = layout.CompressionSpace;
            double num4 = (num2 / layout.CompressionSpace).Clamp(0.0, 1.0);
            for (int index = 0; index < layout.Plots.Length; ++index)
            {
                View view = (View)this.LogicalChildren[index];
                if (view.IsVisible)
                {
                    Size minimum = layout.Requests[index].Minimum;
                    layout.Plots[index].Y -= num1;
                    Rectangle rectangle = layout.Plots[index];
                    double num5 = rectangle.Height - minimum.Height;
                    if (num5 > 0.0)
                    {
                        num3 -= num5;
                        double num6 = num5 * num4;
                        num1 += num6;
                        double heightConstraint1 = rectangle.Height - num6;
                        SizeRequest sizeRequest = view.GetSizeRequest(widthConstraint, heightConstraint1);
                        layout.Requests[index] = sizeRequest;
                        // ISSUE: explicit reference operation
                        // ISSUE: variable of a reference type
                        Rectangle local = @rectangle;
                        Size request = sizeRequest.Request;
                        double width = request.Width;
                        // ISSUE: explicit reference operation
                        local.Width = width;
                        request = sizeRequest.Request;
                        if (request.Height < heightConstraint1)
                        {
                            double num7 = heightConstraint1;
                            request = sizeRequest.Request;
                            double height = request.Height;
                            double num8 = num7 - height;
                            request = sizeRequest.Request;
                            heightConstraint1 = request.Height;
                            num1 += num8;
                            num2 -= num1;
                            num4 = NumericExtensions.Clamp(num2 / num3, 0.0, 1.0);
                        }
                        rectangle.Height = heightConstraint1;
                        layout.Bounds.Width = Math.Max(layout.Bounds.Width, rectangle.Width);
                        layout.Plots[index] = rectangle;
                    }
                }
            }
        }


        private void ComputeConstraintForView(View view, bool isOnlyExpander)
        {
            //if ((this.Constraint & LayoutConstraint.HorizontallyFixed) != LayoutConstraint.None)
            //{
            //    LayoutOptions layoutOptions = view.HorizontalOptions;
            //    if (layoutOptions.Alignment == LayoutAlignment.Fill)
            //    {
            //        if (isOnlyExpander)
            //        {
            //            layoutOptions = view.VerticalOptions;
            //            if (layoutOptions.Alignment == LayoutAlignment.Fill && this.Constraint == LayoutConstraint.Fixed)
            //            {
            //                view.ComputedConstraint = LayoutConstraint.Fixed;
            //                return;
            //            }
            //        }
            //        view.ComputedConstraint = LayoutConstraint.HorizontallyFixed;
            //        return;
            //    }
            //}
            //view.ComputedConstraint = LayoutConstraint.None;
        }


        // helpers
        private ReadOnlyCollection<View> LogicalChildren => _logicalChildren ?? (_logicalChildren = new ReadOnlyCollection<View>((IList<View>)Children));

        private ReadOnlyCollection<View> _logicalChildren;
        private LayoutInformation layoutInformation = new LayoutInformation();


        private class LayoutInformation
        {
            public Size Constraint;
            public Rectangle[] Plots;
            public SizeRequest[] Requests;
            public Rectangle Bounds;
            public Size MinimumSize;
            public double CompressionSpace;
            public int Expanders;
        }

        protected override SizeRequest OnSizeRequest(double widthConstraint, double heightConstraint)
        {
            var height = 0.0;
            var minHeight = 0.0;
            var width = 0.0;
            var minWidth = 0.0;

            for (int i = 0; i < Children.Count; i++)
            {
                var child = (View)Children[i];
                // skip invisible children

                if (!child.IsVisible)
                    continue;
                var childSizeRequest = child.GetSizeRequest(double.PositiveInfinity, height);
                height = Math.Max(height, childSizeRequest.Minimum.Height);
                minHeight = Math.Max(minHeight, childSizeRequest.Minimum.Height);
                width += childSizeRequest.Request.Width;
                minWidth += childSizeRequest.Minimum.Width;
            }

            return new SizeRequest(new Size(width, height), new Size(minWidth, minHeight));
        }
    }
}
