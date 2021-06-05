using System;
using System.Collections.Generic;

namespace BlogApp.Domain
{
    public class Blog
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Url { get; set; }
        public List<Post> Posts { get; set; }
    }
}