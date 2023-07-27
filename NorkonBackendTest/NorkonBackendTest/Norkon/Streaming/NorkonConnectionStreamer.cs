namespace NorkonBackendTest.Norkon.Streaming
{
    public class NorkonConnectionStreamer : INorkonConnectionStreamer
    {
        private readonly INorkonConnectionSource _connectionSource;

        public NorkonConnectionStreamer(INorkonConnectionSource connectionSource)
        {
            _connectionSource = connectionSource;
        }

        public async IAsyncEnumerable<UpdateInfo> StreamUpdates([EnumeratorCancellation] CancellationToken cancellationToken)
        {
            var updateNumber = 0;
            await foreach (var connectionCount in _connectionSource.GetConnectionCount(cancellationToken))
            {
                yield return new UpdateInfo
                {
                    UpdateNumber = updateNumber++,
                    NumberOfConnections = connectionCount,
                    TimeStamp = DateTime.UtcNow
                };
            }
        }
    }
}
