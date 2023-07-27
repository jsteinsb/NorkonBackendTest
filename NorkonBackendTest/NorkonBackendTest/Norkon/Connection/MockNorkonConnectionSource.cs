namespace NorkonBackendTest.Norkon.Connection
{
    public class MockNorkonConnectionSource : INorkonConnectionSource
    {
        private readonly IConfiguration _config;
        private readonly Random _rnd;

        public MockNorkonConnectionSource(IConfiguration config)
        {
            _config = config;
            _rnd = new Random();
        }

        public async IAsyncEnumerable<int> GetConnectionCount([EnumeratorCancellation] CancellationToken cancellationToken)
        {
            while (!cancellationToken.IsCancellationRequested)
            {
                await Task.Delay(500, cancellationToken);
                yield return _rnd.Next();
            }
        }
    }
}
