// ICardManager.cs
using System.Collections.Generic;

public interface ICardManager
{
    void InitializeCards();
    void ClearGame();
    void FlipCard(ICard card);
    int GetMinGameOver();
    
}