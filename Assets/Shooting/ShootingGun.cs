using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShootingGun : MonoBehaviour
{
    public XRExclusiveSocketObjectInteractor socketInteractor;

    public Transform FirePoint;
    //public float FireForce = 30f;
    //public Vector3 BulletTorque = new Vector3(10f, 0f, -10f);
    //public float BulletLifeTime = 3f;
    //const float DefaultBulletLifeTime = 3f;


    //bool isTriggerDown = false;

    //float bulletDistance = 100000;

    int shootCount = 0; // 발사한 총알의 갯수
    public int magazineSize = 30;

    //float shootRate = 0f;
    //public float RPM = 900f; // 발사속도
    //float shootRateValue;

    //public float GunRecoil = 100f; // 반동


    //enum FireMode
    //{
    //    SemiAuto, Burst, FullAuto
    //}

    //FireMode fireMode = FireMode.SemiAuto;


    public delegate void GunFire(int remainBullet);
    public event GunFire OnGunFire;


    //private void Start()
    //{
    //    shootRateValue = 1f / (RPM / 60f);
    //}

    //// Update is called once per frame
    //void Update()
    //{
    //    FireWithFireMode();
    //}


    public void OnTriggerDown()
    {
        if (socketInteractor.selectTarget == null) return;

        Fire();

        //isTriggerDown = true;
    }

    //public void OnTriggerUp()
    //{
    //    isTriggerDown = false;
    //}


    //void FireWithFireMode()
    //{
    //    shootRate += Time.deltaTime;

    //    // 발사 모드에 따른 총알 발사
    //    switch (fireMode)
    //    {
    //        case FireMode.SemiAuto:
    //            if (canShootCount == 0)
    //            {
    //                if (shootRate >= shootRateValue)
    //                {
    //                    shootRate = 0f;

    //                    Fire();
    //                    canShootCount = -1;
    //                }
    //            }
    //            break;
    //        case FireMode.Burst:
    //            if (canShootCount >= 0)
    //            {
    //                if (shootRate >= shootRateValue)
    //                {
    //                    if (canShootCount == 0)
    //                        shootRate = 0f;
    //                    else
    //                        shootRate -= shootRateValue;

    //                    Fire();
    //                    if (++canShootCount == 3) canShootCount = -1;
    //                }
    //            }
    //            break;
    //        case FireMode.FullAuto:
    //            if (isTriggerDown)
    //            {
    //                if (shootRate >= shootRateValue)
    //                {
    //                    if (canShootCount == 0)
    //                        shootRate = 0f;
    //                    else
    //                        shootRate -= shootRateValue;

    //                    Fire();
    //                }
    //            }
    //            break;
    //    }
    //}

    void Fire()
    {
        if (shootCount >= magazineSize) return;

        //GameObject bullet = Instantiate(Bullet, FirePoint.position, FirePoint.rotation);
        //Rigidbody rigid = bullet.GetComponent<Rigidbody>();
        //rigid.velocity = FirePoint.transform.forward * FireForce;
        //rigid.AddTorque(BulletTorque);
        //Destroy(bullet, (BulletLifeTime <= 0f) ? DefaultBulletLifeTime : BulletLifeTime);


        // Raycast
        Ray ray = new Ray(FirePoint.position, FirePoint.forward);
        RaycastHit hit = new RaycastHit();
        Physics.Raycast(ray, out hit, float.MaxValue, 1 >> LayerMask.NameToLayer("ShootingTarget"));

        if (hit.collider)
        {
            hit.transform.GetComponent<ShootingTarget>().TargetHit();
        }


        shootCount++;

        OnGunFire(magazineSize - shootCount);
    }

    //public void ChangeFireMode()
    //{
    //    // 발사모드 변경
    //    switch (fireMode)
    //    {
    //        case FireMode.SemiAuto:
    //            fireMode = FireMode.Burst;
    //            break;
    //        case FireMode.Burst:
    //            fireMode = FireMode.FullAuto;
    //            canShootCount = 0;
    //            break;
    //        case FireMode.FullAuto:
    //            fireMode = FireMode.SemiAuto;
    //            canShootCount = -1;
    //            break;
    //    }
    //}

    public void Reload()
    {
        shootCount = 0;

        OnGunFire(magazineSize - shootCount);
    }
}