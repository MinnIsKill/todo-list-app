using System;

namespace TodoApi.Models
{
    public class TodoItem
    {
        public string Id { get; set; }
        public string Title { get; set; }
        public string Content { get; set; }
        public long State { get; set; }

        // Parameterless constructor for Dapper
        public TodoItem() { }
    }
}