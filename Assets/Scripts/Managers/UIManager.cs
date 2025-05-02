using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    [SerializeField] private TMP_Text myScoreText;
    [SerializeField] private TMP_Text myComboText;
    [SerializeField] private TMP_Text myMultiplierText;

    [SerializeField] private Slider slider;
    [SerializeField] private ScorePulse scorePulse;

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
        scorePulse.Pulse();
    }

    private void RefreshHealth()
    {
        slider.value = GameManager.Instance.HealthBar.CurrentHealth;
    }

    private void Refresh()
    {
        myScoreText.text = ("Score: " + GameManager.Instance.Score.ToString());
        myComboText.text = GameManager.Instance.Combo.ToString();
        myMultiplierText.text = ("Mul: " + GameManager.Instance.Multiplier.ToString());
    }

    private void Start()
    {
        slider.maxValue = GameManager.Instance.HealthBar.MaxHealth;
    }
}
