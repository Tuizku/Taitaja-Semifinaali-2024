using UnityEngine;
using UnityEngine.Events;

// Maps events to a control

public class Control : MonoBehaviour
{
    [SerializeField] private string input; // The control to make events out of

    [SerializeField] private UnityEvent<float> onAxis; // Returns value between 0 and 1 for control
    [SerializeField] private UnityEvent onButton; // Returns when control is down
    [SerializeField] private UnityEvent onButtonDown; // Return the first frame when control is pressed
    [SerializeField] private UnityEvent onButtonUp; // Return the first frame when control is let go
    
    private void Update()
    {
        float axis = Input.GetAxis(input); // Get control

        // Fire events
        onAxis.Invoke(axis);
        if (Input.GetButton(input)) { onButton.Invoke(); }
        if (Input.GetButtonDown(input)) { onButtonDown.Invoke(); }
        if (Input.GetButtonUp(input)) { onButtonUp.Invoke(); }
    }
}
