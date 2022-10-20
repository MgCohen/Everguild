using System.Collections;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Text.RegularExpressions;
using UnityEngine;

public class CardDatabase : MonoBehaviour
{
    public TextAsset _rawCardData;
    public List<CardData> Cards = new List<CardData>();

    private void Start()
    {
        CreateCards();
        PlayerPersistence.GetPlayerCollection = () => Cards;
    }

    private void CreateCards()
    {
        string[] lines = Regex.Split(_rawCardData.text, "\\\r");
        for (int i = 1; i < lines.Length; i++)
        {
            string[] data = lines[i].Split(';');
            string rawName = data[0].Replace("\r", "").Replace("\n", ""); ;
            CardData card = new CardData(rawName, data[1], int.Parse(data[2]), int.Parse(data[3]), int.Parse(data[4]), data[5]);
            Cards.Add(card);
        }
    }

    public CardData GetCard(string name)
    {
        return Cards.FirstOrDefault(c => c.Name == name);
    }

}
