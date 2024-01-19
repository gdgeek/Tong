using UnityEngine;
/*
public class GazeGestureManager : MonoBehaviour
{


    public delegate void Function();


    public event Function onClicked;


    public static GazeGestureManager Instance { get; private set; }

    // Represents the hologram that is currently being gazed at.
    public GameObject FocusedObject { get; private set; }

    GestureRecognizer recognizer;

    // Use this for initialization
    void Awake()
    {
        Instance = this;

        // Set up a GestureRecognizer to detect Select gestures.
        recognizer = new GestureRecognizer();
        
        recognizer.TappedEvent += (source, tapCount, ray) =>
        {
            if (onClicked != null) { 
                onClicked();
            }
            // Send an OnSelect message to the focused object and its ancestors.
            if (FocusedObject != null)
            {
                FocusedObject.SendMessageUpwards("OnSelect");

                var input = FocusedObject.GetComponent<LogoInput>();
                if (input != null)
                {
                    input.OnMouseDown();
                    input.OnMouseUpAsButton();
                }
                


            }
        };
        recognizer.StartCapturingGestures();
    }

    // Update is called once per frame
    void Update()
    {
        // Figure out which hologram is focused this frame.
        GameObject oldFocusObject = FocusedObject;

        // Do a raycast into the world based on the user's
        // head position and orientation.
        var headPosition = Camera.main.transform.position;
        var gazeDirection = Camera.main.transform.forward;

        RaycastHit hitInfo;
        if (Physics.Raycast(headPosition, gazeDirection, out hitInfo))
        {
            // If the raycast hit a hologram, use that as the focused object.
            FocusedObject = hitInfo.collider.gameObject;
        }
        else
        {
            // If the raycast did not hit a hologram, clear the focused object.
            FocusedObject = null;
        }

        // If the focused object changed this frame,
        // start detecting fresh gestures again.
        if (FocusedObject != oldFocusObject)
        {
            if (oldFocusObject != null)
            {
                var input = oldFocusObject.GetComponent<LogoInput>();
                if(input != null){
                    input.OnMouseExit();
                }
            }
            if (FocusedObject != null) {
                var input = FocusedObject.GetComponent<LogoInput>();
                if (input != null)
                {
                    input.OnMouseEnter();
                }
            }
            recognizer.CancelGestures();
            recognizer.StartCapturingGestures();
        }
    }
}*/