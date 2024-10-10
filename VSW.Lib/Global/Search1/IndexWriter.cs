using System;
using System.Collections.Generic;

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
            Lucene.Net.Store.Directory directory_info = Lucene.Net.Store.FSDirectory.GetDirectory(directory, true);

            writer = new Lucene.Net.Index.IndexWriter(directory, new StandardAnalyzer(), create);

            writer.SetUseCompoundFile(true);
        }

        public void Add(object item)
        {
            if (item is IList)
            {
                IList listItem = (IList)item;
                for (int i = 0; listItem != null && i < listItem.Count; i++)
                    Add(listItem[i] as ISearchIndex);
            }
            else if (item is ISearchIndex)
                Add(item as ISearchIndex);
        }

        public void Add(ISearchIndex item)
        {
            if (item == null) return;

            Document doc = new Document();

            doc.Add(new Field("IndexID", item.IndexID.ToString(), Field.Store.YES, Field.Index.NO));
            doc.Add(new Field("IndexLangID", item.IndexLangID.ToString(), Field.Store.YES, Field.Index.NO));
            doc.Add(new Field("IndexType", item.IndexType, Field.Store.YES, Field.Index.NO));
            doc.Add(new Field("IndexContent", item.IndexContent, Field.Store.NO, Field.Index.NO));

            writer.AddDocument(doc);
        }

        //public void Delete(ISearchIndex item)
        //{
        //    writer.DeleteDocuments(new Lucene.Net.Index.Term[] {
        //        new Lucene.Net.Index.Term("ID", item.IndexID.ToString()), 
        //        new Lucene.Net.Index.Term("IndexType", item.IndexType) });
        //}

        public void Close()
        {
            writer.Optimize();
            writer.Close();
        }
    }
}
