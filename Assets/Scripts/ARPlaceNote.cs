using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.ARFoundation;
using UnityEngine.XR.ARSubsystems;

public class ARPlaceNote : MonoBehaviour
{
    [SerializeField] private GameObject notePrefab;

    private ARRaycastManager raycastManager;
    private readonly List<ARRaycastHit> hits = new List<ARRaycastHit>();

    private void Awake()
    {
        raycastManager = GetComponent<ARRaycastManager>();
    }

    private void Update()
    {
        if (Input.touchCount == 0)
            return;

        Touch touch = Input.GetTouch(0);

        if (touch.phase != TouchPhase.Began)
            return;

        if (raycastManager.Raycast(touch.position, hits, TrackableType.PlaneWithinPolygon))
        {
            Pose pose = hits[0].pose;

            GameObject noteObject = Instantiate(notePrefab, pose.position, pose.rotation);

            NoteData newNote = NoteManager.Instance.AddNote(
                "New Note",
                "Tap to edit this note"
            );

            NoteObject noteComponent = noteObject.GetComponent<NoteObject>();

            if (noteComponent != null)
            {
                noteComponent.Initialize(newNote.noteId);
            }
            else
            {
                Debug.LogWarning("NoteObject script is missing from NotePrefab.");
            }
        }
    }
}