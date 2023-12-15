namespace SoftLineTestApp.Models
{
    public class Task
    {
        public int ID { get; }
        public string Name { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;
        public int Status_ID { get; }
    }
}
