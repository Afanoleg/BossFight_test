using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimatorTrigger : MonoBehaviour
{
    public GameObject levelAnimator;
    public GameObject startLineAnimator;
    public GameObject bossAnimator;
    public GameObject platforms;

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player")
        {
            levelAnimator.GetComponent<Animator>().SetTrigger("Started");
            startLineAnimator.GetComponent<Animator>().SetTrigger("Started");
            bossAnimator.GetComponent<Animator>().SetTrigger("Started");
            platforms.GetComponent<Animator>().SetTrigger("Started");
        }
    }
}
