using TMPro;
using UnityEngine;

public class Score : MonoBehaviour
{
    [SerializeField] private TMP_Text scoreText;

    [SerializeField] private TMP_Text nameText;
    [SerializeField] private TMP_Text authorText;

    public SongData songdata;

    private void Start()
    {
        scoreText.text = PlayerPrefs.HasKey("score")? $"{PlayerPrefs.GetString("score")}": "Last Score 0";

        nameText.text = songdata.songName;
        authorText.text = songdata.songAuthor;
    }
}
