using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;
using RehabMakerV2.Utils;

namespace RehabMakerV2.Pages
{
    public partial class MainPage : ContentPage
    {
        public MainPage()
        {
            InitializeComponent();

            BletoothRM.Source = ImageSource.FromResource("RehabMakerV2.Picture.bletooth.png");
            LogoRM.Source = ImageSource.FromResource("RehabMakerV2.Picture.logo_min_svg.png");
            LogoRM.HorizontalOptions = LayoutOptions.Center;
            button3.Source = ImageSource.FromResource("RehabMakerV2.Picture.brows.png");
            button4.Source = ImageSource.FromResource("RehabMakerV2.Picture.brows.png");
            StartRM.Source = ImageSource.FromResource("RehabMakerV2.Picture.start_svg.png");
            StartRM.Opacity = 0;
            StopRM.Source = ImageSource.FromResource("RehabMakerV2.Picture.stop.png");
            StopRM.IsVisible = false;
            button1.BackgroundColor = Color.Red;
            button1.TextColor = Color.White;
            stackLayout2.IsVisible = false;
            DatePicker1.IsEnabled = false;
        }
        Random rnd = new Random();

        private void button1_clicked(object sender, EventArgs e)
        {
            button1.BackgroundColor = Color.Red;
            button1.TextColor = Color.White;
            button2.BackgroundColor = Color.White;
            button2.TextColor = Color.Red;
            stackLayout1.IsVisible = true;
            stackLayout2.IsVisible = false;
            DatePicker1.IsVisible = false;
        }
        private void button2_clicked(object sender, EventArgs e)
        {

            button2.BackgroundColor = Color.Red;
            button2.TextColor = Color.White;
            button1.BackgroundColor = Color.White;
            button1.TextColor = Color.Red;
            stackLayout1.IsVisible = false;
            stackLayout2.IsVisible = true;
            DatePicker1.IsVisible = true;
        }
        private async void button3_clicked(object sender, EventArgs e)
        {
            //await Navigation.PushAsync(new LoginInToShare());

        }

        private async void BletoothRM_clicked(object sender, EventArgs e)
        {

            activityIndicator.IsRunning = true;

            try
            {
                //await BletoothRM.ScaleTo(0.9, 1500, Easing.Linear);
                string LastDevices = Settings.LastUsedDevices;

                string Get = "";
                try
                {
                    Get = GETApi("api/web/ok/1");
                }
                catch
                {
                    label1.Text = "Server is not available. Check your Internet connection,";
                    activityIndicator.IsRunning = false;
                }
                if (Get == "\"Oks\"")
                {
                    //await BletoothRM.ScaleTo(1, 2000, Easing.Linear);
                    if (LastDevices == null)
                    {

                        activityIndicator.IsRunning = false;
                        await DisplayAlert("Device error", "Check the device number in 'Settings'", "Ok");
                        return;
                    }
                    string json13 = GETApi("api/params/paraparams?id=" + LastDevices + "&simbol=1&ugo=ugo");
                    AverageJsonParams JParams = Newtonsoft.Json.JsonConvert.DeserializeObject<AverageJsonParams>(json13);
                    AvSpeed.Text = JParams.AverageSpeed.ToString();
                    AvCal.Text = JParams.TotalDistance.ToString();
                    AvDis.Text = JParams.TotalCalories.ToString();
                    await stackLayout3.FadeTo(0, 1000);
                    stackLayout3.IsVisible = false;
                    await StartRM.FadeTo(1, 1000);
                    DatePicker1.IsEnabled = true;
                    activityIndicator.IsRunning = false;
                }
                else
                    await DisplayAlert("Error", "Server is not available. Check your Internet connection", "Ok");
                activityIndicator.IsRunning = false;
            }
            catch
            {
                await DisplayAlert("Device error", "Check the device number in 'Settings'", "Ok");
                activityIndicator.IsRunning = false;
            }
        }

        private static string GETApi(string Data)
        {
            string Url = "http://rehabmaker-001-site1.dtempurl.com";
            string Out = "";

            try
            {
                System.Net.WebRequest req = System.Net.WebRequest.Create(Url + "/" + Data);
                System.Net.WebResponse resp = req.GetResponse();
                System.IO.Stream stream = resp.GetResponseStream();
                System.IO.StreamReader sr = new System.IO.StreamReader(stream);
                Out = sr.ReadToEnd();
                sr.Close();
            }
            catch (Exception ex)
            {

            }
            return Out;
        }

        private void StartRM_clicked(object sender, EventArgs e)
        {

            activityIndicator.HorizontalOptions = LayoutOptions.FillAndExpand;

            StartRM.IsVisible = false;
            string LastDevices = Settings.LastUsedDevices;
            string json = "";
            string Get = "";
            try
            {
                Get = GETApi("api/params/paraparams?id=" + LastDevices + "&simbol=0&ugo=ugo");
            }
            catch
            {
                DisplayAlert("Server is not available", "Check your Internet connection", "Ok");
            }
            if (Get != null)
            {
                StopRM.IsVisible = true;
                ApiConnect(LastDevices, json, 1);
            }
            else
                DisplayAlert("Device error", "Check the device number in 'Settings'", "Ok");
        }

        private async void StopRM_clicked(object sender, EventArgs e)
        {
            StartRM.IsVisible = true;
            StopRM.IsVisible = false;
            await StopRM.RotateTo(0, 0);
        }
        private async void ApiConnect(string LastDevices, string date, int destin)
        {

            try
            {
                if (destin == 0)
                {
                    string json4Date = GETApi("api/params/paraparams?id=" + LastDevices + "&simbol=2&ugo=" + date);
                    AverageJsonParams JParamss = Newtonsoft.Json.JsonConvert.DeserializeObject<AverageJsonParams>(json4Date);
                    AvDateSp.Text = JParamss.AverageSpeed.ToString();
                    AvDateDist.Text = JParamss.TotalDistance.ToString();
                    AvDateCal.Text = JParamss.TotalCalories.ToString();
                }
            }
            catch
            {
                await DisplayAlert("Error", "No data found for this date", "Ok");
            }
            while (StopRM.IsVisible == true)
            {
                try
                {
                    if (destin != 0)
                    {
                        string json = GETApi("api/params/paraparams?id=" + LastDevices + "&simbol=0&ugo=ugo");
                        JsonParams JParams = Newtonsoft.Json.JsonConvert.DeserializeObject<JsonParams>(json);
                        LSpeed.Text = JParams.Speed.ToString();
                        LDistance.Text = JParams.Distance.ToString();
                        LCalories.Text = JParams.Сalories.ToString();

                        string json4Average = GETApi("api/params/paraparams?id=" + LastDevices + "&simbol=1&ugo=ugo");

                        AverageJsonParams JParamss = Newtonsoft.Json.JsonConvert.DeserializeObject<AverageJsonParams>(json4Average);
                        AvSpeed.Text = JParamss.AverageSpeed.ToString();
                        AvCal.Text = JParamss.TotalDistance.ToString();
                        AvDis.Text = JParamss.TotalCalories.ToString();

                    }


                    int i = rnd.Next(1, 3);
                    if (i == 1)
                    {
                        await StopRM.RotateTo(720, 4000, Easing.CubicInOut);
                        await StopRM.RotateTo(0, 0);
                    }
                    if (i == 2)
                    {
                        await StopRM.RotateTo(1080, 6000, Easing.CubicInOut);
                        await StopRM.RotateTo(0, 0);
                    }
                }
                catch
                {
                    StopRM.IsVisible = false;
                    StartRM.IsVisible = true;
                    await DisplayAlert("Server is not available", "Check your Internet connection", "Ok");
                    break;
                }
            }
        }
        private void OnDateSelected(object sender, DateChangedEventArgs e)
        {
            string date = e.NewDate.ToString("MM/dd/yyyy");

            try
            {
                string LastDevices = Settings.LastUsedDevices;
                DateTime data = Convert.ToDateTime(date);
                date = data.ToShortDateString();
                date = date.Replace("/", ".");
                ApiConnect(LastDevices, date, 0);
            }
            catch
            {
                label1.Text = "Server is not available. Check your Internet connection,";
            }


        }

        private async void buttonviewdate_clicked(object sender, EventArgs e)
        {
            string LastDevices = Settings.LastUsedDevices;
            string date = DatePicker1.Date.ToString("MM/dd/yyyy");
            try
            {
                if (date[0] == Convert.ToChar("0"))
                {
                    date = date.Remove(0, 1);
                }
                date = date.Replace("/", ".");
                ApiConnect(LastDevices, date, 0);
            }
            catch
            {
                await DisplayAlert("Error", "Server is not available", "Ok");
            }
        }
    }
}
