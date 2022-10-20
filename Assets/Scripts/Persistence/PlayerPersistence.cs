using System;
using System.Collections.Generic;

public class PlayerPersistence
{
    public static Func<List<CardData>> GetPlayerDeck;
    public static Action<List<CardData>> SavePlayerDeck;
    public static Func<List<CardData>> GetPlayerCollection;

    public static Func<Card> GetCardBack;
}