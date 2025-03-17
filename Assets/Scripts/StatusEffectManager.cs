using System.Collections.Generic;
using UnityEngine;

public class StatusEffectManager : MonoBehaviour
{
    [SerializeField]
    private CharacterManager characterManager;

    private List<StatusEffectController> statusEffects = new List<StatusEffectController>();

    public void ApplyPerpetual(StatusEffectType statusEffectType) {
        StatusEffectController controller = statusEffectType.GetController(characterManager);
        controller.Apply();
        statusEffects.Add(controller);
    }

    public void ApplyForTurn(StatusEffectType statusEffectType) {
        StatusEffectController controller = statusEffectType.GetController(characterManager);
        Apply(controller);
    }

    private void Apply(StatusEffectController statusEffectController) {
        statusEffectController.Apply();
        statusEffects.Add(statusEffectController);
    }
}
