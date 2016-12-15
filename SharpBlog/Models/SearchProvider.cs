using System.Collections.Generic;
using System.IO;
using System.Web;
using Lucene.Net.Analysis.Standard;
using Lucene.Net.Documents;
using Lucene.Net.Index;
using Lucene.Net.QueryParsers;
using Lucene.Net.Search;
using Lucene.Net.Store;
using Lucene.Net.Util;

namespace SharpBlog.Models
{
    public static class SearchProvider
    {
        //private static string _luceneDir = Path.Combine(HttpContext.Current.Request.PhysicalApplicationPath, "_luceneIndex");

        //private static FSDirectory _directory;

        //private static FSDirectory Directory
        //{
        //    get
        //    {
        //        if (_directory == null)
        //            _directory = FSDirectory.Open(new DirectoryInfo(_luceneDir));

        //        if (IndexWriter.IsLocked(_directory))
        //            IndexWriter.Unlock(_directory);

        //        var lockFilePath = Path.Combine(_luceneDir, "write.lock");
        //        if (File.Exists(lockFilePath))
        //            File.Delete(lockFilePath);

        //        return _directory;
        //    }
        //}

        private static RAMDirectory _directory;

        private static RAMDirectory Directory
        {
            get
            {
                if (_directory == null)
                    _directory = new RAMDirectory();

                return _directory;
            }
        }

        public static void AddToIndex(List<Post> posts)
        {
            using (var analyzer = new StandardAnalyzer(Version.LUCENE_30))
            {
                using (var writer = new IndexWriter(Directory, analyzer, IndexWriter.MaxFieldLength.UNLIMITED))
                {
                    foreach (var post in posts)
                    {
                        var doc = new Document();
                        doc.Add(new Field("slug", post.Slug, Field.Store.YES, Field.Index.NO));
                        doc.Add(new Field("title", post.Title, Field.Store.NO, Field.Index.ANALYZED));
                        doc.Add(new Field("body", post.Body, Field.Store.NO, Field.Index.ANALYZED));

                        writer.AddDocument(doc);
                    }
                }
            }
        }

        public static List<Post> Search(string terms)
        {
            if (string.IsNullOrEmpty(terms.Replace("*", "").Replace("?", "")))
                return new List<Post>();

            using (var searcher = new IndexSearcher(Directory, false))
            {
                using (var analyzer = new StandardAnalyzer(Version.LUCENE_30))
                {
                    var parser = new MultiFieldQueryParser(Version.LUCENE_30, new[] {"title", "body"}, analyzer);
                    var query = parser.Parse(terms.Trim());
                    var hits = searcher.Search(query, null, 1000, Sort.RELEVANCE).ScoreDocs;

                    var posts = new List<Post>();
                    var db = new PostRepository();

                    foreach (var hit in hits)
                    {
                        int id = hit.Doc;
                        Document doc = searcher.Doc(id);

                        posts.Add(db.GetPost(doc.Get("slug")));
                    }

                    return posts;
                }
            }
        }
    }
}