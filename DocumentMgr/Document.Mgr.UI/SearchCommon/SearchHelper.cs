using Document.Mgr.BLL;
using Document.Mgr.IBLL;
using Document.Mgr.Model;
using Document.Mgr.UI.Models;
using Lucene.Net.Analysis.PanGu;
using Lucene.Net.Documents;
using Lucene.Net.Index;
using Lucene.Net.Search;
using Lucene.Net.Store;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.Hosting;

namespace Document.Mgr.UI.Search
{
    public static class SearchHelper
    {
        private static readonly string ContactIndexPath = HostingEnvironment.MapPath("~/SearchCommon/ContactIndex");
        private static readonly string UserInfoIndexPath = HostingEnvironment.MapPath("~/SearchCommon/UserInfoIndex");
        static IUserInfoService UserInfoService = new UserInfoService();
        public static void CreateContactIndex()
        {
            // 索引文档保存位置
            FSDirectory directory = FSDirectory.Open(new DirectoryInfo(ContactIndexPath), new NativeFSLockFactory());
            //判断索引库是否存在
            bool isUpdate = IndexReader.IndexExists(directory);
            if (isUpdate)
            {
                //  如果索引目录被锁定（比如索引过程中程序异常退出），则首先解锁
                //  Lucene.Net在写索引库之前会自动加锁，在close的时候会自动解锁
                //  不能多线程执行，只能处理意外被永远锁定的情况
                if (IndexWriter.IsLocked(directory))
                {
                    IndexWriter.Unlock(directory);  //unlock:强制解锁
                }
            }
            //  创建向索引库写操作对象  IndexWriter(索引目录,指定使用盘古分词进行切词,最大写入长度限制)
            //  补充:使用IndexWriter打开directory时会自动对索引库文件上锁
            IndexWriter writer = new IndexWriter(directory, new PanGuAnalyzer(), !isUpdate,
                IndexWriter.MaxFieldLength.UNLIMITED);

            var users = UserInfoService.LoadEntities(u => u.IsDeleted == false).ToList();
            if (users != null && users.Count > 0)
            {
                foreach (var userInfo in users)
                {
                    //创建document
                    //  一条Document相当于一条记录  与本项目 命名空间冲突了...
                    Lucene.Net.Documents.Document document = new Lucene.Net.Documents.Document();
                    //  每个Document可以有自己的属性（字段），所有字段名都是自定义的，值都是string类型
                    //  Field.Store.YES不仅要对文章进行分词记录，也要保存原文，就不用去数据库里查一次了
                    document.Add(new Field("id", userInfo.Id.ToString(), Field.Store.YES, Field.Index.NOT_ANALYZED));
                    //  需要进行全文检索的字段加 Field.Index. ANALYZED
                    //  Field.Index.ANALYZED:指定文章内容按照分词后结果保存，否则无法实现后续的模糊查询 
                    //  WITH_POSITIONS_OFFSETS:指示不仅保存分割后的词，还保存词之间的距离
                    //不检索 只存储
                    document.Add(new Field("name", userInfo.Name, Field.Store.YES, Field.Index.NOT_ANALYZED, Field.TermVector.WITH_POSITIONS_OFFSETS));
                    document.Add(new Field("email", userInfo.Email, Field.Store.YES, Field.Index.NOT_ANALYZED, Field.TermVector.WITH_POSITIONS_OFFSETS));
                    document.Add(new Field("phone", string.IsNullOrEmpty(userInfo.Contact.PhoneNumber) ? "" : userInfo.Contact.PhoneNumber, Field.Store.YES, Field.Index.NOT_ANALYZED, Field.TermVector.WITH_POSITIONS_OFFSETS));
                    document.Add(new Field("position", string.IsNullOrEmpty(userInfo.Position.Name) ? "" : userInfo.Position.Name, Field.Store.YES, Field.Index.NOT_ANALYZED));

                    String contactInfo = userInfo.Name + "," + userInfo.Email + ",";
                    contactInfo += string.IsNullOrEmpty(userInfo.Contact.PhoneNumber) ? "" : userInfo.Contact.PhoneNumber;
                    contactInfo += string.IsNullOrEmpty(userInfo.Position.Name) ? "" : userInfo.Position.Name;
                    //只检索  不存储
                    document.Add(new Field("contactInfo", contactInfo, Field.Store.NO, Field.Index.ANALYZED, Field.TermVector.WITH_POSITIONS_OFFSETS));
                    //  防止重复索引，删除已存在的。
                    writer.DeleteDocuments(new Term("id", userInfo.Id.ToString()));
                    //  把文档写入索引库
                    writer.AddDocument(document);
                }
            }

            writer.Close(); // Close后自动对索引库文件解锁
            directory.Close();  //  不要忘了Close，否则索引结果搜不到

        }

        public static void CreateUserInfoIndex()
        {
            // 索引文档保存位置
            FSDirectory directory = FSDirectory.Open(new DirectoryInfo(UserInfoIndexPath), new NativeFSLockFactory());
            //判断索引库是否存在
            bool isUpdate = IndexReader.IndexExists(directory);
            if (isUpdate)
            {
                //  如果索引目录被锁定（比如索引过程中程序异常退出），则首先解锁
                //  Lucene.Net在写索引库之前会自动加锁，在close的时候会自动解锁
                //  不能多线程执行，只能处理意外被永远锁定的情况
                if (IndexWriter.IsLocked(directory))
                {
                    IndexWriter.Unlock(directory);  //unlock:强制解锁
                }
            }
            //  创建向索引库写操作对象  IndexWriter(索引目录,指定使用盘古分词进行切词,最大写入长度限制)
            //  补充:使用IndexWriter打开directory时会自动对索引库文件上锁
            IndexWriter writer = new IndexWriter(directory, new PanGuAnalyzer(), !isUpdate,
                IndexWriter.MaxFieldLength.UNLIMITED);

            var users = UserInfoService.LoadEntities(u => u.IsDeleted == false).ToList();
            if (users != null && users.Count > 0)
            {
                foreach (var userInfo in users)
                {
                    //创建document
                    //  一条Document相当于一条记录  与本项目 命名空间冲突了...
                    Lucene.Net.Documents.Document document = new Lucene.Net.Documents.Document();
                    //  每个Document可以有自己的属性（字段），所有字段名都是自定义的，值都是string类型
                    //  Field.Store.YES不仅要对文章进行分词记录，也要保存原文，就不用去数据库里查一次了
                    document.Add(new Field("id", userInfo.Id.ToString(), Field.Store.YES, Field.Index.NOT_ANALYZED));


                    //为了支持多条件联合搜索。需要把所有信息写到一条信息中，然后设置SetSlop较大值。
                    String userInfoContent = "";
                    userInfoContent += string.IsNullOrEmpty(userInfo.Position.Name) ? " " : userInfo.Position.Name;
                    userInfoContent += "  ";//手动分割词汇 便于查找
                    userInfoContent += string.IsNullOrEmpty(userInfo.Role.Name) ? " " : userInfo.Role.Name;
                    userInfoContent += "  ";//手动分割词汇 便于查找
                    userInfoContent += string.IsNullOrEmpty(userInfo.Group.Name) ? " " : userInfo.Group.Name;
                    userInfoContent += "  ";//手动分割词汇 便于查找

                    StringBuilder sb = new StringBuilder();
                    foreach (var tech in userInfo.R_UserInfo_Tech)
                    {
                        //搜索英文时  一定要加分隔符
                        sb.Append(tech.Tech.TechName.ToLower() + " ");
                    }
                    userInfoContent += sb.ToString();
                    //  需要进行全文检索的字段加 Field.Index. ANALYZED
                    //  Field.Index.ANALYZED:指定文章内容按照分词后结果保存，否则无法实现后续的模糊查询 
                    //  WITH_POSITIONS_OFFSETS:指示不仅保存分割后的词，还保存词之间的距离
                    document.Add(new Field("userInfo", userInfoContent, Field.Store.NO, Field.Index.ANALYZED, Field.TermVector.WITH_POSITIONS_OFFSETS));


                    //  防止重复索引，删除已存在的。为了区分contact的索引id。使用uid
                    writer.DeleteDocuments(new Term("id", userInfo.Id.ToString()));
                    //  把文档写入索引库
                    writer.AddDocument(document);
                }
            }

            writer.Close(); // Close后自动对索引库文件解锁
            directory.Close();  //  不要忘了Close，否则索引结果搜不到

        }

        public static List<SearchResult> SeartchContact(string keyword, int startIndex, int pageSize, out int totalCount)
        {

            FSDirectory directory = FSDirectory.Open(new DirectoryInfo(ContactIndexPath), new NoLockFactory());
            IndexReader reader = IndexReader.Open(directory, true);
            IndexSearcher searcher = new IndexSearcher(reader);

            IEnumerable<string> keyList = SplitHelper.SplitWords(keyword);

            PhraseQuery queryContact = new PhraseQuery();
            foreach (var key in keyList)
            {
                queryContact.Add(new Term("contactInfo", key));
            }
            queryContact.SetSlop(100);

            BooleanQuery query = new BooleanQuery();
            query.Add(queryContact, BooleanClause.Occur.SHOULD); // SHOULD => 表示或者

            // TopScoreDocCollector:盛放查询结果的容器
            TopScoreDocCollector collector = TopScoreDocCollector.create(1000, true);
            // 使用query这个查询条件进行搜索，搜索结果放入collector
            searcher.Search(query, null, collector);
            // 首先获取总条数
            totalCount = collector.GetTotalHits();
            // 从查询结果中取出第m条到第n条的数据
            ScoreDoc[] docs = collector.TopDocs(startIndex, pageSize).scoreDocs;
            // 遍历查询结果
            List<SearchResult> resultList = new List<SearchResult>();
            for (int i = 0; i < docs.Length; i++)
            {
                // 拿到文档的id
                int docId = docs[i].doc;
                // 所以查询结果中只有id，具体内容需要二次查询
                // 根据id查询内容：放进去的是Document，查出来的还是Document
                Lucene.Net.Documents.Document doc = searcher.Doc(docId);
                SearchResult result = new SearchResult();
                result.UserId = doc.Get("id");
                result.Name = doc.Get("name");
                result.Email = doc.Get("email");
                result.PhoneNumber = doc.Get("phone");
                result.Position = doc.Get("position");
                resultList.Add(result);
            }

            return resultList;
        }

        public static List<int> SeartchUser(string keyword)
        {

            FSDirectory directory = FSDirectory.Open(new DirectoryInfo(UserInfoIndexPath), new NoLockFactory());
            IndexReader reader = IndexReader.Open(directory, true);
            IndexSearcher searcher = new IndexSearcher(reader);

            IEnumerable<string> keyList = SplitHelper.SplitWords(keyword);

            PhraseQuery queryUserInfo = new PhraseQuery();
            foreach (var key in keyList)
            {
                queryUserInfo.Add(new Term("userInfo", key));
            }
            queryUserInfo.SetSlop(100);

            BooleanQuery query = new BooleanQuery();
            query.Add(queryUserInfo, BooleanClause.Occur.SHOULD);


            // TopScoreDocCollector:盛放查询结果的容器
            TopScoreDocCollector collector = TopScoreDocCollector.create(1000, true);
            // 使用query这个查询条件进行搜索，搜索结果放入collector
            searcher.Search(query, null, collector);
            // 首先获取总条数
            int totalCount = collector.GetTotalHits();

            //这里取所有的数据 以方便后续的查找。
            ScoreDoc[] docs = collector.TopDocs(0, totalCount).scoreDocs;
            // 遍历查询结果
            List<int> resultList = new List<int>();
            for (int i = 0; i < docs.Length; i++)
            {
                // 拿到文档的id，因为Document可能非常占内存（DataSet和DataReader的区别）
                int docId = docs[i].doc;
                // 所以查询结果中只有id，具体内容需要二次查询
                // 根据id查询内容：放进去的是Document，查出来的还是Document
                Lucene.Net.Documents.Document doc = searcher.Doc(docId);
                int uid = Convert.ToInt32(doc.Get("id"));
                resultList.Add(uid);
            }

            return resultList;
        }

    }
}