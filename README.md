# C# DAPR Pub/Sub with Azure Event Hub & Integration Test

[In this links you can view the official documentation](https://docs.dapr.io/reference/components-reference/supported-pubsub/setup-azure-eventhubs/).

## Create Azure Resource Group:

```terminal
az group create -l westeurope -n MyResourceGroup
```

## Create Azure Storage Account:

```terminal
az storage account create -n mydaprdemostorageaccount -g MyResourceGroup -l westeurope --sku Standard_LRS
```

And the blob container:

```terminal
az storage container create -n mydaprdemostoragecontainer --public-access blob --account-name mydaprdemostorageaccount
```

## Create Azure Event Hub:

```terminal
az eventhubs namespace create --name mydaprdemonamespace --resource-group MyResourceGroup -l westeurope
```

Add the Event Hub:

```terminal
az eventhubs eventhub create --resource-group MyResourceGroup --namespace-name mydaprdemonamespace --name mydaprdemoeventhub --message-retention 1 --partition-count 1
```

You need to add a [new capture](https://docs.microsoft.com/en-en/azure/event-hubs/event-hubs-capture-enable-through-portal) linked with **mydaprdemostoragecontainer**

Add policies for send & listener:

```terminal
az eventhubs eventhub authorization-rule create --resource-group MyResourceGroup --namespace-name mydaprdemonamespace --eventhub-name mydaprdemoeventhub --name send --rights Send
```

```terminal
az eventhubs eventhub authorization-rule create --resource-group MyResourceGroup --namespace-name mydaprdemonamespace --eventhub-name mydaprdemoeventhub --name listen --rights Listen
```

Add a consumer group:

```terminal
az eventhubs eventhub consumer-group create --resource-group myresourcegroup --namespace-name mydaprdemonamespace --eventhub-name mydaprdemoeventhub --name subscriptor
```

[![GitKraken shield](https://img.shields.io/badge/GitKraken-Legendary%20Git%20Tools-teal?style=plastic&logo=gitkraken)](https://gitkraken.com/invite/sUviHf86)

[![MIT License](https://img.shields.io/apm/l/atomic-design-ui.svg?)](https://github.com/tterb/atomic-design-ui/blob/master/LICENSEs)
