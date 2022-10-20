using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

public class PlayerData : MonoBehaviour
{
    [SerializeField] private Card _cardPrefab;
    [SerializeField] private List<CardData> _savedDeck = new List<CardData>();

    private void Start()
    {
        PlayerPersistence.GetPlayerDeck = GetSavedDeck;
        PlayerPersistence.SavePlayerDeck = SaveDeck;
        PlayerPersistence.GetCardBack = GetSavedCardTemplate;

        LoadDeckFromDisk();
    }

    public List<CardData> GetSavedDeck()
    {
        return _savedDeck;
    }

    public Card GetSavedCardTemplate()
    {
        return _cardPrefab;
    }

    public void SaveDeck(List<CardData> cards)
    {
        _savedDeck = cards;
        SaveDeckToDisk();
    }

    private void SaveDeckToDisk()
    {
        string path = Application.persistentDataPath + "MainDeck.json";
        string json = JsonUtility.ToJson(new DeckWrapper(_savedDeck));
        File.WriteAllText(path, json);
    }

    private void LoadDeckFromDisk()
    {
        string path = Application.persistentDataPath + "MainDeck.json";
        if (!File.Exists(path))
        {
            return;
        }
        string raw = File.ReadAllText(path);
        _savedDeck = JsonUtility.FromJson<DeckWrapper>(raw).Cards;
    }
}

public class DeckWrapper
{
    public DeckWrapper(List<CardData> cards)
    {
        Cards = cards;
    }
    public List<CardData> Cards = new List<CardData>();
}
