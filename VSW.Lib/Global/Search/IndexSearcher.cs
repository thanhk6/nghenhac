using System;
using System.Collections.Generic;

using Lucene.Net.Search;
using Lucene.Net.Documents;

namespace VSW.Lib.Search
{
    public class IndexSearcher
    {
        private Lucene.Net.Search.IndexSearcher searcher;

        public IndexSearcher(string directory)
        {
            searcher = new Lucene.Net.Search.IndexSearcher(Lucene.Net.Store.FSDirectory.Open(new System.IO.DirectoryInfo(directory)), true);
        }

        public List<SearchResult> Search(int PageIndex, int PageSize, ref int TotalRecord, Query query)
        {
            List<SearchResult> listItem = new List<SearchResult>();
 
            Hits hits = searcher.Search(query);
            TotalRecord = hits.Length();

            for (int i = PageIndex * PageSize; i < (PageIndex + 1) * PageSize && i < TotalRecord; i++)
            {
                Document doc = hits.Doc(i);
                SearchResult item = new SearchResult();

                item.IndexID = Core.Global.Convert.ToInt(doc.Get("IndexID"));
                item.IndexType = doc.Get("IndexType");

                listItem.Add(item);
            }

            return listItem;
        }

        public void Close()
        {
            searcher.Close();
        }
    }
}
