using System;
using System.Collections.Generic;
using UnityEngine;

public class NoteManager : MonoBehaviour
{
    public static NoteManager Instance { get; private set; }

    public List<NoteData> notes = new List<NoteData>();

    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
            return;
        }

        Instance = this;
        notes = NoteStorage.LoadNotes();
    }

    public NoteData AddNote(string title, string content)
    {
        NoteData newNote = new NoteData(title, content);
        notes.Add(newNote);
        SaveNotes();

        Debug.Log("Note added: " + newNote.noteId);
        return newNote;
    }

    public void EditNote(string noteId, string newTitle, string newContent)
    {
        NoteData note = FindNoteById(noteId);

        if (note == null)
        {
            Debug.LogWarning("Edit failed. Note not found: " + noteId);
            return;
        }

        note.title = newTitle;
        note.content = newContent;
        note.updatedAt = DateTime.Now.ToString();

        SaveNotes();
        Debug.Log("Note edited: " + noteId);
    }

    public void DeleteNote(string noteId)
    {
        NoteData note = FindNoteById(noteId);

        if (note == null)
        {
            Debug.LogWarning("Delete failed. Note not found: " + noteId);
            return;
        }

        notes.Remove(note);
        SaveNotes();

        Debug.Log("Note deleted: " + noteId);
    }

    public void ToggleNoteVisibility(string noteId)
    {
        NoteData note = FindNoteById(noteId);

        if (note == null)
        {
            Debug.LogWarning("Visibility toggle failed. Note not found: " + noteId);
            return;
        }

        note.isVisible = !note.isVisible;
        note.updatedAt = DateTime.Now.ToString();

        SaveNotes();
        Debug.Log("Note visibility changed to: " + note.isVisible);
    }

    public void AddChecklistItem(string noteId, string itemText)
    {
        NoteData note = FindNoteById(noteId);

        if (note == null)
        {
            Debug.LogWarning("Add checklist failed. Note not found: " + noteId);
            return;
        }

        if (string.IsNullOrWhiteSpace(itemText))
        {
            Debug.LogWarning("Checklist item cannot be empty.");
            return;
        }

        note.checklistItems.Add(new ChecklistItem(itemText));
        note.updatedAt = DateTime.Now.ToString();

        SaveNotes();
        Debug.Log("Checklist item added to note: " + noteId);
    }

    public void ToggleChecklistItem(string noteId, int itemIndex)
    {
        NoteData note = FindNoteById(noteId);

        if (note == null)
        {
            Debug.LogWarning("Checklist toggle failed. Note not found: " + noteId);
            return;
        }

        if (itemIndex < 0 || itemIndex >= note.checklistItems.Count)
        {
            Debug.LogWarning("Invalid checklist item index: " + itemIndex);
            return;
        }

        note.checklistItems[itemIndex].isCompleted =
            !note.checklistItems[itemIndex].isCompleted;

        note.updatedAt = DateTime.Now.ToString();

        SaveNotes();
        Debug.Log(
            "Checklist item toggled for note: " + noteId +
            " | Completed: " + note.checklistItems[itemIndex].isCompleted
        );
    }

    public NoteData FindNoteById(string noteId)
    {
        return notes.Find(note => note.noteId == noteId);
    }

    public List<NoteData> GetAllNotes()
    {
        return notes;
    }

    public void SaveNotes()
    {
        NoteStorage.SaveNotes(notes);
    }

    public void ClearAllNotes()
    {
        notes.Clear();
        NoteStorage.ClearNotes();
        Debug.Log("All notes cleared.");
    }
}