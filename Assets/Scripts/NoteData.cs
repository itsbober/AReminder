using System;
using System.Collections.Generic;

[Serializable]
public class NoteData
{
    public string noteId;
    public string title;
    public string content;
    public bool isVisible;
    public List<ChecklistItem> checklistItems;
    public string createdAt;
    public string updatedAt;

    public NoteData(string title, string content)
    {
        noteId = Guid.NewGuid().ToString();
        this.title = title;
        this.content = content;
        isVisible = true;
        checklistItems = new List<ChecklistItem>();
        createdAt = DateTime.Now.ToString();
        updatedAt = DateTime.Now.ToString();
    }
}