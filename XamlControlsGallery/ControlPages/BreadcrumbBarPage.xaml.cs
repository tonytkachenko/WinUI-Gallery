using Windows.UI.Xaml.Controls;


namespace AppUIBasics.ControlPages
{
    public sealed partial class BreadcrumbBarPage : Page
    {
 
    public BreadcrumbBarPage()
        {
            this.InitializeComponent();
            Breadcrumb1.ItemsSource = new string[] { "Home", "Documents", "Design", "Northwind", "Images" };
        }
    }
}
