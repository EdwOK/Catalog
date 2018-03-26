namespace Catalog.Services.Networks
{
    public interface INetworkService
    {
        bool IsInternetConnected { get; }
    }
}
