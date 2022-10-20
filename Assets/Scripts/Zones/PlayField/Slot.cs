using DG.Tweening;
using UnityEngine;

public class Slot: MonoBehaviour
{
    [SerializeField] private PlayField _field;
    public Card SlottedCard { get; private set; }

    private Player player => _field.Player;
    private Tween _scaleTween;

    public void Drop(Card card)
    {
        SlottedCard = card;
    }

    public void Dispose()
    {
        if(SlottedCard == null)
        {
            return;
        }
        CardTransition.Move(SlottedCard, player.Field, player.Discard);
        SlottedCard = null;
    }

    public void SetHighlight(bool state)
    {
        float scale = state ? 1.2f : 1f;
        if(_scaleTween.IsActive())
        {
            _scaleTween.Kill();
        }
        _scaleTween = transform.DOScale(scale, 0.2f);
    }
}