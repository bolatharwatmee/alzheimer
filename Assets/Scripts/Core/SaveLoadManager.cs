using UnityEngine;

[System.Serializable]
public class GameData
{
    public int score;
    // Add more properties as needed
}

public class SaveLoadManager : ISaveLoadable
{
    private const string SaveKey = "GameData";

    public void SaveGame(GameData data)
    {
        string jsonData = JsonUtility.ToJson(data);
        PlayerPrefs.SetString(SaveKey, jsonData);
        PlayerPrefs.Save();
    }

    public GameData LoadGame()
    {
        if (PlayerPrefs.HasKey(SaveKey))
        {
            string jsonData = PlayerPrefs.GetString(SaveKey);
            return JsonUtility.FromJson<GameData>(jsonData);
        }
        return null;
    }
}