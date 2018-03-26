using Plugin.Connectivity;

namespace Catalog.Services.Networks
{
    public class NetworkService : INetworkService
    {
        public bool IsInternetConnected
        {
            get
            {
                //if (!CrossConnectivity.IsSupported)
                //{
                //    return true;
                //}

                var connectivity = CrossConnectivity.Current;

                try
                {
                    return connectivity.IsConnected;
                }
                finally
                {
                    CrossConnectivity.Dispose();
                }
            }
        }
    }
}
