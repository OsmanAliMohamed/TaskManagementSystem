using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TaskManagementSystem.Models.Models
{
    public class AuditTrail
    {
        public int AuditTrailId { get; set; }
        public string Action { get; set; }
        public DateTime Timestamp { get; set; }
        public int TaskId { get; set; }
        public Task Task { get; set; }
        public string UserId { get; set; }
        public User User { get; set; }
    }
}
