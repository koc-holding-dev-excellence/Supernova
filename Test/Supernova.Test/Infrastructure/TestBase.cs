using Supernova.Common.Enums;

namespace Supernova.Test.Infrastructure;

public class TestBase
{
    public string AString() => $"{Guid.NewGuid()}";
    public Guid AGuid() => Guid.NewGuid();
    public DateTime ADateTime() => DateTime.Now;
    public ActiveFlag AnActiveFlag() => ActiveFlag.Active;

    public int ANumber(int minValue = 1, int maxValue = 99999, int excludedNumber = default)
    {
        if (excludedNumber == default)
        {
            return new Random().Next(minValue, maxValue);
        }
        int result;
        do
        {
            result = new Random().Next(minValue, maxValue);
        } while (excludedNumber == result);
        return result;
    }
}
