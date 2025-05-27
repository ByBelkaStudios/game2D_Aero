using UnityEngine;

namespace Data
{
    [CreateAssetMenu(fileName = "SoGameController", menuName = "Data/Controller", order = 0)]
    public class SoGameController : ScriptableObject
    {
        public SongData currentSongData;

        public void SetSong(SongData songData)
        {
            currentSongData = songData;
        }

        public void ResetSong()
        {
            currentSongData = null;
        }
    }
}