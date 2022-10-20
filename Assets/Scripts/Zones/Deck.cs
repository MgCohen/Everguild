using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class Deck : CardZone
{
    [SerializeField] private Hand _playerHand;


    protected override void CardAdded(Card card)
    {
        card.gameObject.SetActive(false);
        card.transform.localRotation = Quaternion.Euler(new Vector3(0, 0, 0));
        card.transform.localPosition = Vector3.zero;
    }

    public void Draw()
    {
        Card card = Cards.FirstOrDefault();
        if(card == null)
        {
            return;
        }
        CardTransition.Move(card, this, _playerHand);
    }
}
