using System.Text.RegularExpressions;

namespace SharpBlog.Models
{
    public class Tag
    {
        public string Name { get; set; }

        public string Slug
        {
            get
            {
                string slug = Name.ToLower().Replace(" ", "-");
                slug = Regex.Replace(slug, @"[^0-9a-z\-]", "");

                return slug;
            }
        }

        public Tag(string name)
        {
            Name = name;
        }
    }
}