
public class ScoreManager : IScoreManager
{
    private int currentScore; 

    private readonly IScoreDisplay _scoreDisplay; 

    
    
    public ScoreManager(IScoreDisplay scoreDisplay)
    {
        _scoreDisplay = scoreDisplay;
        currentScore = 0;
        _scoreDisplay.UpdateScore(currentScore);
    }

    public void AddPoints(int points)
    {
        currentScore += points;
        _scoreDisplay.UpdateScore(currentScore);
    }

    public void SubtractPoints(int points)
    {
        currentScore -= points;
        _scoreDisplay.UpdateScore(currentScore);
    }

    public void ResetScore()
    {
        currentScore = 0;
        _scoreDisplay.UpdateScore(currentScore);
    }

    public int GetCurrentScore()
    {
        return currentScore;
    }
    
}