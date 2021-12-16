using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InputManager : MonoBehaviour
{
    public event Action OnShoot;
    public event Action OnReload;
    public event Action OnSwitch;

    // Update is called once per frame
    void Update() {
        if (Input.GetButtonDown("Fire1")) {
            OnShoot?.Invoke();
        }
        if (Input.GetKeyDown(KeyCode.R)) {
            OnReload?.Invoke();
        }
        if (Input.GetKeyDown(KeyCode.Q)) {
            OnSwitch?.Invoke();
        }
    }
}
