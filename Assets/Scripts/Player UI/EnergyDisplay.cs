using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using TMPro;

public class EnergyDisplay : MonoBehaviour
{
    [SerializeField] private Player _player;
    [SerializeField] private TextMeshProUGUI _text;

    private void Start()
    {
        _player.OnEnergyChanged.AddListener(UpdateEnergy);
    }

    private void UpdateEnergy(int current, int max)
    {
        transform.DOKill();
        transform.DOPunchScale(Vector3.one * 1.15f,0.5f);
        _text.text = $"{current}/{max}";
    }

}
