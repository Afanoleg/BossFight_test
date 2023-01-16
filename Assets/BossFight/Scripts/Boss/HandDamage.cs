using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HandDamage : MonoBehaviour
{
    public int handAttackDamage = 15;
    private void OnCollisionEnter(Collision collision)
    {
        Debug.LogWarning(collision.collider.name);

        if (collision.gameObject.CompareTag("Player"))
        {
            collision.gameObject.GetComponent<PlayerHealth>().TakingDamage2(handAttackDamage);
        }
    }
}
