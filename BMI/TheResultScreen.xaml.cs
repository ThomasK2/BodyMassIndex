using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Windows;
using System.Windows.Controls;
using Microsoft.Phone.Controls;
using Microsoft.Phone.Shell;
using System.Windows.Navigation;
using System.Windows.Media;

namespace BMI
{
    public partial class TheResultScreen : PhoneApplicationPage
    {
        
        public TheResultScreen()
        {
            InitializeComponent();
        }

        private void BtnBack(object sender, RoutedEventArgs e)
        {
            NavigationService.Navigate(new Uri("/MainPage.xaml", UriKind.Relative));
            
        }

        protected override void OnNavigatedTo(System.Windows.Navigation.NavigationEventArgs e)
        {
            base.OnNavigatedTo(e);
            IDictionary<string, string> parameters = this.NavigationContext.QueryString;
            if (parameters.ContainsKey("Text"))
            {
                Result.Text = parameters["Text"];
            }
        }

        protected override void OnBackKeyPress(System.ComponentModel.CancelEventArgs e)
        {
            string caption = "Confirm Exit?";
            string message = "Are you sure you want to exit?";
            e.Cancel = MessageBoxResult.Cancel == MessageBox.Show(message, caption, MessageBoxButton.OKCancel);
            NavigationService.RemoveBackEntry();
            base.OnBackKeyPress(e);
        }
    }
}