using System;

namespace TodoApi.Models
{
    public class TodoItem
    {
        public Guid Id { get; set; } = Guid.NewGuid();
        public string Title { get; set; }
        public string Content { get; set; }
        public int State { get; set; } // 1 = open, 2 = in-progress, 3 = finished
    }
}