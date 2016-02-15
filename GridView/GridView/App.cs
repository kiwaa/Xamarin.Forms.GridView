using System.Collections.Generic;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace GridView
{
    public class App : Application
    {


        public App()
        {
            // The root page of your application
            MainPage = new NavigationPage(CreatePage());
        }

        private ContentPage CreatePage()
        {
            return new ContentPage
            {
                Content = new StackLayout
                {
                    VerticalOptions = LayoutOptions.Start,
                    Spacing = 5,
                    Children =
                    {
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
            return new Label
            {
                XAlign = TextAlignment.Center,
                Text = "Welcome to Grid View sample!"
            };
        }


        private static GridView CreateGridView(int child)
        {
            var gridView = new GridView()
            {
                ItemHeight = 50,
                RowSpacing = 5,
                ColumnSpacing = 5,
                MaxItemsPerRow = 3,
                ItemTemplate = new DataTemplate(typeof(GridCell)),
                ItemsSource = GetItems(child),
            };
            return gridView;
        }

        private static IEnumerable<GridCellViewModel> GetItems(int count)
        {
            for (int i = 0; i < count; i++)
                yield return new GridCellViewModel();
        }
    }
}
