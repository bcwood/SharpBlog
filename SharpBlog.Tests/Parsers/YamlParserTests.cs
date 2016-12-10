using System;
using NUnit.Framework;
using SharpBlog.Models;
using SharpBlog.Parsers;

namespace SharpBlog.Tests.Parsers
{
    [TestFixture]
    public class YamlParserTests
    {
        private const string YAML_VALID = 
            @"title: Test Post 1
              date: 2016-11-05 9:00 AM
              tags: tag1, tag2, tag3";

        private const string YAML_INVALID =
            @"title - Test Post 2";

        [Test]
        public void Parse_ValidYaml()
        {
            Post post = YamlParser.ParsePostHeader(YAML_VALID);

            Assert.AreEqual("Test Post 1", post.Title);
            Assert.AreEqual(DateTime.Parse("2016-11-05 9:00 AM"), post.Date);
            Assert.AreEqual(3, post.Tags.Count);
        }

        [Test]
        public void Parse_InvalidYaml()
        {
            Post post = YamlParser.ParsePostHeader(YAML_INVALID);

            Assert.IsNull(post.Title);
            Assert.IsNull(post.Date);
            Assert.AreEqual(0, post.Tags.Count);
        }
    }
}
