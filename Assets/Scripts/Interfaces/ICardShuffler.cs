using System.Collections.Generic;

public interface ICardShuffler
{
    void ShuffleCards<T>(List<T> cards);
}