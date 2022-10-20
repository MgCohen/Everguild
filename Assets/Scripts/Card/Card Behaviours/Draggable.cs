using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;
using UnityEngine.EventSystems;

public class Draggable : MonoBehaviour, IDragHandler, IBeginDragHandler, IEndDragHandler
{
    private Card _card;

    private void Start()
    {
        _card = GetComponent<Card>();
    }

    public void OnBeginDrag(PointerEventData eventData)
    {
        Pointer.StartDragging(_card);
    }

    public void OnDrag(PointerEventData eventData)
    {
        Ray ray = Camera.main.ScreenPointToRay(eventData.position);
        bool hitted = Physics.Raycast(ray, out RaycastHit hit, 100, LayerMask.GetMask("Zone"));
        if (hitted)
        {
            CardZone zone = hit.transform.GetComponent<CardZone>();
            if (zone)
            {
                transform.localScale = new Vector3(zone.Scale, zone.Scale, zone.Scale);
            }
            transform.position = hit.point + (hit.normal * zone.DragOffset);
            transform.up = hit.normal;
        }
    }

    public void OnEndDrag(PointerEventData eventData)
    {
        Pointer.StopDragging(_card);
    }
}
