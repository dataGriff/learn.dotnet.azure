provider "azurerm" {
  features {}
}

resource "azurerm_resource_group" "change_feed" {
  name     = "lrn-changefeed2-rg"
  location = "North Europe"
}

resource "azurerm_cosmosdb_account" "change_feed" {
  name                = "lrn-changefeed2-cosdb-eun-dgrf"
  location            = azurerm_resource_group.change_feed.location
  resource_group_name = azurerm_resource_group.change_feed.name
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
    location          = azurerm_resource_group.change_feed.location
    failover_priority = 0
  }
}

resource "azurerm_cosmosdb_sql_database" "acme" {
  name                = "acme-webstore"
  resource_group_name = azurerm_resource_group.change_feed.name
  account_name        = azurerm_cosmosdb_account.change_feed.name
  throughput          = 400
}

resource "azurerm_cosmosdb_sql_container" "cart" {
  name                = "cart"
  resource_group_name = azurerm_resource_group.change_feed.name
  account_name        = azurerm_cosmosdb_account.change_feed.name
  database_name       = azurerm_cosmosdb_sql_database.acme.name

  partition_key_path = "/cartId"
}

resource "azurerm_cosmosdb_sql_container" "lease" {
  name                = "lease"
  resource_group_name = azurerm_resource_group.change_feed.name
  account_name        = azurerm_cosmosdb_account.change_feed.name
  database_name       = azurerm_cosmosdb_sql_database.acme.name

  partition_key_path = "/id"
}