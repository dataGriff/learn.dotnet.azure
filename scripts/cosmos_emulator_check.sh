response=500

while [ "$response" -ne 200 ] ; do
    response=$(curl --write-out %{http_code}  --silent --output /dev/null -k https://localhost:8081/_explorer/emulator.pem)
    echo "Cosmos status is $response"
    if [ "$response" -eq 200 ] ; then
        break
    fi
    echo "Waiting for 5 seconds before trying again"
    sleep 5
done

curl -k https://localhost:8081/_explorer/emulator.pem > ~/emulatorcert.crt
sudo cp ~/emulatorcert.crt /usr/local/share/ca-certificates/
sudo update-ca-certificates

