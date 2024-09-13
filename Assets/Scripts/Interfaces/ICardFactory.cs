using System.Collections.Generic;
using UnityEngine;

public interface ICardFactory
{
    List<ICard> CreateCards(int rows, int columns, Transform parent);
}