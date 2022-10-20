using System.Linq;
using UnityEngine;
using DG.Tweening;

public class HandToField : Transition<Hand, PlayField>
{
    protected override void Move(Card card, Hand from, PlayField to)
    {
        float time = 1f;

        to.TryGetNearSlot(card, out Slot slot);
        from.RemoveCard(card);
        card.transform.SetParent(slot.transform);
        slot.Drop(card);

        Sequence sequence = DOTween.Sequence();
        sequence.Append(card.transform.DOLocalMove(Vector3.zero, time));
        sequence.Join(card.transform.DOLocalRotate(new Vector3(-90, 0, 0), time));
        sequence.AppendCallback(() => to.AddCard(card));
    }
}
