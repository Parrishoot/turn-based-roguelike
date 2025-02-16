
public class SelectionAbilityProcessor : AbilityProcessor
{
    private SelectionProcessor abilitySelectionProcessor;

    private CharacterManager characterManager;

    public SelectionAbilityProcessor(CharacterManager characterManager, SelectionProcessor abilitySelectionProcessor) {
        this.characterManager = characterManager;
        this.abilitySelectionProcessor = abilitySelectionProcessor;
    }

    public override void Process()
    {
        SelectionController selectionController = characterManager.GetSelectionController();
        abilitySelectionProcessor.OnSelectionProcessed += () => OnAbilityFinish?.Invoke();
        selectionController.BeginSelection(abilitySelectionProcessor);
    }
}
