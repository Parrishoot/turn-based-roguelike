

public static class CharacterTypeUtil
{
    public static CharacterType[] GetOpposingCharacterTypes(this CharacterType characterType) {

        return characterType switch
        {
            CharacterType.PLAYER or CharacterType.ALLY => new CharacterType[]{ CharacterType.ENEMY },
            CharacterType.ENEMY => new CharacterType[]{ CharacterType.PLAYER, CharacterType.ALLY },
            _ => new CharacterType[]{},
        };

    }
}
