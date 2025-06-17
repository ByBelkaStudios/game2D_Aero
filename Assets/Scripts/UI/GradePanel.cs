using Data;
using TMPro;
using UnityEngine;

public class GradePanel : MonoBehaviour
{
    [SerializeField] private TMP_Text tinyLevelStateText;
    [SerializeField] private TMP_Text bigLevelStateText;
    [SerializeField] private TMP_Text scoreText;
    [SerializeField] private TMP_Text hitsText;
    [SerializeField] private TMP_Text missesText;
    [SerializeField] private TMP_Text gradeText;

    [SerializeField] private GameObject gradePanel;

    public SoGameController gameData;

    private void OnEnable()
    {
        GameManager.Instance.OnSongEnd += EnablePanel;
    }

    private void OnDisable()
    {
        GameManager.Instance.OnSongEnd -= EnablePanel;
    }

    private void OnDestroy()
    {
        GameManager.Instance.OnSongEnd -= EnablePanel;
    }


    private void EnablePanel()
    {
        //read info

        if(gameData.currentSongData.grade <= 50)
        {
            tinyLevelStateText.text = "Level failed";
            bigLevelStateText.text = "Failed";
            scoreText.text = "Score: " + GameManager.Instance.Score.ToString();
            gradeText.text = "Grade: F";

            gradePanel.SetActive(true);
            return;
        }

        tinyLevelStateText.text = "Level Passed";
        bigLevelStateText.text = "Success";
        scoreText.text = "Score: " + GameManager.Instance.Score.ToString();
        gradeText.text = "Grade: X";

        gradePanel.SetActive(true);
    }
}
