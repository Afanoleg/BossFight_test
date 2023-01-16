using UnityEngine;

public class Gun : MonoBehaviour
{
    public int damage = 10;
    public float range = 100f;
    public float fireRate = 15f;
    public float impactForce = 30f;

    public Camera fpsCam;
    public ParticleSystem muzzleFX;
    public GameObject impactFX;


    private float nextTimeToFire = 0f;
    void Update()
    {
        if(Input.GetButton("Fire1") && Time.time >= nextTimeToFire)
        {
            nextTimeToFire = Time.time + 1f / fireRate;
            Shoot();
        }
    }

    void Shoot()
    {
        muzzleFX.Play();

        RaycastHit hit;
        if (Physics.Raycast(fpsCam.transform.position, fpsCam.transform.forward, out hit, range))
        {
           if (hit.transform.TryGetComponent<HitTarget>(out var hitTarget))
           {
                hitTarget.InvokeOnHit(damage);
           }

            Debug.Log(hit.transform.name);

            GameObject impactGO = Instantiate(impactFX, hit.point, Quaternion.LookRotation(hit.normal));
            Destroy(impactGO, 2f);
        }
    }
}
