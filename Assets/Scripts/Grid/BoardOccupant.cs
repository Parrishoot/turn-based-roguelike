using UnityEngine;

public abstract class BoardOccupant: MonoBehaviour
{
    // TODO: Implement
    public BoardSpace Space { get; set; }

    public abstract CharacterType GetCharacterType();

    public virtual void Damage(int damage) {

    }
}
