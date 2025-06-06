using TMPro;
using UnityEngine;
using UnityEngine.InputSystem;
using Data;

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

    [SerializeField] private float distanceLimit = 2.0f;

    [SerializeField] private GameObject judmentLineAbove;
    [SerializeField] private GameObject judmentLineBellow;
    [SerializeField] private GameObject judmentLineLeft;
    [SerializeField] private GameObject judmentLineRight;

    private InputAction clickAboveAction;
    private InputAction clickBellowAtion;
    private InputAction clickLeftAtion;
    private InputAction clickRightAtion;

    [SerializeField] private GameObject scoresGroup;

    [SerializeField] private GameObject clickedNoteFeedback;

    public SoNoteTypes noteTypes;

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

    private void NoteSuccess(GameObject noteGroup, Vector2 targetPos)
    {
        var notePosition = noteGroup.transform.position;

        var feedbackNote = Instantiate(clickedNoteFeedback, notePosition, Quaternion.identity);
        feedbackNote.transform.position = notePosition;

        feedbackNote.GetComponent<ClickedNoteFeedback>().EnableNote(targetPos);

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
        judmentLineAbove.GetComponent<PulseController>().Pulse();

        if (noteGroupAbove.transform.childCount == 0)
        {
            return;
        }

        var distance = Mathf.Abs(noteGroupAbove.transform.GetChild(0).position.y - targetPositionAbove.position.y);

        if(distance <= distanceLimit)
        {
            NoteSuccess(noteGroupAbove.transform.GetChild(0).gameObject, noteTypes.aboveData.targetLocation);
        }
        else
        {
            NoteMiss();
        }
    }

    private void ClickBellow(InputAction.CallbackContext context)
    {
        judmentLineBellow.GetComponent<PulseController>().Pulse();

        if (noteGroupBellow.transform.childCount == 0)
        {
            return;
        }

        var distance = Mathf.Abs(noteGroupBellow.transform.GetChild(0).position.y - targetPositionBellow.position.y);

        if (distance <= distanceLimit)
        {
            NoteSuccess(noteGroupBellow.transform.GetChild(0).gameObject, noteTypes.bellowData.targetLocation);
        }
        else
        {
            NoteMiss();
        }
    }

    private void ClickLeft(InputAction.CallbackContext context) 
    {
        judmentLineLeft.GetComponent<PulseController>().Pulse();

        if (noteGroupLeft.transform.childCount == 0)
        {
            return;
        }

        var distance = Mathf.Abs(noteGroupLeft.transform.GetChild(0).position.x - targetPositionLeft.position.x);

        if (distance <= distanceLimit)
        {
            NoteSuccess(noteGroupLeft.transform.GetChild(0).gameObject, noteTypes.leftData.targetLocation);
        }
        else
        {
            NoteMiss();
        }
    }

    private void ClickRight(InputAction.CallbackContext context)
    {
        judmentLineRight.GetComponent<PulseController>().Pulse();

        if (noteGroupRight.transform.childCount == 0)
        {
            return;
        }

        var distance = Mathf.Abs(noteGroupRight.transform.GetChild(0).position.x - targetPositionRight.position.x);

        if (distance <= distanceLimit)
        {
            NoteSuccess(noteGroupRight.transform.GetChild(0).gameObject, noteTypes.rightData.targetLocation);

        }
        else
        {
            NoteMiss();
        }
    }
}
