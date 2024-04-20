using System.Drawing;

namespace AdvisorHealthAPI.Tests.Services;

public static class Generator
{
    public static int GenerateRandomNumber(int size)
    {
        Random random = new Random();
        int min = (int)Math.Pow(10, size - 1);
        int max = (int)Math.Pow(10, size) - 1;
        return random.Next(min, max);
    }
}
