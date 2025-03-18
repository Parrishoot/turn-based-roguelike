

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

    public static CharacterType[] GetAlliedCharacterTypes(this CharacterType characterType) {

        return characterType switch
        {
            CharacterType.PLAYER or CharacterType.ALLY => new CharacterType[]{ CharacterType.PLAYER, CharacterType.ALLY },
            CharacterType.ENEMY => new CharacterType[]{ CharacterType.ENEMY },
            _ => new CharacterType[]{},
        };

    }

    public static TurnType? GetDefaultTurnType(this CharacterType characterType) {

        return characterType switch
        {
            CharacterType.PLAYER => TurnType.PLAYER,
            CharacterType.ALLY => TurnType.ALLY_CPU,
            CharacterType.ENEMY => TurnType.ENEMY_CPU,
            _ => null,
        };

    }
}
