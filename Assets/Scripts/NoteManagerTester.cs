using UnityEngine;

public class NoteManagerTester : MonoBehaviour
{
    private string testNoteId;

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.A))
        {
            NoteData note = NoteManager.Instance.AddNote(
                "Backend Test Note",
                "This note was created by pressing A."
            );

            testNoteId = note.noteId;

            Debug.Log("TEST: Created note with ID: " + testNoteId);
        }

        if (Input.GetKeyDown(KeyCode.E))
        {
            if (string.IsNullOrEmpty(testNoteId))
            {
                Debug.LogWarning("TEST: No test note selected. Press A first.");
                return;
            }

            NoteManager.Instance.EditNote(
                testNoteId,
                "Edited Backend Test Note",
                "This note was edited by pressing E."
            );

            Debug.Log("TEST: Edited note with ID: " + testNoteId);
        }

        if (Input.GetKeyDown(KeyCode.C))
        {
            if (string.IsNullOrEmpty(testNoteId))
            {
                Debug.LogWarning("TEST: No test note selected. Press A first.");
                return;
            }

            NoteManager.Instance.AddChecklistItem(
                testNoteId,
                "First checklist item"
            );

            Debug.Log("TEST: Added checklist item to note with ID: " + testNoteId);
        }

        if (Input.GetKeyDown(KeyCode.T))
        {
            if (string.IsNullOrEmpty(testNoteId))
            {
                Debug.LogWarning("TEST: No test note selected. Press A first.");
                return;
            }

            NoteManager.Instance.ToggleChecklistItem(testNoteId, 0);

            Debug.Log("TEST: Toggled checklist item for note with ID: " + testNoteId);
        }

        if (Input.GetKeyDown(KeyCode.H))
        {
            if (string.IsNullOrEmpty(testNoteId))
            {
                Debug.LogWarning("TEST: No test note selected. Press A first.");
                return;
            }

            NoteManager.Instance.ToggleNoteVisibility(testNoteId);

            NoteData note = NoteManager.Instance.FindNoteById(testNoteId);

            if (note != null)
            {
                Debug.Log("TEST: Note visibility is now: " + note.isVisible);
            }
        }

        if (Input.GetKeyDown(KeyCode.D))
        {
            if (string.IsNullOrEmpty(testNoteId))
            {
                Debug.LogWarning("TEST: No test note selected. Press A first.");
                return;
            }

            NoteManager.Instance.DeleteNote(testNoteId);

            Debug.Log("TEST: Deleted note with ID: " + testNoteId);

            testNoteId = "";
        }

        if (Input.GetKeyDown(KeyCode.X))
        {
            NoteManager.Instance.ClearAllNotes();

            testNoteId = "";

            Debug.Log("TEST: Cleared all notes.");
        }

        if (Input.GetKeyDown(KeyCode.L))
        {
            Debug.Log("TEST: Current number of notes: " + NoteManager.Instance.GetAllNotes().Count);

            foreach (NoteData note in NoteManager.Instance.GetAllNotes())
            {
                Debug.Log(
                    "NOTE: " + note.noteId +
                    " | Title: " + note.title +
                    " | Content: " + note.content +
                    " | Visible: " + note.isVisible +
                    " | Checklist Count: " + note.checklistItems.Count
                );
            }
        }
    }
}