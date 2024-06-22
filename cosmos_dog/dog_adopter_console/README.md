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
docker compose up
```

useful for seeing files in a docker image
```bash
docker export dog_adopter_console | tar t > dog_adopter_console.txt
```