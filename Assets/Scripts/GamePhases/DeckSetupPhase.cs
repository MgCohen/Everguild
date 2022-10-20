using System.Collections.Generic;
using UnityEngine;

public class DeckSetupPhase : GamePhase
{
    [SerializeField] private Player _player;

    protected override void OnPhaseStart(Player player)
    {
        List<CardData> deck = PlayerPersistence.GetPlayerDeck();
        Card _cardPrefab = PlayerPersistence.GetCardBack();
        player.Setup(deck, _cardPrefab);
        End();
    }
}
