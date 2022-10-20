using UnityEngine;

public class RefreshPhase : GamePhase
{
    [SerializeField] private int _maxEnergy;
    [SerializeField] private int _energyPerTurn;

    protected override void OnPhaseStart(Player player)
    {
        if (player.MaxEnergy < _maxEnergy)
        {
            player.ChangeMaxEnergy(_energyPerTurn);
        }

        int energyToBeReplenished = player.MaxEnergy - player.CurrentEnergy;
        player.ChangeEnergy(energyToBeReplenished);
        End();
    }
}
