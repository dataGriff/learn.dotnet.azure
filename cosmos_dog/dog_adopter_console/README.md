# Dog Adopter

* [Cosmos Emulator](https://localhost:8081/_explorer/index.html)

```bash
dotnet add package Microsoft.Azure.Cosmos
dotnet add package Newtonsoft.Json
```

```bash
docker run -d  -e COSMOS_CONN=$COSMOS_CONN --name dog_adopter_console dog_adopter_console:latest 
```