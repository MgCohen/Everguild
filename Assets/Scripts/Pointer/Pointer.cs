using System;
using System.Collections;
using System.Collections.Generic;

public class Pointer
{
    public static Func<CardZone> PointedCardZone;
    public static Func<Card> PointedCard;
    public static Func<Card> DraggedCard;

    public static Action<Card> StartDragging = delegate { };
    public static Action<Card> StopDragging = delegate { };
}
