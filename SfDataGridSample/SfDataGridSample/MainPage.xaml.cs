using SfDataGridSample.Helper;

namespace SfDataGridSample
{
    public partial class MainPage : ContentPage
    {
        public MainPage()
        {
            InitializeComponent();
            sfGrid.CellRenderers.Remove("Text");
            sfGrid.CellRenderers.Add("Text", new CustomTextRenderer());
        }
    }
}
