using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponRotation : MonoBehaviour
{
    // Update is called once per frame
    void Update()
    {
        var tr = transform;
        var mousePos = Utils.GetMouseWorldPosition();
        var aimDirection = (mousePos - tr.position).normalized;
        var angle = Mathf.Atan2(aimDirection.y, aimDirection.x) * Mathf.Rad2Deg;

        tr.eulerAngles = new Vector3(0,0,angle);
        
        var localScale = tr.localScale;
        
        if (angle > 90 || angle < -90) 
            localScale.y = -localScale.x;
        else 
            localScale.y = localScale.x;

        tr.localScale = localScale;
    }
}
