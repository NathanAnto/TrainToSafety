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
        // player.PlayerWeapon = new Rifle();
    }

    // Update is called once per frame
    void Update()
    {
        Debug.Log(player.State);
        if(player.State == PlayerState.aiming) {
            Debug.Log("AIMING");
            if(Input.GetButtonDown("Fire1")) {
                shoot();
            }
        }
    }

    private void shoot() {
        Debug.Log("SHOOTING");
        /* Enemy enemy = hitInfo.transform.GetComponent<Enemy>();
        if(enemy != null) {
            enemy.takeDamage(player.PlayerWeapon.damage);
        }
        */
    	Vector2 mouseOnScreen = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        firePoint.LookAt(mouseOnScreen);

        Instantiate(bullet, firePoint.transform.position, Quaternion.identity);
    }
}
