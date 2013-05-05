namespace PodcastUtilities.PortableDevices
{
    public interface IDevice
    {
        string Id { get; }
        string Name { get; }

        IDeviceObject GetObjectFromPath(string path);
        void Delete(string path);
    }
}