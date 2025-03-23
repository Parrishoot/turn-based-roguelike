using UnityEngine;

public class CharacterEvents
{
    public EventProcessor<AttackEvent> Attack { get;} = new EventProcessor<AttackEvent>();
}
