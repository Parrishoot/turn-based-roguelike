using System;
using System.Collections.Generic;

public abstract class SelectionProcessor
{
    public Action OnSelectionProcessed { get; set; }

    public abstract SelectionCriteria GetCriteria();

    public abstract void ProcessSelection(List<BoardSpace> selectedSpaces);
}
