public interface IScoreManager
{
    void AddPoints(int points);
    void SubtractPoints(int points);
    int GetCurrentScore();
    void ResetScore();
}