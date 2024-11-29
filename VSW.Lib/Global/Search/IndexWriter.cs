using Lucene.Net.Analysis.Standard;
using Lucene.Net.Documents;
using System.Collections;

namespace VSW.Lib.Search
{
    public class IndexWriter
    {
        private Lucene.Net.Index.IndexWriter writer;

        public IndexWriter(string directory) : this(directory, false)
        {

        }

        public IndexWriter(string directory, bool create)
        {
            Lucene.Net.Store.Directory directory_info = Lucene.Net.Store.FSDirectory.Open(new System.IO.DirectoryInfo(directory));
            writer = new Lucene.Net.Index.IndexWriter(directory_info, new StandardAnalyzer(Lucene.Net.Util.Version.LUCENE_29), create, Lucene.Net.Index.IndexWriter.MaxFieldLength.UNLIMITED);

            writer.SetUseCompoundFile(true);
        }

        public void Add(object obj)
        {
            if (obj is IList)
            {
                IList listItem = (IList)obj;
                for (int i = 0; listItem != null && i < listItem.Count; i++)
                    Add(listItem[i] as ISearchIndex);
            }
            else if (obj is ISearchIndex)
                Add(obj as ISearchIndex);
        }

        public void Add(ISearchIndex item)
        {
            if (item == null) return;

            Document doc = new Document();

            doc.Add(new Field("IndexID", item.IndexID.ToString(), Field.Store.YES, Field.Index.NO));
            doc.Add(new Field("IndexLangID", item.IndexLangID.ToString(), Field.Store.YES, Field.Index.NOT_ANALYZED));
            doc.Add(new Field("IndexType", item.IndexType, Field.Store.YES, Field.Index.NO));
            doc.Add(new Field("IndexContent", item.IndexContent, Field.Store.NO, Field.Index.ANALYZED));

            writer.AddDocument(doc);
        }

        public void Delete(ISearchIndex item)
        {
            writer.DeleteDocuments(new Lucene.Net.Index.Term[] {
                new Lucene.Net.Index.Term("IndexID", item.IndexID.ToString()), 
                new Lucene.Net.Index.Term("IndexType", item.IndexType) });
        }

        public void Close()
        {
            writer.Optimize();
            writer.Close();
        }
    }
}
