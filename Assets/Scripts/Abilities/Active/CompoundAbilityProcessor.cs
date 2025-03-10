using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class CompoundAbilityProcessor : ActiveAbilityProcessor
{
    private AbilitySelectionCriteria overallAbilitySelectionCriteria;

    private Queue<ActiveAbility> abilityQueue;

    private CharacterManager characterManager;

    public CompoundAbilityProcessor(CharacterManager characterManager, List<ActiveAbility> abilities, AbilitySelectionCriteria abilitySelectionCriteria): base(characterManager) {
        this.abilityQueue = new Queue<ActiveAbility>(abilities);
        this.overallAbilitySelectionCriteria = abilitySelectionCriteria;
        this.characterManager = characterManager;
    }

    public override void Process()
    {
        if(abilityQueue.Count <= 0) {
            OnAbilityFinish?.Invoke();
            return;
        }

        ActiveAbility nextAbility = abilityQueue.Dequeue();
        AbilitySelectionCriteria abilitySelectionCriteria = new AbilitySelectionCriteria()
            .WithOverallAbilityType(overallAbilitySelectionCriteria.OverallAbilityType)
            .WithCurrentAbilityType(nextAbility.GetDefaultAbilitySelectionCriteria(characterManager).CurrentAbilityType)
            .WithRangeToTarget(overallAbilitySelectionCriteria.RangeToTarget);
    
        nextAbility.AbilitySelectionCriteriaOverride = abilitySelectionCriteria;
        ActiveAbilityProcessor nextAbilityProcessor = nextAbility.GetAbilityProcessor(characterManager);

        nextAbilityProcessor.OnAbilityFinish += Process;
        nextAbilityProcessor.Process();
    }
}
