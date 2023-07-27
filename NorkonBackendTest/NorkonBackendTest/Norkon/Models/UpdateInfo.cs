namespace NorkonBackendTest.Norkon.Models
{
    public class UpdateInfo
    {
        public DateTime TimeStamp;
        public int NumberOfConnections;
        public int UpdateNumber;

        public override string ToString()
        {
            return $"{UpdateNumber}|{TimeStamp}|{NumberOfConnections}";
        }
    }
}
