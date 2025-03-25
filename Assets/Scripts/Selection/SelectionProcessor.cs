using System;
using System.Collections.Generic;

public abstract class SelectionProcessor
{
    public Action<List<BoardSpace>> OnSelectionProcessed { get; set; }

    public abstract SelectionCriteria GetCriteria();

    public abstract void ProcessSelection(List<BoardSpace> selectedSpaces);
}
