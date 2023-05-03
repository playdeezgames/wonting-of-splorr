Friend Module GameShoppes
    Friend Const ExchangeShoppeName = "exchange"
    Friend Const InnShoppeName = "inn"
    Friend Const SmokeShoppeName = "smokeshoppe"
    Friend Sub InitializeShoppes(data As WorldData)
        data.Shoppes.Clear()
        data.Shoppes.Add(SmokeShoppeName, New ShoppeData With
                         {
                            .DisplayName = "Smoke Shoppe",
                            .Trades = New List(Of TradeData) From
                            {
                                New TradeData With
                                {
                                    .FromItem = New ItemInstanceData With
                                    {
                                        .ItemName = JoolsItemName,
                                        .Quantity = 9
                                    },
                                    .ToItem = New ItemInstanceData With
                                    {
                                        .ItemName = PotionItemName,
                                        .Quantity = 1
                                    }
                                }
                            }
                         })
        data.Shoppes.Add(InnShoppeName, New ShoppeData With
                         {
                            .DisplayName = "Tavern",
                            .Trades = New List(Of TradeData) From
                            {
                                New TradeData With
                                {
                                    .FromItem = New ItemInstanceData With
                                    {
                                        .ItemName = CrownsItemName,
                                        .Quantity = 2
                                    },
                                    .ToItem = New ItemInstanceData With
                                    {
                                        .ItemName = FoodItemName,
                                        .Quantity = 1
                                    }
                                }
                            }
                         })
        data.Shoppes.Add(ExchangeShoppeName, New ShoppeData With
                         {
                            .DisplayName = "Exchange",
                            .Trades = New List(Of TradeData) From
                            {
                                New TradeData With
                                    {
                                        .FromItem = New ItemInstanceData With
                                            {
                                                .ItemName = CrownsItemName,
                                                .Quantity = 10
                                            },
                                        .ToItem = New ItemInstanceData With
                                            {
                                                .ItemName = JoolsItemName,
                                                .Quantity = 9
                                            }
                                    },
                                New TradeData With
                                    {
                                        .FromItem = New ItemInstanceData With
                                            {
                                                .ItemName = JoolsItemName,
                                                .Quantity = 10
                                            },
                                        .ToItem = New ItemInstanceData With
                                            {
                                                .ItemName = CrownsItemName,
                                                .Quantity = 9
                                            }
                                    }
                            }
                         })
    End Sub
End Module
