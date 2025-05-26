using UnityEngine;

public class SongSelector : MonoBehaviour
{
    //Each one is for the 3 difficulties

    public SongData songdata;

    public void SelectSong()
    {
        GameManager.Instance.SelectSongData = songdata;
        GameManager.Instance.LoadScene("Gameplay");
    }
}
