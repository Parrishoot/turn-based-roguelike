using System;
using System.Collections.Generic;
using Unity.VisualScripting;

public abstract class CharacterPassiveControllerBase<T> : PassiveController
where T: CharacterManager
{
    protected virtual Predicate<T> Filter { get; private set; } = (x) => true;

    private EventHandler<T> characterSpawnedHandler;

    protected CharacterPassiveControllerBase(Predicate<T> filter=null) {
        if(filter != null) {
            Filter = filter;
        }

        this.characterSpawnedHandler = new EveryEventHandler<T>(CheckApply);
    }

    protected override void ProcessActivation() {

        foreach(T characterManager in GetSpawnedCharacters()) {
            CheckApply(characterManager);
        }
        
        GetSpawnedEventProcessor().SubscribeHandler(characterSpawnedHandler);
    }

    protected override void ProcessDeactivation() {

        foreach(T characterManager in GetSpawnedCharacters()) {
            CheckRemove(characterManager);
        }
        
        GetSpawnedEventProcessor().UnsubscribeHandler(characterSpawnedHandler);
    }

    protected void CheckApply(T characterManager) {

        if(!Filter.Invoke(characterManager)) {
            return;
        }

        ProcessApplication(characterManager);
    }

    protected void CheckRemove(T characterManager) {

        if(!Filter.Invoke(characterManager)) {
            return;
        }

        ProcessRemoval(characterManager);
    }

    protected abstract void ProcessApplication(T characterManager);

    protected abstract void ProcessRemoval(T characterManager);

    protected abstract EventProcessor<T> GetSpawnedEventProcessor();

    protected abstract List<T> GetSpawnedCharacters();
}
