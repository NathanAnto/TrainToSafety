using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InputManager : MonoBehaviour
{
    public Vector2 Movement { get; private set; }
    
    // Start is called before the first frame update
    void Awake() {
        Movement = Vector2.zero;
    }

    // Update is called once per frame
    void Update() {
        Movement = new Vector2 {
            x = Input.GetAxis("Horizontal"),
            y = Input.GetAxis("Vertical")
        }.normalized;
    }
}
