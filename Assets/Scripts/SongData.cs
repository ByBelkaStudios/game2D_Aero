using System.Collections.Generic;
using Data;
using UnityEngine;

[CreateAssetMenu(fileName = "SongData", menuName = "SongData", order = 1)]
public class SongData : ScriptableObject
{
    public int score;
    public List<int> scoreLeaderboard = new List<int>();

    public TextAsset chartFile;

    public int stepSubdivision; 

    public string songName;
    public string songAuthor;
    
    public AudioClip songAudioFile;
    public float bpm;
    
    public ESongDifficulty songDifficulty;
}
