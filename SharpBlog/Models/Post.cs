using System;
using System.Collections.Generic;
using System.Text.RegularExpressions;

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
        public int Order { get; set; } = -1;

        public bool IsActive => IsPublished && Date <= DateTime.Now;
        public bool IncludeInMenu => Order >= 0;

        public string Excerpt
        {
            get
            {
                // strip HTML tags
                string excerpt = Regex.Replace(Body, "<.*?>", string.Empty);

                // truncate
                const int EXCERPT_LENGTH = 200;
                if (excerpt.Length > EXCERPT_LENGTH)
                    excerpt = excerpt.Substring(0, EXCERPT_LENGTH) + "...";

                return excerpt;
            }
        }

        public Post()
        {
            Tags = new List<string>();
        }
    }
}