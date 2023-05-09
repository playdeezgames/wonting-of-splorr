Friend Module GameShoppes
    Friend Const ExchangeShoppeName = "exchange"
    Friend Const InnShoppeName = "inn"
    Friend Const SmokeShoppeName = "smokeshoppe"
    Friend Const BlacksmithShoppeName = "blacksmith"
    Friend Const ArmoryShoppeName = "armory"
    Friend Sub InitializeShoppes(data As WorldData)
        data.Shoppes.Clear()
        data.Shoppes.Add(ArmoryShoppeName, New ShoppeData With
                         {
                            .DisplayName = "Armory",
                            .Trades = New List(Of TradeData) From
                            {
                                New TradeData With
                                {
                                    .FromItem = New ItemInstanceData With
                                    {
                                        .ItemName = JoolsItemName,
                                        .Quantity = 2
                                    },
                                    .ToItem = New ItemInstanceData With
                                    {
                                        .ItemName = WoodenShieldItemName,
                                        .Quantity = 1
                                    }
                                },
                                New TradeData With
                                {
                                    .FromItem = New ItemInstanceData With
                                    {
                                        .ItemName = JoolsItemName,
                                        .Quantity = 5
                                    },
                                    .ToItem = New ItemInstanceData With
                                    {
                                        .ItemName = HelmetItemName,
                                        .Quantity = 1
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
                                        .ItemName = ShieldItemName,
                                        .Quantity = 1
                                    }
                                },
                                New TradeData With
                                {
                                    .FromItem = New ItemInstanceData With
                                    {
                                        .ItemName = JoolsItemName,
                                        .Quantity = 25
                                    },
                                    .ToItem = New ItemInstanceData With
                                    {
                                        .ItemName = LeatherArmorItemName,
                                        .Quantity = 1
                                    }
                                },
                                New TradeData With
                                {
                                    .FromItem = New ItemInstanceData With
                                    {
                                        .ItemName = JoolsItemName,
                                        .Quantity = 50
                                    },
                                    .ToItem = New ItemInstanceData With
                                    {
                                        .ItemName = ChainMailItemName,
                                        .Quantity = 1
                                    }
                                },
                                New TradeData With
                                {
                                    .FromItem = New ItemInstanceData With
                                    {
                                        .ItemName = JoolsItemName,
                                        .Quantity = 100
                                    },
                                    .ToItem = New ItemInstanceData With
                                    {
                                        .ItemName = PlateMailItemName,
                                        .Quantity = 1
                                    }
                                }
                            }
                         })
        data.Shoppes.Add(BlacksmithShoppeName, New ShoppeData With
                         {
                            .DisplayName = "Blacksmith's Shoppe",
                            .Trades = New List(Of TradeData) From
                            {
                                New TradeData With
                                {
                                    .FromItem = New ItemInstanceData With
                                    {
                                        .ItemName = CrownsItemName,
                                        .Quantity = 1
                                    },
                                    .ToItem = New ItemInstanceData With
                                    {
                                        .ItemName = ClubItemName,
                                        .Quantity = 1
                                    }
                                },
                                New TradeData With
                                {
                                    .FromItem = New ItemInstanceData With
                                    {
                                        .ItemName = CrownsItemName,
                                        .Quantity = 5
                                    },
                                    .ToItem = New ItemInstanceData With
                                    {
                                        .ItemName = SpearItemName,
                                        .Quantity = 1
                                    }
                                },
                                New TradeData With
                                {
                                    .FromItem = New ItemInstanceData With
                                    {
                                        .ItemName = CrownsItemName,
                                        .Quantity = 10
                                    },
                                    .ToItem = New ItemInstanceData With
                                    {
                                        .ItemName = DaggerItemName,
                                        .Quantity = 1
                                    }
                                },
                                New TradeData With
                                {
                                    .FromItem = New ItemInstanceData With
                                    {
                                        .ItemName = CrownsItemName,
                                        .Quantity = 25
                                    },
                                    .ToItem = New ItemInstanceData With
                                    {
                                        .ItemName = SwordItemName,
                                        .Quantity = 1
                                    }
                                },
                                New TradeData With
                                {
                                    .FromItem = New ItemInstanceData With
                                    {
                                        .ItemName = CrownsItemName,
                                        .Quantity = 50
                                    },
                                    .ToItem = New ItemInstanceData With
                                    {
                                        .ItemName = AxeItemName,
                                        .Quantity = 1
                                    }
                                }
                            }
                         })
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
                                },
                                New TradeData With
                                {
                                    .FromItem = New ItemInstanceData With
                                    {
                                        .ItemName = RatTailItemName,
                                        .Quantity = 5
                                    },
                                    .ToItem = New ItemInstanceData With
                                    {
                                        .ItemName = JoolsItemName,
                                        .Quantity = 1
                                    }
                                },
                                New TradeData With
                                {
                                    .FromItem = New ItemInstanceData With
                                    {
                                        .ItemName = SnakeFangItemName,
                                        .Quantity = 3
                                    },
                                    .ToItem = New ItemInstanceData With
                                    {
                                        .ItemName = JoolsItemName,
                                        .Quantity = 1
                                    }
                                },
                                New TradeData With
                                {
                                    .FromItem = New ItemInstanceData With
                                    {
                                        .ItemName = BatWingItemName,
                                        .Quantity = 4
                                    },
                                    .ToItem = New ItemInstanceData With
                                    {
                                        .ItemName = JoolsItemName,
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
