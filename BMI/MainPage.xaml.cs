using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Navigation;
using Microsoft.Phone.Controls;
using Microsoft.Phone.Shell;
using BMI.Resources;
using System.Diagnostics;


namespace BMI
{

    public partial class MainPage : PhoneApplicationPage
    {

        public decimal bmi, weight, height;

        // Constructor
        public MainPage()
        {
            InitializeComponent();
            EUstandard.IsChecked = true;
            unit_1.Text = "KG";
            unit_2.Text = "CM";
            
            // Sample code to localize the ApplicationBar
            //BuildLocalizedApplicationBar();
        }

        private void BtnCalculateBmi(object sender, RoutedEventArgs e)
        {
            if (isValidData())
            {
                if (EUstandard.IsChecked == true)
                {
                    weight = decimal.Parse(Weight.Text);
                    height = decimal.Parse(Height.Text);
                    bmi = Math.Round(weight / (height * height) * 10000, 1, MidpointRounding.AwayFromZero);
                }
                else if (ENstandard.IsChecked == true)
                {
                    weight = decimal.Parse(Weight.Text);
                    height = decimal.Parse(Height.Text);
                    bmi = Math.Round(weight / (height * height) * 703, 1, MidpointRounding.AwayFromZero);
                }


                string uri = "/TheResultScreen.xaml?Text=";
                uri += bmi.ToString();
                NavigationService.Navigate(new Uri(uri, UriKind.Relative));
            }
         }

        public bool isValidData()
        {
            return
             IsPresent(Weight, "Weight")
             && IsPresent(Height, "Height")
             && IsDecimal(Weight, "Weight")
             && IsDecimal(Height, "Height")
             && IsWithinRange(Weight, "Weight", 1, 2500)
             && IsWithinRange(Height, "Height", 1, 1000);
             //&& IsOnChecked(EUstandard, "EU Standard or EN Standard")
             //&& IsOnChecked(ENstandard, "EN Standard");
        }

        //private bool IsOnChecked(RadioButton EUstandard, string p)
        //{
        //    if (EUstandard.IsChecked == false)
        //    {
        //        MessageBox.Show(p + " should be checked. Entry Error.");
        //        EUstandard.Focus();
        //        return false;
        //    }
        //    return true;
        //}

        private bool IsPresent(TextBox Weight, string p)
        {
            if (Weight.Text == "")
            {
                MessageBox.Show(p + " should not be empty. Entry Error.");
                Weight.Focus();
                return false;
            }
            return true;
        }

        private bool IsDecimal(TextBox Weight, string p)
        {
            try
            {
                Convert.ToInt64(Weight.Text);
                return true;
            }
            catch (FormatException)
            {
                MessageBox.Show(p + " must be a integer value." + "eg. 1234. Without a comma. Invalid Data Error");
                Weight.Focus();
                return false;
            }
        }

        private bool IsWithinRange(TextBox Weight, string p1, int min, int max)
        {
            decimal digit = Convert.ToDecimal(Weight.Text);
            if (digit < min || digit > max)
            {
                MessageBox.Show(p1 + " must be between " + min + " and " + max + "!. Range Error.");
                Weight.Focus();
                return false;
            }
            return true;
        }


        private void click_EU(object sender, RoutedEventArgs e)
        {
            EUstandard.IsChecked = true;
            unit_1.Text = "KG";
            unit_2.Text = "CM";
        }

        private void click_EN(object sender, RoutedEventArgs e)
        {
            ENstandard.IsChecked = true;
            unit_1.Text = "POUNDS";
            unit_2.Text = "INCHES";
        }

        protected override void OnBackKeyPress(System.ComponentModel.CancelEventArgs e)
        {
            if (MessageBox.Show("Are you sure you want to exit?", "Confirm Exit?",
                                    MessageBoxButton.OKCancel) != MessageBoxResult.Cancel)
            {
                Application.Current.Terminate();
            }
            else
            {
                e.Cancel = true;
            }
        }

        
        // Sample code for building a localized ApplicationBar
        //private void BuildLocalizedApplicationBar()
        //{
        //    // Set the page's ApplicationBar to a new instance of ApplicationBar.
        //    ApplicationBar = new ApplicationBar();

        //    // Create a new button and set the text value to the localized string from AppResources.
        //    ApplicationBarIconButton appBarButton = new ApplicationBarIconButton(new Uri("/Assets/AppBar/appbar.add.rest.png", UriKind.Relative));
        //    appBarButton.Text = AppResources.AppBarButtonText;
        //    ApplicationBar.Buttons.Add(appBarButton);

        //    // Create a new menu item with the localized string from AppResources.
        //    ApplicationBarMenuItem appBarMenuItem = new ApplicationBarMenuItem(AppResources.AppBarMenuItemText);
        //    ApplicationBar.MenuItems.Add(appBarMenuItem);
        //}
    }
}