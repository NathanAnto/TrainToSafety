using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerShoot : MonoBehaviour
{
    private Player player;
    public Transform firePoint;
    public GameObject bullet;

    // Start is called before the first frame update
    void Start()
    {
        player = Player.getPlayerInstance();
    }

    // Update is called once per frame
    void Update()
    {
        if(player.State == PlayerState.aiming) {
            if(Input.GetButtonDown("Fire1")) {
                attack();
            }
        }
    }

    private void attack() {
    	Vector2 mouseOnScreen = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        firePoint.LookAt(mouseOnScreen);

        // Reduce ammo size
        player.PlayerWeapon.changeValue();

        Instantiate(bullet, firePoint.transform.position, gameObject.transform.rotation);
    }
}
