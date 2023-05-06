Friend Module GameCharacters
    Friend Const N00bCharacterName = "n00b"
    Friend Const MarcusCharacterName = "marcus"
    Friend Const GrahamCharacterName = "graham"
    Friend Const DanCharacterName = "dan"
    Friend Const SamuliCharacterName = "samuli"
    Friend Const ChickenCharacterName = "chicken"
    Friend Const BlobCharacterName = "blob"
    Friend Const DavidCharacterName = "david"
    Friend Sub InitializeCharacters(_data As WorldData)
        _data.Characters.Clear()
        InitializeCharacter(
            _data,
            N00bCharacterName,
            "N00b",
            " "c,
            Hue.Brown,
            statistics:=New Dictionary(Of StatisticType, Integer) From
            {
                {StatisticType.MaximumHealth, 3},
                {StatisticType.BaseAttack, 3},
                {StatisticType.MaximumAttack, 1},
                {StatisticType.BaseDefend, 4},
                {StatisticType.MaximumDefend, 2},
                {StatisticType.Intelligence, 10},
                {StatisticType.IntelligenceIncreaseMultiplier, 1},
                {StatisticType.HealthIncreaseMultiplier, 5}
            },
            isMessageSink:=True,
            deathSfx:=Sfx.PlayerDeath,
            hitSfx:=Sfx.PlayerHit)
        InitializeCharacter(_data, MarcusCharacterName, "Blackmage", "Z"c, Hue.Magenta)
        InitializeCharacter(_data, GrahamCharacterName, "Gorachan", "["c, Hue.Red)
        InitializeCharacter(_data, DanCharacterName, "Dan", "\"c, Hue.Cyan)
        InitializeCharacter(_data, SamuliCharacterName, "Sam", "Y"c, Hue.LightMagenta)
        InitializeCharacter(_data, DavidCharacterName, "Dave", "]"c, Hue.Yellow)
        InitializeCharacter(
            _data,
            BlobCharacterName,
            "Blob",
            "="c,
            Hue.Cyan,
            statistics:=New Dictionary(Of StatisticType, Integer) From
            {
                {StatisticType.MaximumHealth, 1},
                {StatisticType.BaseAttack, 2},
                {StatisticType.MaximumAttack, 1},
                {StatisticType.BaseDefend, 1},
                {StatisticType.MaximumDefend, 1},
                {StatisticType.XPValue, 1}
            },
            itemDrops:=New List(Of (String, Integer, Integer)) From
            {
                (CrownsItemName, 1, 1),
                (JoolsItemName, 1, 1)
            },
            deathSfx:=Sfx.EnemyDeath,
            hitSfx:=Sfx.EnemyHit)
        InitializeCharacter(
            _data,
            ChickenCharacterName,
            "Chicken",
            "V"c,
            Hue.Yellow,
            statistics:=New Dictionary(Of StatisticType, Integer) From
            {
                {StatisticType.MaximumHealth, 1},
                {StatisticType.BaseAttack, 4},
                {StatisticType.MaximumAttack, 2},
                {StatisticType.BaseDefend, 1},
                {StatisticType.MaximumDefend, 1},
                {StatisticType.XPValue, 0}
            },
            itemDrops:=New List(Of (String, Integer, Integer)) From
            {
                (FoodItemName, 1, 1),
                (FoodItemName, 0, 1)
            },
            deathSfx:=Sfx.EnemyDeath,
            hitSfx:=Sfx.EnemyHit)
    End Sub

    Private Sub InitializeCharacter(
                                   _data As WorldData,
                                   characterName As String,
                                   displayName As String,
                                   glyph As Char,
                                   hue As Hue,
                                   Optional statistics As IReadOnlyDictionary(Of StatisticType, Integer) = Nothing,
                                   Optional isMessageSink As Boolean = False,
                                   Optional itemDrops As IReadOnlyList(Of (String, Integer, Integer)) = Nothing,
                                   Optional deathSfx As Sfx? = Nothing,
                                   Optional hitSfx As Sfx? = Nothing)
        _data.Characters.Add(
            characterName,
            New CharacterData With
            {
                .DisplayName = displayName,
                .FontName = CharacterFontName,
                .Glyph = glyph,
                .Hue = hue,
                .IsMessageSink = isMessageSink,
                .DeathSfx = deathSfx,
                .HitSfx = hitSfx,
                .Statistics = If(
                    statistics IsNot Nothing,
                    New Dictionary(Of StatisticType, Integer)(statistics),
                    New Dictionary(Of StatisticType, Integer)),
                .ItemDrops = If(
                    itemDrops IsNot Nothing,
                    itemDrops.Select(Function(x) New CharacterItemDropData With
                                         {
                                            .ItemName = x.Item1,
                                            .Quantity = x.Item2,
                                            .Weight = x.Item3
                                         }).ToList,
                    New List(Of CharacterItemDropData))
            })
    End Sub

End Module
