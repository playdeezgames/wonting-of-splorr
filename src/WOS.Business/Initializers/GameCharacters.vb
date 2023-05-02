Friend Module GameCharacters
    Friend Const N00bCharacterName = "n00b"
    Friend Const MarcusCharacterName = "marcus"
    Friend Const GrahamCharacterName = "graham"
    Friend Const DanCharacterName = "dan"
    Friend Const SamuliCharacterName = "samuli"
    Friend Const BlobCharacterName = "blob"
    Friend Sub InitializeCharacters(_data As WorldData)
        _data.Characters.Clear()
        InitializeCharacter(
            _data,
            N00bCharacterName,
            " "c,
            Hue.Brown,
            statistics:=New Dictionary(Of StatisticType, Integer) From
            {
                {StatisticType.MaximumHealth, 3},
                {StatisticType.BaseAttack, 3},
                {StatisticType.MaximumAttack, 1},
                {StatisticType.BaseDefend, 4},
                {StatisticType.MaximumDefend, 2}
            },
            isMessageSink:=True,
            deathSfx:=Sfx.PlayerDeath,
            hitSfx:=Sfx.PlayerHit)
        InitializeCharacter(_data, MarcusCharacterName, "Z"c, Hue.Magenta)
        InitializeCharacter(_data, GrahamCharacterName, "["c, Hue.Red)
        InitializeCharacter(_data, DanCharacterName, "\"c, Hue.Cyan)
        InitializeCharacter(_data, SamuliCharacterName, "Y"c, Hue.LightMagenta)
        InitializeCharacter(
            _data,
            BlobCharacterName,
            "="c,
            Hue.Cyan,
            statistics:=New Dictionary(Of StatisticType, Integer) From
            {
                {StatisticType.MaximumHealth, 1},
                {StatisticType.BaseAttack, 2},
                {StatisticType.MaximumAttack, 1},
                {StatisticType.BaseDefend, 1},
                {StatisticType.MaximumDefend, 1}
            },
            itemDrops:=New List(Of (String, Integer, Integer)) From
            {
                (CrownsItemName, 1, 1),
                (JoolsItemName, 1, 1)
            },
            deathSfx:=Sfx.EnemyDeath,
            hitSfx:=Sfx.EnemyHit)
    End Sub

    Private Sub InitializeCharacter(
                                   _data As WorldData,
                                   characterName As String,
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
                    itemDrops.Select(Function(x) New CreatureItemDropData With
                                         {
                                            .ItemName = x.Item1,
                                            .Quantity = x.Item2,
                                            .Weight = x.Item3
                                         }).ToList,
                    New List(Of CreatureItemDropData))
            })
    End Sub

End Module
