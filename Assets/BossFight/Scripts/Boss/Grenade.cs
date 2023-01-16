using UnityEngine;

public class Grenade : MonoBehaviour
{
    public GameObject impactEffect;
    public int radius = 3;
    public int grenadeDamage = 10;
    private void OnCollisionEnter(Collision collision)
    {
        GameObject impact  = Instantiate(impactEffect, transform.position, Quaternion.identity);
        Destroy(impact, 2);
        Collider[] colliders = Physics.OverlapSphere(transform.position, radius);
        foreach(Collider nearbyObject in colliders)
        {
            if (nearbyObject.tag == "Player")
            {
                nearbyObject.GetComponent<PlayerHealth>().TakingDamage2(grenadeDamage);
            }
            if (nearbyObject.tag == "Walls")
            {
                Destroy(gameObject);
            }
            Destroy(gameObject, 1f);
        }
        this.enabled = false;
    }
}
