using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "GameConfig", menuName = "CardMatch/GameConfig", order = 1)]
public class GameConfig : ScriptableObject
{
    public GameObject cardPrefab;       
    public List<Sprite> cardSprites;
    public int minGameOver;
    public float cardSpacing = 0.1f;    
    public int rows = 4;               
    public int columns = 4;             
}