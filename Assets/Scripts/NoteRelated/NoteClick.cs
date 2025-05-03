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

    [SerializeField] private int scoreScale = 1;

    [SerializeField] private float distanceLimit = 2.0f;

    private InputAction clickAboveAction;
    private InputAction clickBellowAtion;
    private InputAction clickLeftAtion;
    private InputAction clickRightAtion;

    private int score = 0;
    private int combo = 0;
    private float scoreMultiplier = 1.00f;

    [SerializeField] private float scoreMultiplierIncrease = 0.25f;
    [SerializeField] private float scoreMultiplerDefault = 1.00f;
    [SerializeField] private GameObject scoresGroup;

    private void OnEnable()
    {
        GameManager.Instance.OnNoteMiss += NoteMiss;
    }

    private void OnDisable()
    {
        GameManager.Instance.OnNoteMiss -= NoteMiss;
    }

    private void Start()
    {
        scoreMultiplier = scoreMultiplerDefault;

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

        GameManager.Instance.OnNoteMiss -= NoteMiss;
    }

    private void NoteSuccess(GameObject noteGroup)
    {
        Destroy(noteGroup);

        GameManager.Instance.ApplyPulse();

        GameManager.Instance.HealthBar.IncreaseHealth();

        GameManager.Instance.IncrementScore(1);
        GameManager.Instance.IncrementCombo();
        GameManager.Instance.IncrementMultiplier();

        GameManager.Instance.SuccessNoteSound.Play();
    }

    private void NoteMiss()
    {

        GameManager.Instance.ResetCombo();
        GameManager.Instance.ApplyImpulse();

        GameManager.Instance.MissNoteSound.Play();
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
            NoteSuccess(noteGroupAbove.transform.GetChild(0).gameObject);

            //click sound
        }
        else
        {
            NoteMiss();
        }
    }

    private void ClickBellow(InputAction.CallbackContext context)
    {
        if (noteGroupBellow.transform.childCount == 0)
        {
            return;
        }

        var distance = Mathf.Abs(noteGroupBellow.transform.GetChild(0).position.y - targetPositionBellow.position.y);

        if (distance <= distanceLimit)
        {
            NoteSuccess(noteGroupBellow.transform.GetChild(0).gameObject);

            //click sound
        }
        else
        {
            NoteMiss();
        }
    }

    private void ClickLeft(InputAction.CallbackContext context) 
    {

        if (noteGroupLeft.transform.childCount == 0)
        {
            return;
        }

        var distance = Mathf.Abs(noteGroupLeft.transform.GetChild(0).position.x - targetPositionLeft.position.x);

        if (distance <= distanceLimit)
        {
            NoteSuccess(noteGroupLeft.transform.GetChild(0).gameObject);

            //click sound
        }
        else
        {
            NoteMiss();
        }
    }

    private void ClickRight(InputAction.CallbackContext context)
    {
        if (noteGroupRight.transform.childCount == 0)
        {
            return;
        }

        var distance = Mathf.Abs(noteGroupRight.transform.GetChild(0).position.x - targetPositionRight.position.x);

        if (distance <= distanceLimit)
        {
            NoteSuccess(noteGroupRight.transform.GetChild(0).gameObject);

            //click sound
        }
        else
        {
            NoteMiss();
        }
    }
}
