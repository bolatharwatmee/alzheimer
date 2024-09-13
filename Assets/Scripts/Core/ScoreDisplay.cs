using DG.Tweening;
using TMPro;
using UnityEngine;

public class ScoreDisplay : MonoBehaviour, IScoreDisplay
{
    [SerializeField] private TextMeshProUGUI scoreText;
    [SerializeField] private GameObject gameOverScreen;
    private void Awake()
    {
        ServiceLocator.RegisterService(this);
        gameOverScreen.transform.localScale = Vector3.zero;
    }

    public void UpdateScore(int currentScore)
    {
        scoreText.text = "Score: " + currentScore;
    }

    public void ShowGameOver()
    {
        gameOverScreen.transform.DOScale(Vector3.one, 1)
            .SetEase(Ease.InSine)
            .OnComplete(HideGameOver); 
    }

    private void HideGameOver()
    {
        gameOverScreen.transform.DOScale(Vector3.zero, 1)
            .SetDelay(3)
            .SetEase(Ease.OutSine);
    }
}