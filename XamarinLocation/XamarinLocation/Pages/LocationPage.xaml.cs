using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Plugin.Geolocator;
using Xamarin.Forms;

namespace XamarinLocation.Pages
{
    public partial class LocationPage : ContentPage
    {

        

        public LocationPage()
        {
            InitializeComponent();

            btnLocation.Clicked += BtnLocation_Clicked;

            var locator = CrossGeolocator.Current;

           
        }

        private async void BtnLocation_Clicked(object sender, EventArgs e)
        {

            var locator = CrossGeolocator.Current;

            locator.PositionChanged += Locator_PositionChanged;



            try
            {
                ShowLoading(true, btnLocation);

                var position = await locator.GetPositionAsync(timeoutMilliseconds: 10000);


                lblLatitude.Text = position.Latitude.ToString();
                lblLongitude.Text = position.Longitude.ToString();


            }
            catch (Exception ex)
            {

                this.DisplayAlert("Error", ex.Message, "Ok");
            }
            finally
            {
                ShowLoading(false, btnLocation);
            }


        }

        private void Locator_PositionChanged(object sender, Plugin.Geolocator.Abstractions.PositionEventArgs e)
        {
            var position = e.Position;

            lblLatitude.Text = position.Latitude.ToString();
            lblLongitude.Text = position.Longitude.ToString();
        }

        public void ShowLoading(bool isLoading, Button btn = null)
        {
            actPiece.IsRunning = isLoading;
            actPiece.IsEnabled = isLoading;
            actPiece.IsVisible = isLoading;

            if (btn != null)
                btn.IsEnabled = !isLoading;
        }
    }
}
