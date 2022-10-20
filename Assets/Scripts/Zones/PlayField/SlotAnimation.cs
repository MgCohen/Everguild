using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class SlotAnimation : MonoBehaviour
{
    [SerializeField] private Transform _slotHolder;
    [SerializeField] private PlayField _field;

    private Coroutine _draggingRoutine;
    private Slot _lastSelectedSlot;

    private void Start()
    {
        Pointer.StartDragging += OnDraggingStart;
        Pointer.StopDragging += OnDraggingStop;
    }

    private void OnDraggingStart(Card card)
    {
        if (_draggingRoutine != null)
        {
            StopCoroutine(_draggingRoutine);
        }
        _draggingRoutine = StartCoroutine(DraggingCheck());
    }

    private void OnDraggingStop(Card card)
    {
        if (_draggingRoutine != null)
        {
            StopCoroutine(_draggingRoutine);
        }
        _slotHolder.DOKill();
        _slotHolder.DOLocalMoveY(0.1f, 0.2f);
        RemoveSlotHighlights();
    }

    IEnumerator DraggingCheck()
    {
        bool isTargetted = false;
        while (true)
        {
            bool targetted = Pointer.PointedCardZone() == _field;
            if (targetted != isTargetted)
            {
                isTargetted = targetted;
                float height = isTargetted ? 0.15f : 0.1f;
                _slotHolder.DOKill();
                _slotHolder.DOLocalMoveY(height, 0.2f);
            }

            if (targetted)
            {
                CheckSlots();
            }

            yield return null;
        }
    }

    private void CheckSlots()
    {
        Card card = Pointer.DraggedCard();
        if (_field.TryGetNearSlot(card, out Slot slot))
        {
            if (slot == _lastSelectedSlot)
            {
                return;
            }
            _lastSelectedSlot?.SetHighlight(false);
            slot.SetHighlight(true);
            _lastSelectedSlot = slot;
        }
        else
        {
            _lastSelectedSlot?.SetHighlight(false);
        }
    }

    private void RemoveSlotHighlights()
    {
        foreach(Slot slot in _field.GetSlots())
        {
            slot.SetHighlight(false);
        }
    }
}
