using Xamarin.Essentials;

namespace XFInternetStatusSample
{
    public class NetworkConnectivity
    {
        private bool IsInternetConnected;

        #region Constructor

        public NetworkConnectivity()
        {
            // Register for connectivity changes, be sure to unsubscribe when finished
            Connectivity.ConnectivityChanged += OnConnectivityChanged;

            IsInternetConnected = CheckConnection();
        }

        #endregion

        #region Destructor

        ~NetworkConnectivity()
        {
            Connectivity.ConnectivityChanged -= OnConnectivityChanged;
        }

        #endregion

        #region Handle Network Connection 

        public static bool CheckConnection()
        {
            var current = Connectivity.NetworkAccess;

            if (current == NetworkAccess.Internet)
                return true;
            else
                return false;
        }

        private void OnConnectivityChanged(object sender, ConnectivityChangedEventArgs e)
        {
            var access = e.NetworkAccess;
            var profiles = e.ConnectionProfiles;

            switch (access)
            {
                case NetworkAccess.Internet:

                    IsInternetConnected = true;
                    ShowInternetConnection(true);

                    break;

                case NetworkAccess.None:

                    IsInternetConnected = false;
                    ShowInternetConnection(false);

                    break;

                case NetworkAccess.Local:

                    IsInternetConnected = false;
                    ShowInternetConnection(false);

                    break;

                case NetworkAccess.Unknown:
                    System.Console.Write("A problem has occurred with your network connection.");
                    break;
                default:
                    break;
            }
        }

        private void ShowInternetConnection(bool isInternetAvail)
        {
            // Set Internet connection view in every page in header
            App.Instance.SetInternetConnectionView(isInternetAvail);
        }


        #endregion

    }
}

