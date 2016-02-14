using Xamarin.Forms;

namespace GridView
{
    public class GridCell : Image
    {
        public GridCell()
        {
            //View = new Image()
            //{
            Source = "https://pp.vk.me/c629213/v629213547/388ff/dgVYO6xG76I.jpg";
            WidthRequest = 100;
            HeightRequest = 100;
            //};
        }

        protected override SizeRequest OnSizeRequest(double widthConstraint, double heightConstraint)
        {
            return base.OnSizeRequest(widthConstraint, heightConstraint);
        }
    }
}