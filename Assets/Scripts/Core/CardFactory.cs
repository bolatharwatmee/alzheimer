using System.Collections.Generic;
using UnityEngine;

public class CardFactory : ICardFactory
{
    private readonly GameObject _cardPrefab;
    private readonly List<Sprite> _cardSprites;
    private readonly float _spacing;
    private readonly ICardShuffler _cardShuffler;

    public CardFactory(GameObject cardPrefab, List<Sprite> cardSprites, float spacing, ICardShuffler cardShuffler)
    {
        _cardPrefab = cardPrefab;
        _cardSprites = cardSprites;
        _spacing = spacing;
        _cardShuffler = cardShuffler;
    }

    public List<ICard> CreateCards(int rows, int columns, Transform parent)
    {
        List<ICard> cards = new List<ICard>();
        float cardWidth = _cardPrefab.GetComponent<SpriteRenderer>().bounds.size.x;
        float cardHeight = _cardPrefab.GetComponent<SpriteRenderer>().bounds.size.y;

        Vector2 startPos = new Vector2(
            -((columns - 1) / 2f) * (cardWidth + _spacing),
            -((rows - 1) / 2f) * (cardHeight + _spacing)
        );
        
        int totalCardsNeeded = rows * columns;

        if (_cardSprites.Count * 2 < totalCardsNeeded)
        {
            Debug.LogError("Not enough unique card sprites to create pairs for the grid size.");
            return cards;
        }

        List<Sprite> cardPairList = GenerateCardPairs(totalCardsNeeded);

        _cardShuffler.ShuffleCards(cardPairList);

        int cardIndex = 0;
        for (int i = 0; i < rows; i++)
        {
            for (int j = 0; j < columns; j++)
            {
                if (cardIndex >= cardPairList.Count)
                {
                    Debug.LogError("Card index out of range. This should not happen.");
                    break;
                }

                Vector2 position = startPos + new Vector2(j * (cardWidth + _spacing), i * (cardHeight + _spacing));
                GameObject cardObject = Object.Instantiate(_cardPrefab, position, Quaternion.identity, parent);

                Card card = cardObject.GetComponent<Card>();
                card.frontSprite = cardPairList[cardIndex];
                card.ShowBack();

                cards.Add(card);
                cardIndex++;
            }
        }

        return cards;
    }

    private List<Sprite> GenerateCardPairs(int totalCardsNeeded)
    {
        List<Sprite> cardPairList = new List<Sprite>();

        for (int i = 0; i < totalCardsNeeded / 2; i++)
        {
            cardPairList.Add(_cardSprites[i % _cardSprites.Count]);
            cardPairList.Add(_cardSprites[i % _cardSprites.Count]);
        }

        return cardPairList;
    }
}