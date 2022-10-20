using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using DG.Tweening;
using UnityEngine;

public class PlayField : CardZone
{
    [SerializeField] private List<Slot> _slots = new List<Slot>();
    [SerializeField] private float _minDistance;

    public List<Slot> GetSlots()
    {
        return _slots;
    }

    public bool TryGetNearSlot(Card card, out Slot slot)
    {
        slot = _slots.Where(s => s.SlottedCard == null)
                     .Where(s => Vector3.Distance(s.transform.position, card.transform.position) < _minDistance)
                     .OrderBy(s => Vector3.Distance(s.transform.position, card.transform.position))
                     .FirstOrDefault();
        return slot != null;
    }
}
