using System;
using UnityEngine;

[Serializable]
public class CardData
{
    public CardData(string name, string description, int cost, int attack, int defense, string image)
    {
        Name = name;
        Description = description;
        Cost = cost;
        Attack = attack;
        Defense = defense;
        Image = Resources.Load<Sprite>($"Cards/" + image);
    }

    public string Name;
    public string Description;

    public int Cost;
    public int Attack;
    public int Defense;

    public Sprite Image;
}
