
public class PlayPhase : GamePhase
{
    protected override void OnPhaseStart(Player player)
    {
        player.StartPlay(End);
    }
}
