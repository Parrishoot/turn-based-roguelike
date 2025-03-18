using System.Collections.Generic;
using UnityEngine;

public class SelfSelectionController : ISelectionController
{
    private CharacterManager characterManager;

    public SelfSelectionController(CharacterManager characterManager)
    {
        this.characterManager = characterManager;
    }

    public void BeginSelection(SelectionProcessor selectionProcessor)
    {
        selectionProcessor.ProcessSelection(new List<BoardSpace>(){ characterManager.Space });
    }
}
