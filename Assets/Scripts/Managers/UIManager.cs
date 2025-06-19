using TMPro;
using UnityEngine;
using UnityEngine.SocialPlatforms.Impl;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    [SerializeField] private TMP_Text myScoreText;
    [SerializeField] private TMP_Text myComboText;
    [SerializeField] private TMP_Text myMulText;

    [SerializeField] private Slider slider;
    [SerializeField] private PulseController scorePulse;

    private void OnEnable()
    {
        GameManager.Instance.OnNoteSuccess += OnNoteSuccess;
        GameManager.Instance.OnHealthChanged += RefreshHealth;
        GameManager.Instance.OnUIUpdate += Refresh;
    }

    private void OnDisable()
    {
        GameManager.Instance.OnNoteSuccess -= OnNoteSuccess;
        GameManager.Instance.OnHealthChanged -= RefreshHealth;
        GameManager.Instance.OnUIUpdate -= Refresh;
    }

    private void OnDestroy()
    {
        GameManager.Instance.OnNoteSuccess -= OnNoteSuccess;
        GameManager.Instance.OnHealthChanged -= RefreshHealth;
        GameManager.Instance.OnUIUpdate -= Refresh;
    }

    private void OnNoteSuccess()
    {
        //scorePulse.Pulse();
    }

    private void RefreshHealth()
    {
        slider.value = GameManager.Instance.HealthBar.CurrentHealth;
    }

    private void Refresh()
    {
        string formatted = GameManager.Instance.Score.ToString().PadRight(7, '0');
        string combo = GameManager.Instance.Combo.ToString().PadRight(3, '0');
        float mul = Mathf.Round(GameManager.Instance.Multiplier);

        myScoreText.text = formatted;
        myComboText.text = combo;
        myMulText.text = mul.ToString();
    }

    private void Start()
    {
        slider.maxValue = GameManager.Instance.HealthBar.MaxHealth;
    }
}
