using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.Foundation;
using Windows.Foundation.Collections;
using AccuraNotes.DataModel;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Controls.Maps;
using Windows.Devices.Geolocation;
using Windows.UI.Xaml.Navigation;
using Windows.UI.Popups;

// The Blank Page item template is documented at http://go.microsoft.com/fwlink/?LinkID=390556

namespace AccuraNotes
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class ViewLocation : Page
    {
        NavigationEventArgs args;
        string title, latitude, longitude;

        public ViewLocation()
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

            var note = e.Parameter as Note;
            title = note.Title;
            latitude = note.Latitude;
            longitude = note.Longitude;

        }

        private void map_Loaded(object sender, RoutedEventArgs e)
        {
            Geopoint geoPoint = new Geopoint(new BasicGeoposition()
            {
                Latitude = double.Parse(latitude),
                Longitude = double.Parse(longitude)
            });

            //add center position and zoom level to map
            NotesMap.Center = geoPoint;
            NotesMap.ZoomLevel = 19;
            // Add map elements
            MapIcon mapIcon = new MapIcon();
            mapIcon.Location = geoPoint;
            mapIcon.Title = title;
            NotesMap.MapElements.Add(mapIcon);

        }

        private void btnBack_Click(object sender, RoutedEventArgs e)
        {
            Frame.Navigate(typeof(MainPage));
        }

        private void btnOut_Click(object sender, RoutedEventArgs e)
        {

        }
        private void btnIn_Click(object sender, RoutedEventArgs e)
        {

        }
        private async void btnExit_Click(object sender, RoutedEventArgs e)
        {
            Windows.UI.Popups.MessageDialog msgbox = new MessageDialog("Are you sure you want to exit this page?");
            //Calling the Show method of MessageDialog class  
            //which will show the MessageBox  
            await msgbox.ShowAsync();
            Frame.Navigate(typeof(MainPage));
        }


    }
}
