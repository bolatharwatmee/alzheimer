using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    private ICardManager _cardManager;
    private IAudioManager _audioManager;
    private IInputManager _inputManager;
    private ScoreDisplay _scoreDisplay;
    private IScoreManager _scoreManager;

    void Awake()
    {
        ServiceLocator.RegisterService(this);
    }

    void Start()
    {
        _cardManager = ServiceLocator.GetService<ICardManager>();
        _audioManager = ServiceLocator.GetService<IAudioManager>();
        _inputManager = ServiceLocator.GetService<IInputManager>();
        _scoreDisplay = ServiceLocator.GetService<ScoreDisplay>();
        _scoreManager = new ScoreManager(_scoreDisplay);
        _cardManager.InitializeCards();
    }

    void Update()
    {
        _inputManager.HandleInput();
    }

    public void CardFlipped(ICard card)
    {
        _cardManager.FlipCard(card);
        _audioManager.PlaySound(SoundType.Flip);
    }
    
    public void OnCardMatch(bool isMatch)
    {
        if (isMatch)
        {
            _audioManager.PlaySound(SoundType.Match);
            _scoreManager.AddPoints(10); 
        }
        else
        {
            _audioManager.PlaySound(SoundType.Mismatch);
            _scoreManager.SubtractPoints(5); 
        }

        if ( _cardManager.GetMinGameOver() >= _scoreManager.GetCurrentScore())
        {
            GameOver();
        }
    }

    private void GameOver()
    {
        _audioManager.PlaySound(SoundType.GameOver);
        _scoreDisplay.ShowGameOver();
        _scoreManager.ResetScore();
        _cardManager.ClearGame();
        StartCoroutine(WaitToNewCards());
    }

    IEnumerator WaitToNewCards()
    {
        yield return new WaitForSeconds(4);
        _cardManager.InitializeCards();
        
    }
    void OnDestroy()
    {
        ServiceLocator.UnregisterService<GameManager>();
    }
}