using UnityEngine;

public class NoteObject : MonoBehaviour
{
    [SerializeField] private string noteId;

    public string NoteId
    {
        get { return noteId; }
    }

    public void Initialize(string id)
    {
        noteId = id;
        Debug.Log("NoteObject initialized with noteId: " + noteId);
    }

    public void DeleteThisNote()
    {
        if (string.IsNullOrEmpty(noteId))
        {
            Debug.LogWarning("Cannot delete note because noteId is empty.");
            return;
        }

        NoteManager.Instance.DeleteNote(noteId);
        Destroy(gameObject);
    }

    public void ToggleVisibility()
    {
        if (string.IsNullOrEmpty(noteId))
        {
            Debug.LogWarning("Cannot toggle visibility because noteId is empty.");
            return;
        }

        NoteManager.Instance.ToggleNoteVisibility(noteId);

        NoteData note = NoteManager.Instance.FindNoteById(noteId);

        if (note != null)
        {
            gameObject.SetActive(note.isVisible);
        }
    }
}