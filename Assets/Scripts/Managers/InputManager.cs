using UnityEngine;
using UnityEngine.InputSystem;

public class InputManager : MonoBehaviour
{
    public static InputManager Instance { get; private set; }
    private InputSystem_Actions gameInputActions;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            gameInputActions = new InputSystem_Actions();
        }
        else
        {
            Destroy(gameObject);
        }
    }

    private void OnEnable()
    {
        gameInputActions.Enable();
    }

    private void OnDisable()
    {
        gameInputActions.Disable();
    }

    public InputAction GetClickAboveAction()
    {
        return gameInputActions.RhythmGame.ClickAbove;
    }

    public InputAction GetClickBellowAction()
    {
        return gameInputActions.RhythmGame.ClickBellow;
    }

    public InputAction GetClickLeftAction()
    {
        return gameInputActions.RhythmGame.ClickLeft;
    }

    public InputAction GetClickRightAction()
    {
        return gameInputActions.RhythmGame.ClickRight;
    }
}
