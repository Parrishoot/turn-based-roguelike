
public class SelectionAbilityProcessor : AbilityProcessor
{
    private ISelectionController abilitySelectionController;

    private SelectionProcessor abilitySelectionProcessor;

    public SelectionAbilityProcessor(CharacterManager characterManager, ISelectionController selectionController, SelectionProcessor abilitySelectionProcessor): base(characterManager) {
        this.abilitySelectionProcessor = abilitySelectionProcessor;
        this.abilitySelectionController = selectionController;
    }

    public override void Process()
    {
        abilitySelectionProcessor.OnSelectionProcessed += () => OnAbilityFinish?.Invoke();
        abilitySelectionController.BeginSelection(abilitySelectionProcessor);
    }
}
