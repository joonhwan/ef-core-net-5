using System;
using System.Collections.Generic;

namespace BlogApp.Domain
{
    public class Blog
    {
        public Blog()
        {
            Posts = new List<Post>();
        }
        
        public int Id { get; set; }
        public string Name { get; set; }
        public string Url { get; set; }
        public List<Post> Posts { get; set; }

        public override string ToString()
        {
            return $"{nameof(Id)}: {Id}, {nameof(Name)}: {Name}, {nameof(Url)}: {Url}, {nameof(Posts)}: {Posts.Count} ea";
        }
    }
}