using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TaskManagementSystem.Models.Models
{
    public class Attachment
    {
        public int AttachmentId { get; set; }
        public string FilePath { get; set; }
        public string FileName { get; set; }
        public int TaskId { get; set; }
        public Task Task { get; set; }
    }
}
