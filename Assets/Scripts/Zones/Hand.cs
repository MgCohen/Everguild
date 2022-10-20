using System;
using DG.Tweening;
using UnityEngine;

public class Hand : CardZone
{
    private void Start()
    {
        Pointer.StartDragging += OnCardDrag;
    }

    protected override void CardAdded(Card card)
    {
        base.CardAdded(card);
        card.gameObject.AddComponent<Draggable>();
        if (_playing)
        {
            HighlightAvailableCards(Player.CurrentEnergy, Player.MaxEnergy);
        }
        OrderHand();
    }

    protected override void CardRemoved(Card card)
    {
        base.CardRemoved(card);
        card.gameObject.RemoveComponent<Draggable>();
        OrderHand();
    }

    private void OnCardDrag(Card card)
    {
        if (!Cards.Contains(card))
        {
            return;
        }

        Pointer.StopDragging += OnCardDrop;
    }

    private void OnCardDrop(Card card)
    {
        if (Player.Field.TryGetNearSlot(card, out Slot slot))
        {
            CardTransition.Move(card, this, Player.Field);
            Player.ChangeEnergy(-card.Cost);
        }
        else
        {
            OrderHand();
        }
        Pointer.StopDragging -= OnCardDrop;
    }

    private void OrderHand()
    {
        foreach (Card card in _cards)
        {
            if (card.Moving)
            {
                return;
            }
            Vector3 pos = GetCardPosition(card);
            card.transform.DOKill();
            card.transform.DOLocalMove(pos, 0.2f);
            card.transform.DOScale(Scale, 0.2f);

            Vector3 rot = Holder.localRotation.eulerAngles;
            rot.x = -90;

            card.transform.DOLocalRotate(rot, 0.2f);
        }
    }

    public Vector3 GetCardPosition(Card card)
    {
        if (!_cards.Contains(card))
        {
            return Vector3.zero;
        }

        float spacing = Mathf.Min(8 / _cards.Count, 1.5f);
        int count = _cards.Count;
        float startingX; ;

        if (_cards.Count % 2 == 0)
        {
            startingX = ((count / 2) - 0.5f) * (-spacing);
        }
        else
        {
            startingX = ((count - 1) / 2) * (-spacing);
        }
        int index = _cards.IndexOf(card);
        float depth = 0.01f * index;
        return new Vector3(startingX + (spacing * index), 0, depth);
    }

    #region Highlight
    private bool _playing = false;

    public void EnableCardPlay()
    {
        _playing = true;
        Player.OnEnergyChanged.AddListener(HighlightAvailableCards);
        HighlightAvailableCards(Player.CurrentEnergy, Player.MaxEnergy);
    }

    private void HighlightAvailableCards(int currentEnergy, int maxEnergy)
    {
        foreach (Card card in _cards)
        {
            bool canPlay = currentEnergy >= card.Cost;
            card.GetComponent<CardVisuals>().ToggleHighlight(canPlay);
        }
    }

    public void DisableCardPlay()
    {
        _playing = false;
        foreach (Card card in _cards)
        {
            card.GetComponent<CardVisuals>().ToggleHighlight(false);
        }
        Player.OnEnergyChanged.RemoveListener(HighlightAvailableCards);
    }
    #endregion
}