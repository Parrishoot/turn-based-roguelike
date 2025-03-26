using UnityEngine;

public class CharacterEvents
{
    public EventProcessor<AttackEvent> Attack { get;} = new EventProcessor<AttackEvent>();

    public EventProcessor Death { get; } = new EventProcessor();
}
