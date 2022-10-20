using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Player : MonoBehaviour
{
    public UnityEvent<int, int> OnEnergyChanged = new UnityEvent<int, int>();

    [SerializeField] private Deck _deck;
    [SerializeField] private Hand _hand;
    [SerializeField] private PlayField _field;
    [SerializeField] private DiscardPile _discard;

    public DiscardPile Discard => _discard;
    public PlayField Field => _field;
    public Hand Hand => _hand;

    public int CurrentEnergy { get; private set; }
    public int MaxEnergy { get; private set; }

    public bool IsActivePlayer { get; private set; }

    private Action _passTurnCallback;

    public void Setup(List<CardData> cards, Card cardPrefab)
    {
        IsActivePlayer = false;
        foreach (CardData cardData in cards)
        {
            Card card = Instantiate(cardPrefab);
            card.Set(cardData);
            _deck.AddCard(card);
        }
    }

    public void Draw()
    {
        _deck.Draw();
    }

    public void ClearBoard()
    {
        List<Slot> slots =  _field.GetSlots();
        foreach(Slot slot in slots)
        {
            slot.Dispose();
        }
    }

    public void ChangeEnergy(int amount)
    {
        CurrentEnergy += amount;
        OnEnergyChanged?.Invoke(CurrentEnergy, MaxEnergy);
    }

    public void ChangeMaxEnergy(int amount)
    {
        MaxEnergy += amount;
        OnEnergyChanged?.Invoke(CurrentEnergy, MaxEnergy);
    }

    public void StartPlay(Action passTurnCallback)
    {
        _passTurnCallback = passTurnCallback;
        IsActivePlayer = true;
        _hand.EnableCardPlay();
    }

    public void Pass()
    {
        IsActivePlayer = false;
        _passTurnCallback.Invoke();
        _hand.DisableCardPlay();
    }
}
