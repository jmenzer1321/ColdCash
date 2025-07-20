using ColdCash.Data;
using ColdCash.Services;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.UI.Xaml;
using Microsoft.UI.Xaml.Controls;
using System;
using System.IO;
using System.Linq;

namespace ColdCash.UI.Views
{
    public sealed partial class LoginPage : Page
    {
        private readonly BudgetDbContext _db;

        public LoginPage()
        {
            InitializeComponent();
            var services = new ServiceCollection();
            services.AddBudgetServices();
            _db = services.BuildServiceProvider()
                          .GetRequiredService<BudgetDbContext>();

            Loaded += (_, __) =>
            {
                // Show last remembered user if any
                var lastUser = _db.Users.FirstOrDefault();
                if (lastUser != null)
                {
                    UserNameBox.Text = lastUser.UserName;
                    WelcomeText.Text = $"Welcome back, {lastUser.UserName}!";
                    WelcomeText.Visibility = Visibility.Visible;
                }
            };
        }

        private void LoginButton_Click(object sender, RoutedEventArgs e)
        {
            var user = _db.Users.FirstOrDefault(u => u.UserName == UserNameBox.Text);
            if (user == null)
            {
                ShowError("User not found.");
                return;
            }

            if (!PasswordHelper.Verify(PasswordBox.Password, user.PasswordHash, user.Salt))
            {
                ShowError("Invalid password.");
                return;
            }

            Frame.Navigate(typeof(MainDashboardPage), user.Id);
        }

        private void RegisterButton_Click(object sender, RoutedEventArgs e)
        {
            if (string.IsNullOrWhiteSpace(UserNameBox.Text) || string.IsNullOrWhiteSpace(PasswordBox.Password))
            {
                ShowError("Username and password are required.");
                return;
            }

            if (_db.Users.Any(u => u.UserName == UserNameBox.Text))
            {
                ShowError("Username already exists.");
                return;
            }

            var (hash, salt) = PasswordHelper.Hash(PasswordBox.Password);
            var newUser = new Core.Models.User
            {
                UserName = UserNameBox.Text,
                PasswordHash = hash,
                Salt = salt
            };

            _db.Users.Add(newUser);
            _db.SaveChanges();

            Frame.Navigate(typeof(MainDashboardPage), newUser.Id);
        }

        private void ShowError(string msg)
        {
            ErrorText.Text = msg;
            ErrorText.Visibility = Visibility.Visible;
        }

        // simple password stub for MVP
        internal static class PasswordHelper
        {
            internal static (string hash, string salt) Hash(string pwd)
            {
                var salt = Guid.NewGuid().ToString();
                var hash = Convert.ToBase64String(
                    System.Text.Encoding.UTF8.GetBytes(pwd + salt));
                return (hash, salt);
            }

            internal static bool Verify(string pwd, string storedHash, string salt)
                => Hash(pwd).hash == storedHash;
        }
    }
}