using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Xamarin.Forms;

namespace GridView
{
    public class App : Application
    {
        public App()
        {
            // The root page of your application
            MainPage = new ContentPage
            {
                Content = new StackLayout
                {
                    VerticalOptions = LayoutOptions.Start,
                    Children = {
                        CreateFunnyLabel(),
                        CreateGridView(3),
                        CreateGridView(4),
                        CreateGridView(5),
                        CreateGridView(6),
                        CreateGridView(7)
                    }
                }
            };
        }

        private static Label CreateFunnyLabel()
        {
            return new Label {
                XAlign = TextAlignment.Center,
                Text = "Welcome to Grid View sample!"
            };
        }

        private static GridView CreateGridView(int child)
        {
            var gridView = new GridView()
            {
                MaxItemsPerRow = 3,
                //ItemTemplate = new DataTemplate(typeof(GridCell)),
                //ItemsSource = GetItems()
            };
            foreach (var item in GetItems(child))
                gridView.Children.Add(item);
            return gridView;
        }

        private static IEnumerable<GridCell> GetItems(int count)
        {
            for (int i = 0; i < count; i++)
                yield return new GridCell();
        }
    }
}
