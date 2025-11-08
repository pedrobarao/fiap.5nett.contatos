provider "azurerm" {
  features {
  }
  resource_provider_registrations = "none"
  subscription_id                 = "5c748bbc-ffa0-41df-a518-5f396262b1b2"
  environment                     = "public"
  use_msi                         = false
  use_cli                         = true # Utiliza as credenciais do AZ CLI
  use_oidc                        = false
}
