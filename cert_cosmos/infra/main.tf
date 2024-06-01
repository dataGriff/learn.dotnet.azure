provider "azurerm" {
  features {}
}

resource "azurerm_resource_group" "dp420" {
  name     = "lrn-dp420-rg"
  location = "North Europe"
}

resource "azurerm_cosmosdb_account" "dp420" {
  name                = "lrn-dp420-cosdb-eun-dgrf"
  location            = azurerm_resource_group.dp420.location
  resource_group_name = azurerm_resource_group.dp420.name
  offer_type          = "Standard"
  kind                = "GlobalDocumentDB"

  capabilities {
    name = "EnableServerless"
  }

  consistency_policy {
    consistency_level       = "Session"
    max_interval_in_seconds = 10
    max_staleness_prefix    = 200
  }

  geo_location {
    location          = azurerm_resource_group.dp420.location
    failover_priority = 0
  }
}

resource "azurerm_cosmosdb_sql_database" "cosmicworks" {
  name                = "cosmicworks"
  resource_group_name = azurerm_resource_group.dp420.name
  account_name        = azurerm_cosmosdb_account.dp420.name
}

resource "azurerm_cosmosdb_sql_container" "products" {
  name                = "products"
  resource_group_name = azurerm_resource_group.dp420.name
  account_name        = azurerm_cosmosdb_account.dp420.name
  database_name       = azurerm_cosmosdb_sql_database.cosmicworks.name

  partition_key_path = "/categoryId"
}

resource "azurerm_cosmosdb_sql_container" "productslease" {
  name                = "productslease"
  resource_group_name = azurerm_resource_group.dp420.name
  account_name        = azurerm_cosmosdb_account.dp420.name
  database_name       = azurerm_cosmosdb_sql_database.cosmicworks.name

  partition_key_path = "/id"
}