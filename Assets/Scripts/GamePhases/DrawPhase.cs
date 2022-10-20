using System.Collections;
using System.Threading.Tasks;
using UnityEngine;

public class DrawPhase : GamePhase
{
    [SerializeField] private int _drawCount;
    [SerializeField] private bool _ignoreFirstPhase;

    private bool _ignored = false;

    protected async override void OnPhaseStart(Player player)
    {
        if(_ignoreFirstPhase && !_ignored)
        {
            _ignored = true;
            End();
            return;
        }

        for (int i = 0; i < _drawCount; i++)
        {
            player.Draw();
            await Task.Delay(500);
        }
        End();
    }
}
