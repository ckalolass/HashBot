using Cirrious.MvvmCross.WindowsPhone.Views;
using HashBot.Core.ViewModels;
using Microsoft.Phone.Controls;
using System;
using System.Windows.Controls;

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