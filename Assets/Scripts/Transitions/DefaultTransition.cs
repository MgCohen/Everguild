using System.Linq;
using DG.Tweening;
using UnityEngine;

public class DefaultTransition : Transition
{
    public override bool Validate(Card card, CardZone from, CardZone to)
    {
        return true;
    }

    public override void Move(Card card, CardZone from, CardZone to)
    {
        float time = 1f;

        from.RemoveCard(card);
        card.transform.SetParent(to.Holder);

        Sequence sequence = DOTween.Sequence();
        sequence.Append(card.transform.DOLocalMove(Vector3.zero, time));
        sequence.Join(card.transform.DOLocalRotate(Vector3.zero, time));
        sequence.AppendCallback(() => to.AddCard(card));
    }
}
