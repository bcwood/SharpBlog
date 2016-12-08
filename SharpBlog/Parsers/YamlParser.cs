using System;
using System.Collections.Generic;
using System.Linq;
using SharpBlog.Models;

namespace SharpBlog.Parsers
{
    public static class YamlParser
    {
        public static Post ParsePostHeader(string text)
        {
            string[] lines = text.Split('\n');
            var post = new Post();

            foreach (string line in lines)
            {
                int splitIndex = line.IndexOf(":");
                if (splitIndex < 0)
                    continue;

                string key = line.Substring(0, splitIndex).Trim();
                string value = line.Substring(splitIndex + 1).Trim();

                switch (key)
                {
                    case "title":
                        post.Title = value;
                        break;
                    case "date":
                        post.Date = DateTime.Parse(value);
                        break;
                    case "tags":
                        var tags = value.Split(',').Select(p => p.Trim()).ToList();
                        foreach (string tag in tags)
                        {
                            post.Tags.Add(new Tag(tag));
                        }
                        break;
                    case "published":
                        post.IsPublished = bool.Parse(value);
                        break;
                    case "order":
                        post.Order = int.Parse(value);
                        break;
                }
            }

            return post;
        }
    }
}