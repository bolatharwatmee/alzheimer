using UnityEngine;

public class InputManager : MonoBehaviour, IInputManager
{
    void Awake()
    {
        ServiceLocator.RegisterService<IInputManager>(this);
    }

    public void HandleInput()
    {
        if (Input.GetMouseButtonDown(0))
        {
            RaycastHit2D hit = Physics2D.Raycast(Camera.main.ScreenToWorldPoint(Input.mousePosition), Vector2.zero);

            if (hit.collider != null)
            {
                ICard card = hit.transform.GetComponent<ICard>();
                if (card != null)
                {
                    var gameManager = ServiceLocator.GetService<GameManager>();
                    gameManager.CardFlipped(card);
                }
            }
        }
    }

    void OnDestroy()
    {
        ServiceLocator.UnregisterService<IInputManager>();
    }
}