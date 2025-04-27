using System;
using System.Collections;
using UnityEditor.Experimental.GraphView;
using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.Windows;

public class NoteSpawner : MonoBehaviour
{
    //NEW
    [SerializeField] private float bpm = 120;
    [SerializeField] private int stepSubdivision = 4;
    [SerializeField] private int continueStepsBehind = 2;

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

    private float enlapsedTime;
    private int songCurrentStep;

    private float stepDuration;

    private string[] chartLines;

    private void Awake()
    {
        if (stepSubdivision % 4 != 0)
        {
            //go back to menu, change throw
            Debug.LogError($"Step subdivision cannot be: {stepSubdivision}, Please use power of 2.");
            stepSubdivision = 4;
        }

        stepSubdivision = stepSubdivision / 4;
        stepDuration = 60 / (bpm * stepSubdivision);
    }

    private void Start()
    {
        var stringVar = gameObject.GetComponent<ChartReader>().ReadFile();
        if (stringVar.Length < 1)
        {
            Debug.LogError("Song file is empty.");
            return;
        }

        StartCoroutine(WaitSong());

        chartLines = stringVar;
    }

    private IEnumerator WaitSong()
    {
        yield return new WaitForSeconds(5f);
        myAudioSource.Play();
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

        if (songCurrentStep > prevGenStep)
        {
            if(songCurrentStep > chartLines.Length)
            {
                Debug.LogError("Beatmat ended, song will be stopped");
                myAudioSource.Stop();
                return;
            }
            char[] characters = chartLines[songCurrentStep].ToCharArray();

            for (int i = 0; i < characters.Length; i++)
            {
                if (characters[i] == 'x')
                {
                    switch (i)
                    {
                        case 0:
                            SpawnAbove();
                            break;
                        case 1:
                            SpawnBellow();
                            break;
                        case 2:
                            SpawnLeft();
                            break;
                        case 3:
                            SpawnRight();
                            break;
                        default:
                            Debug.LogError("Beatmeat written incorrectly at step: " + songCurrentStep);
                            break;
                    }
                }
            }

            prevGenStep = songCurrentStep;
        }
    }
}
