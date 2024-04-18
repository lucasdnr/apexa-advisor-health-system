using System.ComponentModel.DataAnnotations;

namespace AdvisorHealthAPI.Models;

public class Advisor
{
    [Key]
    public Guid Id {  get; init; }
    public string Name { get; private set; }
    public int SinNumber { get; private set; }
    public string Address { get; private set; }
    public int Phone { get; private set; }
    public string HealthStatus { get; private set; }

    private static Random random = new Random();

    public enum LightColor
    {
        GREEN,
        YELLOW,
        RED
    }

    public Advisor(string name, int sinNumber, string address, int phone) {
        Id = Guid.NewGuid();
        Name = name;
        SinNumber = sinNumber;
        Address = address;
        Phone = phone;

        // Randomly generated in the backend with the following probabilities:
        // Green=60% Yellow = 20% Red =20%)
        HealthStatus = generateHelthStatus();
        
    }

    public void SetName(string name)
    {
        Name = name;
    }

    public void SetAddress(string address)
    {
       Address = address;
    }

    public void SetPhone(int phone) { 
        Phone = phone;
    }
    public void SetSinNumber(int sinNumber)
    {
        SinNumber = sinNumber;
    }
    private static string generateHelthStatus()
    {
        int randomNumber = random.Next(1, 101); // Generate a random number between 1 and 100

        if (randomNumber <= 60)
        {
            return LightColor.GREEN.ToString(); // 60% chance for GREEN
        }
        else if (randomNumber <= 80)
        {
            return LightColor.YELLOW.ToString(); // 20% chance for YELLOW
        }
        else
        {
            return LightColor.RED.ToString(); // 20% chance for RED
        }
    }
}
