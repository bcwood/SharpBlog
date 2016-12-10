using NUnit.Framework;
using SharpBlog.Models;

namespace SharpBlog.Tests.Models
{
    [TestFixture]
    public class TagTests
    {
        [TestCase("tag1", ExpectedResult = "tag1")]
        [TestCase("tag 2", ExpectedResult = "tag-2")]
        [TestCase(" ~ My cR@zy  tag! ", ExpectedResult = "my-crzy-tag")]
        public string Slug_Tests(string name)
        {
            Tag tag = new Tag(name);
            return tag.Slug;
        }
    }
}
