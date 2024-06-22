using System;
using Microsoft.Azure.Cosmos;
using dog_adopter.Models;
using dog_adopter.Data;
using System.Threading;
using System.Diagnostics;

class Program
{
    static async Task Main(string[] args)
    {

        var cosmosSQLDatabase = new CosmosSQLDatabase();
        await cosmosSQLDatabase.InitializeAsync();
        int runDuration = 60;
        int waitBetweenUpdate = 2000;
        Console.WriteLine($"Program will run for {runDuration} seconds and waits between update will be {2000} milliseconds.");

        Stopwatch timer = new Stopwatch();
        timer.Start();
        while (timer.Elapsed.TotalSeconds < runDuration)
        {

            RescueDog rescueDog = GetRandomRescueDog();

            var success = await cosmosSQLDatabase.CreateRescueDog(rescueDog);

            Console.WriteLine($"Created rescue dog: {rescueDog.Name} ({rescueDog.Breed}) has a status of {rescueDog.Status}");
            Console.WriteLine($"The rescue dog has an id of {rescueDog.Id} and was created on {rescueDog.Timestamp}");

            Console.WriteLine("Sleep for 2 seconds.");
            Thread.Sleep(2000);

            rescueDog.Status = Status.Adopted;

            success = await cosmosSQLDatabase.UpdateRescueDog(rescueDog);

            RescueDog updatedRescueDog = await cosmosSQLDatabase.GetRescueDog(rescueDog.Breed, rescueDog.Id);

            Console.WriteLine($"Updated rescue dog: {updatedRescueDog.Name} ({updatedRescueDog.Breed}) has a status of {updatedRescueDog.Status}");
            Console.WriteLine($"The rescue dog has an id of {updatedRescueDog.Id} and was updated on {updatedRescueDog.Timestamp}");

            TimeSpan ts = timer.Elapsed;

            // Format and display the TimeSpan value.
            string elapsedTime = String.Format("{0:00}:{1:00}:{2:00}.{3:00}",
                ts.Hours, ts.Minutes, ts.Seconds,
                ts.Milliseconds / 10);
            Console.WriteLine("RunTime " + elapsedTime);
        }

        timer.Stop();

        Console.WriteLine("Press any key to exit.");
        Console.ReadKey();
    }

    public static RescueDog GetRandomRescueDog()
    {
        Guid newId = Guid.NewGuid();
        DateTime newTimestamp = DateTime.UtcNow;
        string[] dogNames = new string[] { "Harvey", "Mika", "Peppa", "Colin", "Kevin", "Betty", "Bandit", "Bobby", "Hank", "Pip" };
        var random = new Random();
        var randomName = dogNames[(random.Next(dogNames.Length))];
        var valuesBreed = Enum.GetValues(typeof(Breed));
        var randomBreed = (Breed)valuesBreed.GetValue(random.Next(valuesBreed.Length));
        var valuesStatus = Enum.GetValues(typeof(Status));
        var randomStatus = (Status)valuesStatus.GetValue(random.Next(valuesStatus.Length));
        return new RescueDog(randomName, randomBreed, randomStatus, newId, newTimestamp);
    }
}


