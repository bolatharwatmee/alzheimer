using UnityEngine;
using DG.Tweening;

public class Card : MonoBehaviour, ICard
{
    public bool IsMatched { get; set; } 
    private bool _isFlipped = false;     
    private float _flipDuration = 0.5f;  
    private bool _isAnimating = false;   

    private SpriteRenderer _spriteRenderer;
    public Sprite frontSprite; 
    public Sprite backSprite;  

    private void Awake()
    {
        _spriteRenderer = GetComponent<SpriteRenderer>();
        ShowBack();
    }

    private void ShowFront()
    {
        _spriteRenderer.sprite = frontSprite;
    }

    public void ShowBack()
    {
        _spriteRenderer.sprite = backSprite;
    }

    
    public void Flip()
    {
        if (_isAnimating || IsMatched) return; 

        _isAnimating = true;
        _isFlipped = !_isFlipped;  

        transform.DORotate(new Vector3(0, 90, 0), _flipDuration / 2, RotateMode.LocalAxisAdd).OnComplete(() =>
        {
            if (_isFlipped)
                ShowFront();
            else
                ShowBack();

            transform.DORotate(new Vector3(0, 90, 0), _flipDuration / 2, RotateMode.LocalAxisAdd).OnComplete(() =>
            {
                _isAnimating = false;  
            });
        });
    }

    public void SetMatched(bool matched)
    {
        IsMatched = matched;
    }

    public void FlipBack()
    {
        if (!_isFlipped || IsMatched) return; 

        Flip(); 
    }
}
