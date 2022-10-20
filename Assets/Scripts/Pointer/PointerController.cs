using UnityEngine;

public class PointerController : MonoBehaviour
{
    public void Start()
    {
        Pointer.PointedCard = GetPointedCard;
        Pointer.PointedCardZone = GetPointedZone;
        Pointer.DraggedCard = GetDraggedCard;

        Pointer.StartDragging += StartDragging;
        Pointer.StopDragging += StopDragging;
    }

    private Card _card;

    public void StartDragging(Card card)
    {
        _card = card;
    }

    public void StopDragging(Card card)
    {
        _card = null;
    }

    private CardZone GetPointedZone()
    {
        return GetOnPointer<CardZone>(LayerMask.GetMask("Zone"));
    }

    private Card GetPointedCard()
    {
        return GetOnPointer<Card>(LayerMask.GetMask("Card"));
    }

    private Card GetDraggedCard()
    {
        return _card;
    }

    private T GetOnPointer<T>(LayerMask mask)
    {
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        if (Physics.Raycast(ray, out RaycastHit hit, 100, mask))
        {
            return hit.transform.GetComponent<T>();
        }
        return default(T);
    }
}
