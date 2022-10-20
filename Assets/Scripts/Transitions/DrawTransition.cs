using System;
using DG.Tweening;
using UnityEngine;

public class DrawTransition : Transition<Deck, Hand>
{
    [SerializeField] private float _drawTime = 0.75f;
    [SerializeField] private float _animationTime = 1f;
    [SerializeField] private float _finalScale = 0.3f;
    [SerializeField] private Transform _drawTarget;

    protected override void Move(Card card, Deck from, Hand to)
    {
        card.GetComponent<CardVisuals>().ToggleBack(true);
        card.Moving = true;
        from.RemoveCard(card);
        card.gameObject.SetActive(true);
        to.AddCard(card);
        Draw(card, to);
    }

    private void Draw(Card card, Hand to)
    {
        card.transform.DOMove(_drawTarget.position, _drawTime).OnComplete(() => MoveToHand(card, to));
    }

    private void MoveToHand(Card card, Hand to)
    {
        card.GetComponent<Animator>().SetTrigger("Flip");

        Tweener tween = card.transform.DOLocalMove(to.GetCardPosition(card), _animationTime).SetEase(Ease.Linear);
        float missingTime = _animationTime;
        Vector3 target = to.GetCardPosition(card);
        tween.OnUpdate(() =>
        {
            Vector3 newTarget = to.GetCardPosition(card);
            if (target != newTarget)
            {
                missingTime -= tween.Elapsed();
                tween.ChangeEndValue(to.GetCardPosition(card), missingTime, true);
            }
        });

        card.transform.DOScale(_finalScale, _animationTime);

        Vector3 rot = to.Holder.localRotation.eulerAngles;
        rot.x = -90;
        card.transform.DOLocalRotate(rot, _animationTime).OnComplete(() => card.Moving = false);
    }

}
