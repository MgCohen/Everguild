using System;
using UnityEngine;

[Serializable]
public abstract class GamePhase
{
    public bool Completed { get; private set; }

    public void Start(Player player)
    {
        Completed = false;
        OnPhaseStart(player);
    }

    protected abstract void OnPhaseStart(Player player);

    public void End()
    {
        Completed = true;
    }
}
