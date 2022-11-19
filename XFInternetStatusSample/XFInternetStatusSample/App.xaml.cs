using System;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace XFInternetStatusSample
{
    public partial class App : Application
    {
        private static App _instance;

        public static App Instance
        {
            get { return _instance; }
            set { _instance = value; }
        }

        public App ()
        {
            InitializeComponent();
            Instance = this;
            MainPage = new NavigationPage(new MainPage());

            if (!NetworkConnectivity.CheckConnection())
                SetInternetConnectionView(false);
        }

        protected override void OnStart ()
        {
            // Register Network Connectivity Change Event
            NetworkConnectivity networkConnectivityHelper = new NetworkConnectivity();
        }

        protected override void OnSleep ()
        {
        }

        protected override void OnResume ()
        {
            if (!NetworkConnectivity.CheckConnection())
                SetInternetConnectionView(false);
        }

        #region Set Internet Connection View

        public void SetInternetConnectionView(bool isInternetAvail)
        {
            Device.BeginInvokeOnMainThread(() =>
            {
                if (isInternetAvail)
                {
                    App.Current.Resources["InternetConnTemplate"] = App.Current.Resources["OnlineTemplate"];

                    StartTimerFor4Second();
                }
                else
                {
                    App.Current.Resources["InternetConnTemplate"] = App.Current.Resources["OfflineTemplate"];
                }
            });
        }

        private static void RemoveTopInternetConnView()
        {
            App.Current.Resources["InternetConnTemplate"] = App.Current.Resources["EmptyTemplate"];
        }

        private void StartTimerFor4Second()
        {
            System.Timers.Timer timer = new System.Timers.Timer();
            timer.Interval = 4000;
            timer.Enabled = true;
            timer.Elapsed += OnTimedEvent;
        }

        private static void OnTimedEvent(Object source, System.Timers.ElapsedEventArgs e)
        {
            try
            {
                (source as System.Timers.Timer).Enabled = false;
                (source as System.Timers.Timer).Dispose();

                Device.BeginInvokeOnMainThread(() =>
                {
                    RemoveTopInternetConnView();
                });
            }
            catch (Exception ex)
            {
            }

        }

        #endregion
    }
}

