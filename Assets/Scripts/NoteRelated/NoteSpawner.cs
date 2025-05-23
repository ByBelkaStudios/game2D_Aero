using System;
using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class NoteSpawner : MonoBehaviour
{
    [SerializeField] private AudioSource myAudioSource;

    [SerializeField] private GameObject notePrefab;

    [SerializeField] private GameObject noteGroupAbove;
    [SerializeField] private GameObject noteGroupBellow;
    [SerializeField] private GameObject noteGroupLeft;
    [SerializeField] private GameObject noteGroupRight;

    [SerializeField] private Transform targetPositionAbove;
    [SerializeField] private Transform targetPositionBellow;
    [SerializeField] private Transform targetPositionLeft;
    [SerializeField] private Transform targetPositionRight;

    [SerializeField] private float trackLength = 4f;

    public SongData songdata;

    private float bpm;
    private int stepSubdivision;

    private float enlapsedTime;
    private int songCurrentStep;

    private float stepDuration;

    private string[] chartLines;

    private bool songPlaying = false;

    private void Awake()
    {
        songdata = GameManager.Instance.LoadSong();

        bpm = songdata.bpm;
        stepSubdivision = songdata.stepSubdivision;

        if (stepSubdivision % 4 == 0)
        {
            stepSubdivision = stepSubdivision / 4;
            stepDuration = 60 / (bpm * stepSubdivision);

            return;
        }

        Debug.Log($"Step subdivision cannot be: {stepSubdivision}, Please use power of 2.");
        ReturnMenu();
    }

    private void Start()
    {
        var stringVar = songdata.chartFile.text;
        if (stringVar.Length < 1)
        {
            Debug.Log("Song file is empty.");
            ReturnMenu();
            return;
        }

        StartCoroutine(WaitSong());

        string[] chartSplitLines = stringVar.Split(new[] { '\n', '\r' }, StringSplitOptions.RemoveEmptyEntries);
        chartLines = chartSplitLines;
    }

    private IEnumerator WaitSong()
    {
        yield return new WaitForSeconds(2.5f);
        myAudioSource.Play();
        songPlaying = true;
    }

    private void ReturnMenu()
    {
        GameManager.Instance.LoadScene("StartMenu");
    }

    private void SpawnAbove()
    {
        var noteSpawnLocation = new Vector2(0, targetPositionAbove.position.y + trackLength);

        var songOffset = myAudioSource.time % stepDuration;
        var note = Instantiate(notePrefab, noteSpawnLocation, Quaternion.identity, noteGroupAbove.transform);

        note.GetComponent<NoteMovement>().Enable(noteSpawnLocation, targetPositionAbove.position, bpm, songOffset);
    }

    private void SpawnBellow()
    {
        var noteSpawnLocation = new Vector2(0, targetPositionBellow.position.y - trackLength);

        var songOffset = myAudioSource.time % stepDuration;
        var note = Instantiate(notePrefab, noteSpawnLocation, Quaternion.identity, noteGroupBellow.transform);

        note.GetComponent<NoteMovement>().Enable(noteSpawnLocation, targetPositionBellow.position, bpm, songOffset);
    }

    private void SpawnLeft()
    {
        var noteSpawnLocation = new Vector2(targetPositionLeft.position.x - trackLength, 0);

        var songOffset = myAudioSource.time % stepDuration;
        var note = Instantiate(notePrefab, noteSpawnLocation, Quaternion.identity, noteGroupLeft.transform);

        note.GetComponent<NoteMovement>().Enable(noteSpawnLocation, targetPositionLeft.position, bpm, songOffset);
    }

    private void SpawnRight()
    {
        var noteSpawnLocation = new Vector2(targetPositionRight.position.x + trackLength, 0);

        var songOffset = myAudioSource.time % stepDuration;
        var note = Instantiate(notePrefab, noteSpawnLocation, Quaternion.identity, noteGroupRight.transform);

        note.GetComponent<NoteMovement>().Enable(noteSpawnLocation, targetPositionRight.position, bpm, songOffset);
    }

    int prevGenStep = -1;

    private void Update()
    {
        enlapsedTime = myAudioSource.time;

        songCurrentStep = (int)Mathf.Floor(enlapsedTime / stepDuration);

        if (songPlaying == true && songCurrentStep > prevGenStep)
        {
            int chartStepToRead = songCurrentStep + stepSubdivision;

            if (chartStepToRead >= chartLines.Length || myAudioSource.isPlaying is false)
            {
                Debug.Log("Beatmap ended at step: " + chartStepToRead + ", song will be stopped");
                songPlaying = false;
                ReturnMenu();

                songdata.scoreLeaderboard.Add(GameManager.Instance.Score);
                Debug.Log(songdata.scoreLeaderboard[1]);

                return;
            }

            if (chartStepToRead < 0)
            {
                return;
            }

            char[] characters = chartLines[chartStepToRead].ToCharArray();

            for (int i = 0; i < characters.Length; i++)
            {
                if (characters[i] == 'x')
                {
                    switch (i)
                    {
                        case 0: SpawnAbove(); break;
                        case 1: SpawnBellow(); break;
                        case 2: SpawnLeft(); break;
                        case 3: SpawnRight(); break;
                        default:
                            Debug.Log("Beatmap written incorrectly at step: " + chartStepToRead);
                            break;
                    }
                }
            }

            prevGenStep = songCurrentStep;
        }
    }

}
