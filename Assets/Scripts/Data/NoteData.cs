using UnityEngine;

[CreateAssetMenu(fileName = "NoteData", menuName = "NoteData", order = 2)]
public class NoteData : ScriptableObject
{
    public Vector2 targetLocation;
    public bool goesLeft;
}
