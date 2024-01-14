namespace TpControlWork.Services.Extensions;

public static class EnumerableExtensions
{
    public static decimal Median(this IEnumerable<decimal> source)
    {
        if (source == null)
            throw new ArgumentNullException(nameof(source));

        var sortedList = source.OrderBy(x => x).ToList();
        int count = sortedList.Count;

        if (count == 0)
            throw new InvalidOperationException("Sequence contains no elements.");

        if (count % 2 == 0)
        {
            int middle = count / 2;
            return (sortedList[middle - 1] + sortedList[middle]) / 2m;
        }
        else
        {
            return sortedList[count / 2];
        }
    }
}
