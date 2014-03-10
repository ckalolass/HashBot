using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Navigation;
using Microsoft.Phone.Controls;
using Microsoft.Phone.Shell;
using Cirrious.MvvmCross.WindowsPhone.Views;
using HashBot.Core.ViewModels;

namespace HashBot.WP.Views
{
    public partial class MainView : MvxPhonePage
    {
        public MainView()
        {
            InitializeComponent();
        }

        private void btnInfoClick(object sender, EventArgs e)
        {
            MainViewModel vm = this.ViewModel as MainViewModel;
            if (vm != null)
            {
                vm.GoToInfoCommand.Execute(null);
            }
        }

        private void btnMoreTap(object sender, System.Windows.Input.GestureEventArgs e)
        {
            MainViewModel vm = this.ViewModel as MainViewModel;
            if (vm != null)
            {
                vm.LoadTweetsCommand.Execute(null);
            }
        }

        private void llsSelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            LongListSelector lls = sender as LongListSelector;
            if (lls != null && lls.SelectedItem != null)
            {
                MainViewModel vm = this.ViewModel as MainViewModel;
                if (vm != null)
                {
                    vm.GoToTweetCommand.Execute(lls.SelectedItem);
                }
                lls.SelectedItem = null;
            }
        }
    }
}