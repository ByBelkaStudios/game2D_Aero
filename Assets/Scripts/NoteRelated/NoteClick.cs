using System;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UIElements;

public class NoteClick : MonoBehaviour
{
    [SerializeField] private GameObject noteGroupAbove;
    [SerializeField] private GameObject noteGroupBellow;
    [SerializeField] private GameObject noteGroupLeft;
    [SerializeField] private GameObject noteGroupRight;

    [SerializeField] private Transform targetPositionAbove;
    [SerializeField] private Transform targetPositionBellow;
    [SerializeField] private Transform targetPositionLeft;
    [SerializeField] private Transform targetPositionRight;

    [SerializeField] private TMP_Text myScoreText;
    [SerializeField] private TMP_Text myComboText;
    [SerializeField] private TMP_Text myMultiplierText;

    [SerializeField] private AudioSource myAudioSource;

    [SerializeField] private int scoreScale = 1;

    [SerializeField] private float distanceLimit = 2.0f;

    private InputAction clickAboveAction;
    private InputAction clickBellowAtion;
    private InputAction clickLeftAtion;
    private InputAction clickRightAtion;

    private int score = 0;
    private int combo = 0;

    [SerializeField] private float scoreMultiplier = 1.00f;
    [SerializeField] private float scoreMultiplierIncrease = 0.25f;
    [SerializeField] private float scoreMultiplerDefault = 1.00f;

    private void Start()
    {
        clickAboveAction = InputManager.Instance.GetClickAboveAction();
        clickAboveAction.Enable();
        clickAboveAction.performed += ClickAbove;

        clickBellowAtion = InputManager.Instance.GetClickBellowAction();
        clickBellowAtion.Enable();
        clickBellowAtion.performed += ClickBellow;

        clickLeftAtion = InputManager.Instance.GetClickLeftAction();
        clickLeftAtion.Enable();
        clickLeftAtion.performed += ClickLeft;

        clickRightAtion = InputManager.Instance.GetClickRightAction();
        clickRightAtion.Enable();
        clickRightAtion.performed += ClickRight;
    }

    private void OnDestroy()
    {
        clickAboveAction.Disable();
        clickAboveAction.performed -= ClickAbove;

        clickBellowAtion.Disable();
        clickBellowAtion.performed -= ClickBellow;

        clickLeftAtion.Disable();
        clickLeftAtion.performed -= ClickLeft;

        clickRightAtion.Disable();
        clickRightAtion.performed -= ClickRight;
    }

    private void ClickAbove(InputAction.CallbackContext context)
    {
        if (noteGroupAbove.transform.childCount == 0)
        {
            return;
        }

        var distance = Mathf.Abs(noteGroupAbove.transform.GetChild(0).position.y - targetPositionAbove.position.y);

        if(distance <= distanceLimit)
        {
            scoreMultiplier += scoreMultiplierIncrease;
            combo++;

            UpdateScore(1);

            return;
        }

        scoreMultiplier = scoreMultiplerDefault;
        combo = 0;

        UpdateScore(0);
    }

    private void ClickBellow(InputAction.CallbackContext context)
    {
        if (noteGroupBellow.transform.childCount == 0)
        {
            return;
        }

        var distance = Mathf.Abs(noteGroupBellow.transform.GetChild(0).position.y - targetPositionAbove.position.y);


        if (distance <= distanceLimit)
        {
            scoreMultiplier += scoreMultiplierIncrease;
            combo++;

            UpdateScore(1);

            return;
        }

        scoreMultiplier = scoreMultiplerDefault;
        combo = 0;

        UpdateScore(0);
    }

    private void ClickLeft(InputAction.CallbackContext context) 
    {
        if (noteGroupLeft.transform.childCount == 0)
        {
            return;
        }

        var distance = Mathf.Abs(noteGroupLeft.transform.GetChild(0).position.x - targetPositionAbove.position.x);

        if (distance <= distanceLimit)
        {
            scoreMultiplier += scoreMultiplierIncrease;
            combo++;

            UpdateScore(1);

            return;
        }

        scoreMultiplier = scoreMultiplerDefault;
        combo = 0;

        UpdateScore(0);
    }

    private void ClickRight(InputAction.CallbackContext context)
    {
        if (noteGroupRight.transform.childCount == 0)
        {
            return;
        }

        var distance = Mathf.Abs(noteGroupRight.transform.GetChild(0).position.x - targetPositionAbove.position.x);

        if (distance <= distanceLimit)
        {
            scoreMultiplier += scoreMultiplierIncrease;
            combo++;

            UpdateScore(1);

            return;
        }

        scoreMultiplier = scoreMultiplerDefault;
        combo = 0;

        UpdateScore(0);
    }

    private void UpdateScore(int hit)
    {
        score += (int)MathF.Round(hit * scoreMultiplier * scoreScale);

        myScoreText.text = ("Score: " + score.ToString());
        myComboText.text = ("Combo: " + combo.ToString());
        myMultiplierText.text = ("Mul: " + scoreMultiplier.ToString());
    }

}
