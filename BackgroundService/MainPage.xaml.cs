using Xamarin.Forms;

namespace BackgroundService
{
    public partial class MainPage : ContentPage
    {
        public MainPage()
        {
            InitializeComponent();
        }

        private void Button_Clicked(object sender, System.EventArgs e)
        {
            DependencyService.Get<IDemoService>().Start();
        }

        private void Button_Clicked_1(object sender, System.EventArgs e)
        {
            DependencyService.Get<IDemoService>().Stop();
        }
    }
}
