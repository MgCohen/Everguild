using System.Threading.Tasks;

public class EndPhase : GamePhase
{
    protected async override void OnPhaseStart(Player player)
    {
        player.ClearBoard();
        await Task.Delay(500);
        End();
    }
}
