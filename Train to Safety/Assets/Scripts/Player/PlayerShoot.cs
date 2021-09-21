using System;
using UnityEngine;

public class PlayerShoot : MonoBehaviour
{
    [SerializeField] private Transform firePoint;
    private Player player;
    private ObjectPooler objPooler;
    private WeaponHandler weaponHandler;

    private event EventHandler OnShoot;

    // Start is called before the first frame update
    private void Awake()
    {
        player = Player.getPlayerInstance();
        weaponHandler = WeaponHandler.instance;
        player.PlayerWeapon = weaponHandler.getCurrentWeapon();
        OnShoot += attack;
        objPooler = ObjectPooler.instance;
    }

    private void Update()
    {
        if(objPooler == null)   objPooler = ObjectPooler.instance;
        // On left mouse click
        if(Input.GetButtonDown("Fire1"))
        {
            OnShoot?.Invoke(this, EventArgs.Empty);
        }
    }

    private void attack(object sender, EventArgs e)
    {
        Debug.Log("shoot");
        var firePointPos = firePoint.position;
        
        Vector3 mouseOnScreen = Utils.GetMouseWorldPosition();
        Vector3 shootDir = (mouseOnScreen - firePointPos).normalized;
        

        // Reduce ammo size
        player.PlayerWeapon.changeValue();

        BulletRaycast.Shoot(firePointPos, shootDir);
        
        Effect();
    }
    
    private void Effect()
    {
        objPooler.SpawnFromPool("bullets", firePoint.position, firePoint.rotation);
    }
}
