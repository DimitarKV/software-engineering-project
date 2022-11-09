namespace HotelReservations.Helpers;

public class PaginationProperties
{
    public int PreviousPage { get; set; } = 0;
    public int CurrentPage { get; set; } = 0;
    public int NextPage { get; set; } = 0;
    public int LastPage { get; set; } = 0;
    public int PageSpanStart { get; set; } = 0;
    public int PageSpanEnd { get; set; } = 0;
}

public static class Pagination
{
    public static PaginationProperties CalculateProperties(int currentPage, 
        int itemsCount, 
        int itemsPerPage,
        int pageSpan = 9)
    {
        var properties = new PaginationProperties(){CurrentPage = currentPage};
        
        if (currentPage > 1)
        {
            properties.PreviousPage = currentPage - 1;
        }

        properties.LastPage = (int) Math.Ceiling(itemsCount / (float) itemsPerPage);
        if (currentPage < properties.LastPage)
        {
            properties.NextPage = currentPage + 1;
        }

        int halfPageSpan = pageSpan / 2;
        properties.PageSpanStart = currentPage - halfPageSpan;
        properties.PageSpanEnd = currentPage + halfPageSpan;

        if (properties.PageSpanEnd - properties.LastPage >= 0)
            properties.PageSpanStart -= properties.PageSpanEnd - properties.LastPage;
        if (properties.PageSpanStart <= 0)
            properties.PageSpanEnd += -properties.PageSpanStart + 1;

        properties.PageSpanStart = Math.Max(1, properties.PageSpanStart);
        properties.PageSpanEnd = Math.Min(properties.LastPage, properties.PageSpanEnd);

        return properties;
    }
}