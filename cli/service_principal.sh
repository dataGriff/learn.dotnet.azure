#!/bin/bash

# Run the az ad sp create-for-rbac command and store the output in a variable
output=$(az ad sp create-for-rbac --name aprg-griff-dev)

# Check if the command was successful
if [ $? -ne 0 ]; then
  echo "Failed to create service principal"
  exit 1
fi

# Extract values using jq
appId=$(echo $output | jq -r '.appId')
displayName=$(echo $output | jq -r '.displayName')
password=$(echo $output | jq -r '.password')
tenant=$(echo $output | jq -r '.tenant')
objectId1=$(echo $output | jq -r '.id')

sp_details=$(az ad sp show --id $appId)
objectId=$(az ad sp show --id $appId --query id --out tsv)

az ad user show --id aprg-griff-dev --query id --out tsv

# Print the variables to verify
echo "App ID: $appId"
echo "Display Name: $displayName"
echo "Password: $password"
echo "Tenant: $tenant"
echo "Object ID: $objectId"
