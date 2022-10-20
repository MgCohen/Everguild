using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;

public class PassButton : MonoBehaviour
{
    [SerializeField] private Player _player;
    [SerializeField] private GameController _controller;
    [SerializeField] private Button _button;

    [SerializeField] private GameObject _buttonHighlight;

    private void Start()
    {
        _button.onClick.AddListener(_player.Pass);
        _player.OnEnergyChanged.AddListener(CheckHighlight);
        _controller.OnPhaseChange.AddListener(CheckPhase);
    }

    private void CheckHighlight(int current, int max)
    {
        bool availableCards = _player.Hand.Cards.Where(c => c.Cost <= current).Any();
        bool isActive = _button.interactable;
        _buttonHighlight.SetActive(!availableCards && isActive);
    }

    private void CheckPhase(GamePhase phase)
    {
        _button.interactable = _player.IsActivePlayer;
        if(_button.interactable == false)
        {
            _buttonHighlight.SetActive(false);
        }
        else
        {
            CheckHighlight(_player.CurrentEnergy, _player.MaxEnergy);
        }
    }
}
