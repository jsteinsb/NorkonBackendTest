namespace NorkonBackendTest.Norkon.Connection
{
    public interface INorkonConnectionSource
    {
        public IAsyncEnumerable<int> GetConnectionCount(CancellationToken cancellationToken);
    }
}
