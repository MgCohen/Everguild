using System.Linq;
using DG.Tweening;
using UnityEngine;

public class DiscardFromSlot : Transition<PlayField, DiscardPile>
{

    [SerializeField] private float _height;
    [SerializeField] private float _animationTime;
    [SerializeField] private float _spin;

    protected override void Move(Card card, PlayField from, DiscardPile to)
    {
        Transform cardTransform = card.transform;

        Sequence sequence = DOTween.Sequence();
        sequence.Append(cardTransform.DOMoveY(_height, _animationTime));
        sequence.Join(cardTransform.DOLocalRotate(new Vector3(0, 720, 0), _animationTime, RotateMode.FastBeyond360).SetRelative());
        sequence.AppendCallback(() =>
        {
            from.RemoveCard(card);
            to.AddCard(card);
        });
    }
}
