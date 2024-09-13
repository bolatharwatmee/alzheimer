public class CardMatcher : ICardMatcher
{
    public bool CheckMatch(ICard firstCard, ICard secondCard)
    {
        Card card1 = firstCard as Card;
        Card card2 = secondCard as Card;

        if (card1 != null && card2 != null && card1.frontSprite == card2.frontSprite)
        {
            // Set both cards as matched
            card1.SetMatched(true);
            card2.SetMatched(true);

            return true; 
        }

        return false; 
    }
}