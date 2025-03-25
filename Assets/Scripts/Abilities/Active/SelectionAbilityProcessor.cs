
public class SelectionAbilityProcessor : ActiveAbilityProcessor
{
    private ISelectionController abilitySelectionController;

    private SelectionProcessor abilitySelectionProcessor;

    public SelectionAbilityProcessor(CharacterManager characterManager, ISelectionController selectionController, SelectionProcessor abilitySelectionProcessor): base(characterManager) {
        this.abilitySelectionProcessor = abilitySelectionProcessor;
        this.abilitySelectionController = selectionController;
    }

    public override void Process()
    {
        abilitySelectionProcessor.OnSelectionProcessed += (spaces) => {
            AffectedSpaces = spaces;
            OnAbilityFinish?.Invoke();
        };

        if(PredeterminedSpaces != null) {
            abilitySelectionProcessor.ProcessSelection(PredeterminedSpaces);
        }
        else {
            abilitySelectionController.BeginSelection(abilitySelectionProcessor);
        }
    }
}
