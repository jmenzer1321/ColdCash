using ColdCash.Core.Models;
using ColdCash.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.UI.Xaml.Controls;
using Microsoft.UI.Xaml.Navigation;
using System;
using ColdCash.Services;

namespace ColdCash.UI.Views
{
    public sealed partial class AddEditAccountPage : Page
    {
        private readonly BudgetDbContext _db;
        private Account? _editing;

        public AddEditAccountPage()
        {
            InitializeComponent();
            var services = new ServiceCollection();
            services.AddBudgetServices();
            _db = services.BuildServiceProvider()
                          .GetRequiredService<BudgetDbContext>();
        }

        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            if (e.Parameter is Account acc)
            {
                _editing = acc;
                NameBox.Text = acc.Name;
                BalanceBox.Text = acc.CurrentBalance.ToString();
                CurrencyBox.Text = acc.Currency;
            }
            else
            {
                CurrencyBox.Text = "USD";
            }
        }

        private void SaveButton_Click(object sender, Microsoft.UI.Xaml.RoutedEventArgs e)
        {
            if (string.IsNullOrWhiteSpace(NameBox.Text) ||
                !decimal.TryParse(BalanceBox.Text, out var bal))
            {
                return;
            }

            if (_editing == null)
            {
                _db.Accounts.Add(new Account
                {
                    Name = NameBox.Text,
                    CurrentBalance = bal,
                    Currency = CurrencyBox.Text
                });
            }
            else
            {
                _editing.Name = NameBox.Text;
                _editing.CurrentBalance = bal;
                _editing.Currency = CurrencyBox.Text;
            }

            _db.SaveChanges();
            Frame.Navigate(typeof(AccountsPage));
        }
    }
}