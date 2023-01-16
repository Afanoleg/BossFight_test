using UnityEngine;
using System;
using UnityEngine.UI;

public class HitTarget : MonoBehaviour
{
    public event Action<int> OnHit = (damage) => { };

    public void InvokeOnHit(int damage)
    {
        OnHit.Invoke(damage);
    }
}
