Friend Module GameShoppes
    Friend Const ExchangeShoppeName = "exchange"
    Friend Sub InitializeShoppes(data As WorldData)
        data.Shoppes.Clear()
        data.Shoppes.Add(ExchangeShoppeName, New ShoppeData With
                         {
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
