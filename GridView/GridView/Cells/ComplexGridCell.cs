using Xamarin.Forms;

namespace GridView
{
    public class ComplexGridCell : Grid
    {
        public ComplexGridCell()
        {
            SetupUserInterface();
        }

        private void SetupUserInterface()
        {
            _back = new Image()
            {
                Source = "http://cdn.hasinstinct.com/2015/10/27/cute-kitty-cats-adorable-lil-kittens-cute.jpg",
                Aspect = Aspect.Fill
            };

            Grid.SetRow(_back, 0);
            Grid.SetRowSpan(_back, 2);
            Grid.SetColumn(_back, 0);

            _soundLabel = new Label()
            {
                Text = "Hello Kitty"
            };

            var textLayout = new StackLayout()
            {
                Padding = new Thickness(15, 10),
                Spacing = 6,
                VerticalOptions = LayoutOptions.Start,
                Children =
                {
                    _soundLabel,
                },
            };
            Grid.SetRow(textLayout, 0);
            Grid.SetColumn(textLayout, 0);

            var content = new Grid()
            {
                Padding = 0,
                RowSpacing = 0,
                ColumnSpacing = 0,
                BackgroundColor = Color.White,

                RowDefinitions = new RowDefinitionCollection()
                {
                    new RowDefinition(),
                    new RowDefinition { Height = 20 }
                }
            };
            content.Children.Add(_back);
            content.Children.Add(textLayout);
            
            Grid.SetRow(content, 0);
            Grid.SetRowSpan(content, 2);
            Grid.SetColumn(content, 0);
            Grid.SetColumnSpan(content, 2);

            Padding = 0;
            RowSpacing = 0;
            ColumnSpacing = 0;
            Children.Add(content);
        }

        #region Fields

        private Label _soundLabel;

        private static Image _back;
        #endregion // Fields
    }
}
