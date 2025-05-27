using System;
using Data;
using UnityEngine;
using UnityEngine.UI;

namespace UI
{
    public class SongButton : MonoBehaviour
    {
        [SerializeField] private Button action;
        [SerializeField] private SoSongEntry entry;

        public void SetEntrySong(SoSongEntry data)
        {
            entry = data;
        }
        
        public Button GetButton() => action;

        private void OnDestroy()
        {
            action.onClick.RemoveAllListeners();
        }
    }
}