using UnityEngine;

namespace Data
{
    [CreateAssetMenu(fileName = "SongEntry", menuName = "SongEntry", order = 3)]
    public class SoSongEntry : ScriptableObject
    {
        public string songName;
        public string songAuthor;
        
        public SongData easyData;
        public SongData mediumData;
        public SongData hardData;
        
    }
}