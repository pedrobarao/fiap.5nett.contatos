# =========================
# Resource Group
# =========================
resource "azurerm_resource_group" "res-0" {
  name     = "dev-rg-fiap-westus2-01"
  location = "westus2"
}

# =========================
# AKS Cluster (mínimo custo)
# =========================
resource "azurerm_kubernetes_cluster" "res-1" {
  name                = "dev-aks-fiap-westus2-01"
  location            = azurerm_resource_group.res-0.location
  resource_group_name = azurerm_resource_group.res-0.name

  # Mantém prefixo/DNS para consistência
  dns_prefix = "dev-aksdns-fiap-westus2-01"

  # Versão fixa — evita upgrades automáticos
  kubernetes_version           = "1.32.7"
  node_os_upgrade_channel      = "None"
  image_cleaner_interval_hours = 168

  identity {
    type = "SystemAssigned"
  }

  # Configurações de controle de custo
  role_based_access_control_enabled = true
  sku_tier                          = "Free"
  support_plan                      = "KubernetesOfficial"

  # ===== Networking =====
  network_profile {
    network_plugin       = "azure"
    network_plugin_mode  = "overlay"
    network_data_plane   = "azure"
    load_balancer_sku    = "standard"
    outbound_type        = "loadBalancer"

    # Faixas internas mínimas
    dns_service_ip  = "10.0.0.10"
    service_cidr    = "10.0.0.0/16"
    pod_cidr        = "10.244.0.0/16"

    # Um único IP público gerenciado
    load_balancer_profile {
      managed_outbound_ip_count = 1
      backend_pool_type         = "NodeIPConfiguration"
      idle_timeout_in_minutes   = 30
    }
  }

  # ===== Default Node Pool =====
  default_node_pool {
    name              = "agentpool"
    vm_size           = "Standard_A2_v2"    
    node_count        = 1                 
    os_disk_size_gb   = 64                
    os_disk_type      = "Managed"
    os_sku            = "Ubuntu"
    max_pods          = 30               
    kubelet_disk_type = "OS"
    type              = "VirtualMachineScaleSets"

    upgrade_settings {
      max_surge = "10%"
    }
  }

  lifecycle {
    ignore_changes = [
      #network_profile[0].load_balancer_profile[0].effective_outbound_ips,
      #network_profile[0].load_balancer_profile[0].outbound_ports_allocated
    ]
  }

  tags = {
    environment = "dev"
    purpose     = "fiap-lab"
    cost        = "low"
  }
}