using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class DeckEditor : MonoBehaviour
{
    [SerializeField] private Transform _collectionHolder;
    [SerializeField] private Transform _deckHolder;
    [SerializeField] private UICard _cardPrefab;

    private List<UICard> _cardsInDeck = new List<UICard>();

    private void Start()
    {
        SpawnCollection();
        SpawnToDeck();
    }

    private void SpawnToDeck()
    {
        List<CardData> deck = PlayerPersistence.GetPlayerDeck();
        foreach(CardData card in deck)
        {
            SpawnToDeck(card);
        }
    }

    private void SpawnCollection()
    {
        var cards = PlayerPersistence.GetPlayerCollection();
        foreach(CardData card in cards)
        {
            UICard uicard = Instantiate(_cardPrefab);
            uicard.Set(card, () => SpawnToDeck(card));
            uicard.transform.SetParent(_collectionHolder);
        }
    }

    public void SpawnToDeck(CardData card)
    {
        if(_cardsInDeck.Count >= 10)
        {
            return;
        }

        if(_cardsInDeck.Select(s => s.Card).Where(c => c == card).Count() >= 3)
        {
            return;
        }

        UICard uicard = Instantiate(_cardPrefab, _deckHolder);
        _cardsInDeck.Add(uicard);
        uicard.Set(card, () => RemoveFromDeck(uicard));
        Save();
    }

    public void RemoveFromDeck(UICard card)
    {
        _cardsInDeck.Remove(card);
        Destroy(card.gameObject);
        Save();
    }

    public void Save()
    {
        List<CardData> cards = _cardsInDeck.Select(uic => uic.Card).ToList();
        PlayerPersistence.SavePlayerDeck(cards);
    }
}
