using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UICard : MonoBehaviour
{
    [SerializeField] private Image _image;
    [SerializeField] private TextMeshProUGUI _name;
    [SerializeField] private TextMeshProUGUI _description;
    [SerializeField] private TextMeshProUGUI _attack;
    [SerializeField] private TextMeshProUGUI _defense;
    [SerializeField] private TextMeshProUGUI _cost;

    public CardData Card => _card;
    private CardData _card;

    public void Set(CardData card, Action OnSelect)
    {
        _card = card;

        GetComponent<Button>().onClick.AddListener(OnSelect.Invoke);

        _image.sprite = card.Image;
        _name.text = card.Name;
        _description.text = card.Description;
        _attack.text = card.Attack.ToString();
        _defense.text = card.Defense.ToString();
        _cost.text = card.Cost.ToString();
    }
}
