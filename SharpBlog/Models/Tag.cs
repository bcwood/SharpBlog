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
                string slug = Name.ToLower();
                slug = Regex.Replace(slug, @"[^0-9a-z\s\-]", ""); // remove everything but 0-9, a-z, space and hyphen
                slug = slug.Trim(); // remove any remaining outside spaces
                slug = Regex.Replace(slug, @"\s+", "-"); // replace space(s) between words with single hyphen
                
                return slug;
            }
        }

        public Tag(string name)
        {
            Name = name;
        }
    }
}