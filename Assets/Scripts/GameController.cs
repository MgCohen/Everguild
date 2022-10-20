using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class GameController : MonoBehaviour
{
    public Player Player;
    public int CurrentTurn { get; private set; }

    public DynamicList<GamePhase> _setupPhases = new DynamicList<GamePhase>();
    public DynamicList<GamePhase> _phases = new DynamicList<GamePhase>();

    [HideInInspector] public UnityEvent<GamePhase> OnPhaseChange = new UnityEvent<GamePhase>();
    [HideInInspector] public UnityEvent<int> OnTurnStart = new UnityEvent<int>();

    private void Start()
    {
        Setup();
    }

    private void Setup()
    {
        StartCoroutine(GameSetup());
    }

    IEnumerator GameSetup()
    {
        foreach(GamePhase phase in _setupPhases)
        {
            phase.Start(Player);
            while (!phase.Completed)
            {
                yield return null;
            }
            yield return new WaitForSeconds(0.5f);
        }
        StartCoroutine(GameRound());
    }

    IEnumerator GameRound()
    {
        CurrentTurn++;
        OnTurnStart.Invoke(CurrentTurn);
        yield return new WaitForSeconds(1);
        foreach(GamePhase phase in _phases)
        {
            phase.Start(Player);
            OnPhaseChange.Invoke(phase);
            while (!phase.Completed)
            {
                yield return null;
            }
            yield return new WaitForSeconds(0.5f);
        }
        OnPhaseChange.Invoke(null);
        StartCoroutine(GameRound());
    }

}
