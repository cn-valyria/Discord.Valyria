namespace Repository.Services.Cybernations.Contracts;

public class SearchResultSet<T>
{
    public int TotalCount { get; set; }
    public List<T> Results { get; set; }
}