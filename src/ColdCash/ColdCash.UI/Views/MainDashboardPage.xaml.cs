using Microsoft.UI.Xaml.Controls;
using Microsoft.UI.Xaml;

namespace ColdCash.UI.Views
{
    public sealed partial class MainDashboardPage : Page
    {
        public MainDashboardPage()
        {
            InitializeComponent();
        }

        private void Accounts_Click(object sender, RoutedEventArgs e)
            => Frame.Navigate(typeof(AccountsPage));
    }
}