﻿using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using AccuraNotes.DataModel;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;

// The Blank Page item template is documented at http://go.microsoft.com/fwlink/?LinkID=390556

namespace AccuraNotes
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class ViewNote : Page
    {
        Note note;

        public ViewNote()
        {
            this.InitializeComponent();
        }

        /// <summary>
        /// Invoked when this page is about to be displayed in a Frame.
        /// </summary>
        /// <param name="e">Event data that describes how this page was reached.
        /// This parameter is typically used to configure the page.</param>
        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            note = e.Parameter as Note;
            txtvTitle.Text = note.Title;
            txtvContent.Text = note.Content;
        }
        private void btnBack_Click(object sender, RoutedEventArgs e)
        {
            Frame.Navigate(typeof(MainPage));
        }

        private void btnViewMap_Click(object sender, RoutedEventArgs e)
        {
            Frame.Navigate(typeof(ViewLocation), note);
        }

        private async void btnExit_Click(object sender, RoutedEventArgs e)
        {
            Windows.UI.Popups.MessageDialog msgbox = new Windows.UI.Popups.MessageDialog("Are you sure you want to exit this page?");
            //Calling the Show method of MessageDialog class  
            //which will show the MessageBox  
            await msgbox.ShowAsync();
            Frame.Navigate(typeof(MainPage));
        }
    }
}
