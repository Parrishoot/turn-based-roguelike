using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class CompoundAbilityProcessor : AbilityProcessor
{
    private Queue<AbilityProcessor> abilityQueue;

    public CompoundAbilityProcessor(CharacterManager characterManager, List<ActiveAbility> abilities): base(characterManager) {
        this.abilityQueue = new Queue<AbilityProcessor>(abilities.Select(x => x.GetAbilityProcessor(characterManager)));
    }

    public override void Process()
    {
        if(abilityQueue.Count <= 0) {
            OnAbilityFinish?.Invoke();
            return;
        }

        AbilityProcessor nextAbility = abilityQueue.Dequeue();
        nextAbility.OnAbilityFinish += Process;
        nextAbility.Process();
    }
}
