namespace NorkonBackendTest.Norkon.Streaming
{
    public interface INorkonConnectionStreamer
    {
        public IAsyncEnumerable<UpdateInfo> StreamUpdates(CancellationToken cancellationToken);
    }
}
