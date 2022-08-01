namespace DevTrackR.API.Entities
{
    public class PackageUpdate
    {
        // Isso é um CONSTRUTOR para a string Status ---
        public PackageUpdate (string status, int packageId) {
            PackageId = packageId;
            Status = status;
            UpdateDate = DateTime.Now;
        }
        // ---
        public int Id { get; private set; }
        public int PackageId { get; private set; }
        public string Status { get; private set; }
        public DateTime UpdateDate { get; private set; }
    }
}