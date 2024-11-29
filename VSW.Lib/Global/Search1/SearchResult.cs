namespace VSW.Lib.Search
{
    public class SearchResult
    {
       public int IndexID { get; set; }
       public string IndexType { get; set; }

       public string Name { get; set; }
       public string URL { get; set; }
       public string File { get; set; }
       public string Summary { get; set; }

       public string DisplayName { get; set; }
       public string DisplayURL { get; set; }
       public string DisplaySummary { get; set; }
    }
}
