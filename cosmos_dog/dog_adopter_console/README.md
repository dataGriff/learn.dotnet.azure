# Dog Adopter

* [Cosmos Emulator](https://localhost:8081/_explorer/index.html)
* [Emulator Recipes](https://github.com/Azure/cosmosdb-emulator-recipes/tree/main)
* [Docker File Contents](https://betterstack.com/community/questions/how-to-view-contents-of-docker-images/)

```bash
dotnet add package Microsoft.Azure.Cosmos
dotnet add package Newtonsoft.Json
```

```bash
cd cosmos_dog/dog_adopter_console
chmod +x entrypoint.sh
docker compose build
docker compose up --detach ## done this so don't accidentally bring down cosmos emulator when ctrl+c out of the command
docker compose logs --follow ## follows logs, if cancel this not the end of the world!!
docker compose down app ## specifically bring down the app without impacting emulator
docker compose up --detach --build  ##build and run so while debugging can see changes
docker compose logs --follow ## follows logs, if cancel this not the end of the world!!
docker compose down ## be careful doing this as brings down the cosmos emulator which takes ages to come back up!!
```

```bash
```

useful for seeing files in a docker image
```bash
docker export dog_adopter_console | tar t > dog_adopter_console.txt
```