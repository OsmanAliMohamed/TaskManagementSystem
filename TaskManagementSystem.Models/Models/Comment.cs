using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TaskManagementSystem.Models.Models
{
    public class Comment
    {
        public int CommentId { get; set; }
        public string Text { get; set; }
        public DateTime CreatedAt { get; set; }
        public int TaskId { get; set; }/*
        public Task Task { get; set; }*/
        public string UserId { get; set; }
        /*public User User { get; set; }*/
    }
}
