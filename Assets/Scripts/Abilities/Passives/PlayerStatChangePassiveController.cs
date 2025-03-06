using System.Collections.Generic;

public class PlayerStatChangePassiveController : StatChangePassiveController<PlayerStatType>
{
    public PlayerStatChangePassiveController(PlayerStatType statType, StatAdjuster statAdjuster) : base(statType, statAdjuster)
    {
    }

    protected override void ProcessDeactivation()
    {
        base.ProcessDeactivation();

        if(statType == PlayerStatType.HAND_SIZE) {
            DeckManager.Instance.DiscardDown();
        }
    }

    protected override List<StatsManager<PlayerStatType>> GetStatsManagers()
    {
        return new List<StatsManager<PlayerStatType>>() {
            PlayerManager.Instance.StatsManager
        };
    }
}
