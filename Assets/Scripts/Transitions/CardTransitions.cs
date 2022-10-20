using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UIElements;
public class CardTransitions : MonoBehaviour
{
    [SerializeField] private DynamicList<Transition> _transitions = new DynamicList<Transition>();
     
    private void Start()
    {
        CardTransition.Move = DoCardTransition;
    }

    public void DoCardTransition(Card card, CardZone from, CardZone to)
    {
        Transition transition = GetTransition(card, from, to);
        if(transition == null)
        {
            transition = new DefaultTransition();
        }
        card.GetComponent<CardVisuals>().ResetVisuals();
        transition.Move(card, from, to);
    }

    private Transition GetTransition(Card card, CardZone from, CardZone to)
    {
        return _transitions.FirstOrDefault(t => t.Validate(card, from, to));
    }
}

[Serializable]
public abstract class Transition
{
    public abstract bool Validate(Card card, CardZone from, CardZone to);

    public abstract void Move(Card card, CardZone from, CardZone to);
}

[Serializable]
public abstract class Transition<TFrom, TTo>: Transition where TFrom: CardZone where TTo : CardZone
{
    public override bool Validate(Card card, CardZone from, CardZone to)
    {
        return from.GetType() == typeof(TFrom) && to.GetType() == typeof(TTo);
    }

    public override void Move(Card card, CardZone from, CardZone to)
    {
        Move(card, (TFrom)from, (TTo)to);
    }

    protected abstract void Move(Card card, TFrom from, TTo to);
}
