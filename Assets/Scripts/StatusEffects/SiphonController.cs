using UnityEngine;
using UnityEngine.TextCore.Text;

/// <summary>
/// Heal for half the damage dealt via attacks (Rounded Up)
/// </summary>
public class SiphonController : StatusEffectController
{
    private EventHandler<AttackEvent> eventHandler;

    public SiphonController(CharacterManager characterManager) : base(characterManager)
    {

    }

    public override StatusEffectType EffectType => StatusEffectType.SIPHON;

    public override bool Perpetual => true;

    public override void Apply()
    {
        eventHandler = new EveryEventHandler<AttackEvent>(Process);
        CharacterManager.Events.Attack.SubscribeHandler(eventHandler);
    }

    public override void Remove()
    {
        CharacterManager.Events.Attack.UnsubscribeHandler(eventHandler);
    }

    private void Process(AttackEvent attackEvent) {
        int healAmount = (int) ((double) attackEvent.Damage / 2);
        CharacterManager.HealthController.Heal(healAmount);
    }
}
