using UnityEngine;

public class PlayerManager : Singleton<PlayerManager>
{
    [field:SerializeReference]
    public PlayerStatsManager StatsManager { get; private set; }
}
