using System;
using Microsoft.Azure.Cosmos;
using dog_adopter.Models;
using dog_adopter.Data;
using System.Threading;

class Program
{
    static async Task Main(string[] args)
    {

        var cosmosSQLDatabase = new CosmosSQLDatabase();
        await cosmosSQLDatabase.InitializeAsync();

        RescueDog rescueDog = GetRandomRescueDog();
        //new RescueDog("Phil", Breed.Beagle, Status.Available);

        var success = await cosmosSQLDatabase.CreateRescueDog(rescueDog);



        Console.WriteLine($"Created rescue dog: {rescueDog.Name} ({rescueDog.Breed})");
        Console.WriteLine($"The rescue dog has an id of {rescueDog.Id} and was created on {rescueDog.Timestamp}");



        // if (success)
        //     return Ok();
        // else
        //     return StatusCode(StatusCodes.Status500InternalServerError);

        Console.WriteLine("Press any key to exit.");
        Console.ReadKey();
    }

    public static RescueDog GetRandomRescueDog()
    {
        string[] dogNames = new string[] { "Harvey", "Mika", "Peppa", "Colin", "Kevin", "Betty", "Bandit", "Bobby", "Hank", "Pip"};
        var random = new Random();
        var randomName = dogNames[(random.Next(dogNames.Length))];
        var valuesBreed = Enum.GetValues(typeof(Breed));
        var randomBreed = (Breed)valuesBreed.GetValue(random.Next(valuesBreed.Length));
        var valuesStatus = Enum.GetValues(typeof(Status));
        var randomStatus = (Status)valuesStatus.GetValue(random.Next(valuesStatus.Length));
        return new RescueDog(randomName, randomBreed, randomStatus);
    }
}


