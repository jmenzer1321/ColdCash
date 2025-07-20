using ColdCash.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.UI.Xaml;
using Microsoft.UI.Xaml.Controls;
using System.Linq;
using ColdCash.Services;

namespace ColdCash.UI.Views
{
    public sealed partial class AccountsPage : Page
    {
        private readonly BudgetDbContext _db;

        public AccountsPage()
        {
            InitializeComponent();
            var services = new ServiceCollection();
            services.AddBudgetServices();
            _db = services.BuildServiceProvider()
                          .GetRequiredService<BudgetDbContext>();
            LoadAccounts();
        }

        private void LoadAccounts()
        {
            AccountsList.ItemsSource = _db.Accounts.ToList();
        }

        private void AddAccountButton_Click(object sender, RoutedEventArgs e)
        {
            Frame.Navigate(typeof(AddEditAccountPage));
        }
    }
}