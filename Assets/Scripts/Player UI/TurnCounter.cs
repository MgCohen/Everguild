using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class TurnCounter : MonoBehaviour
{
    [SerializeField] private GameController _controller;
    [SerializeField] private TextMeshProUGUI _text;
    // Start is called before the first frame update
    void Start()
    {
        _controller.OnTurnStart.AddListener(UpdateText);
    }

    private void UpdateText(int turn)
    {
        _text.text = $"Turn {turn}";
    }

}
