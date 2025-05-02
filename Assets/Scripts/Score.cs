using TMPro;
using UnityEngine;

public class Score : MonoBehaviour
{
    [SerializeField] private TMP_Text scoreText;

    private void Start()
    {
        scoreText.text = PlayerPrefs.HasKey("score")? $"Last Score {GameManager.Instance.Score}": "Last Score 0";
    }
}
