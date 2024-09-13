public interface ISaveLoadable
{
    void SaveGame(GameData data);
    GameData LoadGame();
}