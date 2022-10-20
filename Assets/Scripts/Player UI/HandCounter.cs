using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class HandCounter : MonoBehaviour
{

    [SerializeField] private Hand _hand;
    [SerializeField] private TextMeshProUGUI _text;

    private void Start()
    {
        _hand.OnCardAdded.AddListener(UpdateCounter);
        _hand.OnCardRemoved.AddListener(UpdateCounter);
    }

    private void UpdateCounter(Card arg0)
    {
        int count = _hand.Cards.Count;
        _text.text = $"{count} cards";
    }
}
