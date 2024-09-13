using System;
using System.Collections.Generic;
using UnityEngine;

public class CardManager : MonoBehaviour, ICardManager
{
    private List<ICard> _cards = new List<ICard>();
    private ICard _firstSelectedCard;
    private ICard _secondSelectedCard;
    private bool _isProcessingTurn = false; 

    private GameManager _gameManager;
    private ICardFactory _cardFactory;
    private ICardShuffler _cardShuffler;
    private ICardMatcher _cardMatcher;
    private GameConfig _gameConfig;
    private int _rows;
    private int _columns;
    private Vector2 _lastScreenSize;

   

    void Awake()
    {
        ServiceLocator.RegisterService<ICardManager>(this);
        
    }

    private void Start()
    {
        _gameManager = ServiceLocator.GetService<GameManager>();
        
        _gameConfig = Resources.Load<GameConfig>("GameConfig");
        //InitializeCards();
    }

    public void InitializeCards()
    {
        _cardShuffler = new CardShuffler(); 
        _cardFactory = new CardFactory(_gameConfig.cardPrefab, _gameConfig.cardSprites, _gameConfig.cardSpacing, _cardShuffler);
        _cardMatcher = new CardMatcher();
        
        _cards = _cardFactory.CreateCards(_gameConfig.rows, _gameConfig.columns, transform); 
    }


    public void FlipCard(ICard card)
    {
        if (_isProcessingTurn || card.IsMatched || card == _firstSelectedCard) return; 

        card.Flip();

        if (_firstSelectedCard == null)
        {
            _firstSelectedCard = card;
        }
        else
        {
            _secondSelectedCard = card;
            _isProcessingTurn = true; 

            bool isMatch = _cardMatcher.CheckMatch(_firstSelectedCard, _secondSelectedCard);
            _gameManager.OnCardMatch(isMatch);
            if (!isMatch)
            {
                Invoke(nameof(FlipBackCards), 1f);
            }
            else
            {
                _firstSelectedCard.SetMatched(true);
                _secondSelectedCard.SetMatched(true);
                ResetSelection();
            }
        }
    }

    private void FlipBackCards()
    {
        if (_firstSelectedCard != null) _firstSelectedCard.Flip();
        if (_secondSelectedCard != null) _secondSelectedCard.Flip();
        ResetSelection();
    }
    public void ClearGame()
    {
        foreach (ICard card in _cards)
        {
            GameObject cardObject = (card as Card).gameObject;
            Destroy(cardObject);
        }

        _cards.Clear();
        ResetSelection();
        _isProcessingTurn = false;
        _firstSelectedCard = null;
        _secondSelectedCard = null;
    }
    
    private void ResetSelection()
    {
        _firstSelectedCard = null;
        _secondSelectedCard = null;
        _isProcessingTurn = false; 
    }



    public int GetMinGameOver()
    {
        return _gameConfig.minGameOver;
    }

    void OnDestroy()
    {
        ServiceLocator.UnregisterService<ICardManager>();
    }
}
