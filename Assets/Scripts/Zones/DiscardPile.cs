public class DiscardPile: CardZone
{
    protected override void CardAdded(Card card)
    {
        base.CardAdded(card);
        card.gameObject.SetActive(false);
    }
}