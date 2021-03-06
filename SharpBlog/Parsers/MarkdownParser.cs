﻿using System.Collections.Generic;
using System.IO;
using SharpBlog.Models;
using CommonMark;

namespace SharpBlog.Parsers
{
    public static class MarkdownParser
    {
        private const string YAML_DELIMITER = "---";

        public static List<Post> ParseFiles(string path)
        {
            var posts = new List<Post>();
            string[] files = Directory.GetFiles(path, "*.md");

            foreach (var file in files)
            {
                posts.Add(ParseFile(file));
            }

            return posts;
        }

        public static Post ParseFile(string path)
        {
            if (!File.Exists(path))
                return null;

            var fileInfo = new FileInfo(path);

            using (var reader = fileInfo.OpenText())
            {
                string content = reader.ReadToEnd();

                Post post = ParsePost(content);
                post.Slug = Path.GetFileNameWithoutExtension(fileInfo.Name);

                return post;
            }
        }

        public static Post ParsePost(string content)
        {
            int yamlStart = content.IndexOf(YAML_DELIMITER) + YAML_DELIMITER.Length;
            int yamlEnd = content.IndexOf(YAML_DELIMITER, yamlStart);

            string yaml = content.Substring(yamlStart, yamlEnd - yamlStart);
            string body = content.Substring(yamlEnd + YAML_DELIMITER.Length);

            Post post = YamlParser.ParsePostHeader(yaml);
            post.Body = CommonMarkConverter.Convert(body);
            
            return post;
        }
    }
}