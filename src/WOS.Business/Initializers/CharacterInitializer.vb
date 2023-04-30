Friend Module CharacterInitializer
    Friend Const N00bCharacterName = "n00b"
    Friend Const MarcusCharacterName = "marcus"
    Friend Const GrahamCharacterName = "graham"
    Friend Const DanCharacterName = "dan"
    Friend Const SamuliCharacterName = "samuli"
    Friend Const BlobCharacterName = "blob"
    Friend Sub InitializeCharacters(_data As WorldData)
        _data.Characters.Clear()
        InitializeCharacter(_data, N00bCharacterName, " "c, Hue.Brown, isMessageSink:=True)
        InitializeCharacter(_data, MarcusCharacterName, "Z"c, Hue.Magenta)
        InitializeCharacter(_data, GrahamCharacterName, "["c, Hue.Red)
        InitializeCharacter(_data, DanCharacterName, "\"c, Hue.Cyan)
        InitializeCharacter(_data, SamuliCharacterName, "Y"c, Hue.LightMagenta)
        InitializeCharacter(_data, BlobCharacterName, "="c, Hue.Cyan)
    End Sub

    Private Sub InitializeCharacter(
                                   _data As WorldData,
                                   characterName As String,
                                   glyph As Char,
                                   hue As Hue,
                                   Optional isMessageSink As Boolean = False)
        _data.Characters.Add(
            characterName,
            New CharacterData With
            {
                .FontName = CharacterFontName,
                .Glyph = glyph,
                .Hue = hue,
                .IsMessageSink = isMessageSink
            })
    End Sub

End Module
