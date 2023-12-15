using System.Collections.Generic;

namespace SoftLine_TA.Models
{
    public class Status
    {
        public int Status_ID { get; set; }
        public string Status_name { get; set; } = string.Empty;

        public virtual List<Task> Tasks { get; set; }
    }
}
