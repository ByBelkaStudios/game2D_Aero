using System.Collections.Generic;
using Data;
using UnityEngine;

[CreateAssetMenu(fileName = "SongData", menuName = "SongData", order = 1)]
public class SongData : ScriptableObject
{
    public List<int> scoreLeaderboard = new List<int>();

    public float grade;

    public TextAsset chartFile;

    public int stepSubdivision; 
    
    public AudioClip songAudioFile;
    public float bpm;
    
    public ESongDifficulty songDifficulty;
}
