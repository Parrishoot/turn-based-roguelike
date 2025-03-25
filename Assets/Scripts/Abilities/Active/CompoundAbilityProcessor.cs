using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class CompoundAbilityProcessor : ActiveAbilityProcessor
{
    private AbilitySelectionCriteria overallAbilitySelectionCriteria;

    private Queue<CompoundAbilityNode> abilityQueue;

    private CharacterManager characterManager;

    private ActiveAbilityProcessor previousAbility = null;

    public CompoundAbilityProcessor(CharacterManager characterManager, List<CompoundAbilityNode> abilities, AbilitySelectionCriteria abilitySelectionCriteria): base(characterManager) {
        this.abilityQueue = new Queue<CompoundAbilityNode>(abilities);
        this.overallAbilitySelectionCriteria = abilitySelectionCriteria;
        this.characterManager = characterManager;
    }

    public override void Process()
    {
        if(abilityQueue.Count <= 0) {
            OnAbilityFinish?.Invoke();
            return;
        }

        CompoundAbilityNode nextAbility = abilityQueue.Dequeue();
        AbilitySelectionCriteria abilitySelectionCriteria = new AbilitySelectionCriteria()
            .WithOverallAbilityType(overallAbilitySelectionCriteria.OverallAbilityType)
            .WithCurrentAbilityType(nextAbility.Ability.GetDefaultAbilitySelectionCriteria(characterManager).CurrentAbilityType)
            .WithRangeToTarget(overallAbilitySelectionCriteria.RangeToTarget);
    
        nextAbility.Ability.AbilitySelectionCriteriaOverride = abilitySelectionCriteria;
        ActiveAbilityProcessor nextAbilityProcessor = nextAbility.Ability.GetAbilityProcessor(characterManager);

        if(nextAbility.Chain && previousAbility != null) {
            nextAbilityProcessor.PredeterminedSpaces = previousAbility.AffectedSpaces;
        }

        previousAbility = nextAbilityProcessor;
        nextAbilityProcessor.OnAbilityFinish += Process;
        nextAbilityProcessor.Process();
    }
}
