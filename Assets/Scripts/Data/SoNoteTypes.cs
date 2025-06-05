using UnityEngine;

namespace Data
{
    [CreateAssetMenu(fileName = "SoNoteTypes", menuName = "SoNoteTypes", order = 4)]
    public class SoNoteTypes : ScriptableObject
    {
        public NoteData aboveData;
        public NoteData bellowData;
        public NoteData leftData;
        public NoteData rightData;
    }
}