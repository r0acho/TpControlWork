using TpControlWork.Domain.Models;

namespace TpControlWork.Services.Implementations.CalculateStrategies;

public class CalculateMedianStrategy : ICalculateStrategy
{
    public decimal Calculate(IEnumerable<Employee> employees)
    {
        return employees.Select(x => x.Salary).Average();
    }

    public static decimal Median(IEnumerable<decimal> source)
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
