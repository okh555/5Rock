using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShootingGun : MonoBehaviour
{
    public XRExclusiveSocketObjectInteractor socketInteractor;

    public LayerMask targetLayerMask;

    public Transform FirePoint;
    int shootCount = 0; // 발사한 총알의 갯수
    public int magazineSize = 30;


    public delegate void GunFire(int remainBullet);
    public event GunFire OnGunFire;


    public void OnTriggerDown()
    {
        if (socketInteractor.selectTarget == null) return;

        Fire();

    }

    public void Fire()
    {
        if (shootCount >= magazineSize) return;


        // Raycast
        Ray ray = new Ray(FirePoint.position, FirePoint.forward);
        Debug.DrawRay(ray.origin, ray.direction * 1000f, Color.red, 3f);

        Physics.Raycast(ray, out RaycastHit hit, float.MaxValue, targetLayerMask);


        if (hit.collider)
        {
            Debug.Log(hit.transform.name);
            hit.transform.GetComponent<ShootingTarget>().TargetHit();
        }


        shootCount++;

        OnGunFire.Invoke(magazineSize - shootCount);
    }


    public void Reload()
    {
        shootCount = 0;

        OnGunFire.Invoke(magazineSize - shootCount);
    }
}