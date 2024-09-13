using System.Collections.Generic;
using UnityEngine;

public class CardShuffler : ICardShuffler
{
    public void ShuffleCards<T>(List<T> cards)
    {
        for (int i = 0; i < cards.Count; i++)
        {
            int randomIndex = Random.Range(i, cards.Count);
            T temp = cards[i];
            cards[i] = cards[randomIndex];
            cards[randomIndex] = temp;
        }
    }
}