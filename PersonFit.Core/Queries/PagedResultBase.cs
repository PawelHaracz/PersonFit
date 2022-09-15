namespace PersonFit.Core.Queries;

public abstract class PagedResultBase
{
    public int CurrentPage { get; }
    public int ResultsPerPage { get; }
    public int TotalPages { get; }
    public int TotalResults { get; }

    protected PagedResultBase()
    {
    }
    
    public int FirstRowOnPage
    {
 
        get { return (CurrentPage - 1) * ResultsPerPage + 1; }
    }
 
    public int LastRowOnPage => Math.Min(CurrentPage * ResultsPerPage, TotalResults);

    protected PagedResultBase(int currentPage, int resultsPerPage,
        int totalPages, int totalResults)
    {
        CurrentPage = currentPage > totalPages ? totalPages : currentPage;
        ResultsPerPage = resultsPerPage;
        TotalPages = totalPages;
        TotalResults = totalResults;
    }
}
