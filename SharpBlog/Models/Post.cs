using System;
using System.Collections.Generic;

namespace SharpBlog.Models
{
    public class Post
    {
        public string Title { get; set; }
        public string Slug { get; set; }
        public string Body { get; set; }
        public DateTime Date { get; set; }
        public List<string> Tags { get; set; }
        public bool IsPublished { get; set; } = true;

        public bool IsActive => IsPublished && Date <= DateTime.Now;

        public Post()
        {
            Tags = new List<string>();
        }
    }
}