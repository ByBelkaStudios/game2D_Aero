using System;
using TMPro;
using UnityEngine;

public class UIManager : MonoBehaviour
{
    [SerializeField] private TMP_Text myScoreText;
    [SerializeField] private TMP_Text myComboText;
    [SerializeField] private TMP_Text myMultiplierText;

    private void OnEnable()
    {
        GameManager.Instance.OnUIUpdate += Refresh;
    }

    private void OnDisable()
    {
        GameManager.Instance.OnUIUpdate -= Refresh;
    }

    private void Refresh()
    {
        myScoreText.text = ("Score: " + GameManager.Instance.Score.ToString());
        myComboText.text = ("Combo: " + GameManager.Instance.Combo.ToString());
        myMultiplierText.text = ("Mul: " + GameManager.Instance.Multiplier.ToString());
    }
}
