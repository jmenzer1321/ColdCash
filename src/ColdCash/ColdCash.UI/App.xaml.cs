using ColdCash.Data;
using ColdCash.Services;
using ColdCash.UI.Views;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.UI.Xaml;
using Microsoft.UI.Xaml.Controls;


namespace ColdCash.UI
{
    public partial class App : Application
    {
        private Window? m_window;

        public App()
        {
            this.InitializeComponent();
        }

        protected override void OnLaunched(LaunchActivatedEventArgs args)
        {
            m_window = new Window();
            m_window.Content = new Frame();
            ((Frame)m_window.Content).Navigate(typeof(LoginPage));
            m_window.Activate();
        }
    }
}