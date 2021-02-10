using System;

namespace PaginationDemo.Data.Entities
{
    public class Hobby
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public DateTime CreatedAt { get; set; }
    }
}
