```bash
dotnet new console

```bash
dotnet add package Microsoft.Azure.Cosmos
```

```bash
docker run \
    --publish 8081:8081 \
    --publish 10250-10255:10250-10255 \
    --interactive \
    --tty \
    mcr.microsoft.com/cosmosdb/linux/azure-cosmos-emulator:latest
```

* [Cosmos Emulator](https://localhost:8081/_explorer/index.html)

```bash
curl -k https://localhost:8081/_explorer/emulator.pem > ~/emulatorcert.crt
sudo cp ~/emulatorcert.crt /usr/local/share/ca-certificates/
sudo update-ca-certificates
```

