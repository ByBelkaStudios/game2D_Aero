using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "ScriptableObjects/SongData")]
public class SongData : ScriptableObject
{
    public int score;
    public List<int> scoreLeaderboard = new List<int>();

    public TextAsset chartFile;
    public float bpm;

    public int stepSubdivision; 

    public string songName;
    public string songAuthor;
    public string songDifficulty;
}
