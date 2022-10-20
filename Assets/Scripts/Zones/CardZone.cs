using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public abstract class CardZone : MonoBehaviour
{
    [SerializeField] private Player _player;
    [SerializeField] private Transform _holder;
    [SerializeField] private float _cardScale;
    [SerializeField] private float _dragOffset;

    public Transform Holder => GetHolder();
    public float Scale => _cardScale;
    public float DragOffset => _dragOffset;
    public Player Player => _player;

    protected List<Card> _cards = new List<Card>();
    public List<Card> Cards => _cards;

    [HideInInspector] public UnityEvent<Card> OnCardAdded = new UnityEvent<Card>();
    [HideInInspector] public UnityEvent<Card> OnCardRemoved = new UnityEvent<Card>();

    protected virtual Transform GetHolder()
    {
        return _holder;
    }

    public void AddCard(Card card)
    {
        _cards.Add(card);
        card.transform.SetParent(_holder);
        CardAdded(card);
        OnCardAdded.Invoke(card);
    }

    protected virtual void CardAdded(Card card)
    {

    }

    public void RemoveCard(Card card)
    {
        _cards.Remove(card);
        CardRemoved(card);
        OnCardRemoved.Invoke(card);
    }

    protected virtual void CardRemoved(Card card)
    {
        
    }
}
