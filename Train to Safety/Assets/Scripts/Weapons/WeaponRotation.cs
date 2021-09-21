using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponRotation : MonoBehaviour
{
    private Player player;
    private Transform weaponTransform;
    
    private void Start()
    {
        player = Player.getPlayerInstance();
        player.FacingRight = true;
        weaponTransform = transform.GetChild(0).transform;
    }

    // Update is called once per frame
    void Update()
    {
        Vector3 mousePos = Utils.GetMouseWorldPosition();
        Vector3 aimDirection = (mousePos - transform.position).normalized;
        float angle = Mathf.Atan2(aimDirection.y, aimDirection.x) * Mathf.Rad2Deg;

        transform.eulerAngles = new Vector3(0,0,angle);
        
        Vector3 localScale = transform.GetChild(0).localScale;
        
        if (angle > 90 || angle < -90) 
            localScale.y = -localScale.x;
        else 
            localScale.y = localScale.x;

        weaponTransform.localScale = localScale;
    }
}
