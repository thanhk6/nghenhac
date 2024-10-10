namespace VSW.Lib.Search
{
    public interface ISearchIndex
    {
        int IndexID { get; }
        int IndexLangID { get; }
        string IndexType { get; }
        string IndexContent { get; }
    }
}