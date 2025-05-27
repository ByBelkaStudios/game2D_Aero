using System;
using System.Collections.Generic;
using Data;
using TMPro;
using UI;
using UnityEngine;
using UnityEngine.UI;

[System.Serializable]
public class WindowData
{
    [SerializeField] private GameObject windowGameObject;
    [SerializeField] private TMP_Text windowSongName;
    
    public Button easy, medium, hard;
    
    public GameObject WindowGameObject => windowGameObject;
    public TMP_Text WindowSongName => windowSongName;
    
}

public class SongSelector : MonoBehaviour
{

    [SerializeField] private List<SoSongEntry> songList;

    [SerializeField] private GameObject prefabFolder;
    
    [SerializeField] private GameObject content;
    
    [SerializeField] private WindowData windowData;
    
    private void Start()
    {
        LoadSongList();
    }

    private void LoadSongList()
    {
        foreach (var entry in songList)
        {
            GameObject go = Instantiate(prefabFolder, content.transform);
            go.GetComponent<SongButton>().SetEntrySong(entry);
            go.GetComponent<SongButton>().GetButton().onClick.AddListener(() =>
            {
                OpenWindow(entry);
            });
            go.GetComponentInChildren<TMP_Text>().text = entry.songName;
        } 
    }

    private void OpenWindow(SoSongEntry entry)
    {
        windowData.easy.onClick.RemoveAllListeners();
        windowData.medium.onClick.RemoveAllListeners();
        windowData.hard.onClick.RemoveAllListeners();

        windowData.easy.onClick.AddListener(() =>
        {
            SelectDifficulty(entry, 1);
        });

        
        windowData.medium.onClick.AddListener(() =>
        {
            SelectDifficulty(entry, 2);
        });

        windowData.hard.onClick.AddListener(() =>
        {
            SelectDifficulty(entry, 3);
        });
        
        windowData.WindowSongName.text = entry.songName;
        windowData.WindowGameObject.SetActive(true);
    }
    
    private void SelectDifficulty(SoSongEntry songEntry, int difficulty)
    {
        switch (difficulty)
        {
            case 1:
                GameManager.Instance.gamedata.currentSongData = songEntry.easyData;
                Debug.Log("Difficulty on Easy");
                break;
            case 2:
                GameManager.Instance.gamedata.currentSongData = songEntry.mediumData;
                Debug.Log("Difficulty on Medium");
                break;
            case 3:
                GameManager.Instance.gamedata.currentSongData = songEntry.hardData;
                Debug.Log("Difficulty on Hard");
                break;
            default:
                Debug.Log("Difficulty not found");
                break;
        }
    }

    //O unity deixa cache!! Apanhar o currentSongData do scriptableobject no inspector do mesmo!
    private void Play()
    {
        if (GameManager.Instance.gamedata.currentSongData is null)
        {
            return;
        }
        
        GameManager.Instance.LoadScene("Gameplay");
    }
}
