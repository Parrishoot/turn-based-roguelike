using System;
using System.Collections.Generic;

public abstract class ActiveAbilityProcessor
{
    protected CharacterManager CharacterManager { get; private set; }

    public List<BoardSpace> PredeterminedSpaces { get; set; }
    
    public List<BoardSpace> AffectedSpaces { get; protected set; } = new List<BoardSpace>();

    protected ActiveAbilityProcessor(CharacterManager characterManager, List<BoardSpace> predeterminedSpaces=null)
    {
        this.CharacterManager = characterManager;
        this.PredeterminedSpaces = predeterminedSpaces;
    }

    public Action OnAbilityFinish { get; set; }

    public abstract void Process();
}
