using TMPro;
using UnityEngine;

public class Score : MonoBehaviour
{
    [SerializeField] private TMP_Text scoreText;

    private void Start()
    {
        scoreText.text = PlayerPrefs.HasKey("score")? $"{PlayerPrefs.GetString("score")}": "Last Score 0";
    }
}
