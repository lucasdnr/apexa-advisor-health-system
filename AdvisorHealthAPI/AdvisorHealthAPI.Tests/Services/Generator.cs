using System.Drawing;
using System.Net.NetworkInformation;

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

    public static string GenerateRandomString(int size)
    {
        Random random = new Random();
        string number = string.Empty;

        for (int i = 0; i < size; i++)
        {
            number += random.Next(0, 10);
        }

        return number;
    }
}
