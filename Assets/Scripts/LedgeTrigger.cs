using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LedgeTrigger : MonoBehaviour
{

    //private Animator _anim;
    //private void OnTriggerEnter(Collider other)
    //{

    //    if (other.tag == "LedgeGrabChecker")
    //    {
    //        Player player = GetComponent<Player>();
    //        _anim = player.GetComponentInChildren<Animator>();
    //        _anim.SetTrigger("LedgeGrab");
    //        Debug.Log("Player triggered the thingy");
    //        player._gravity = 0;
    //    }
    //}

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("LedgeGrabChecker"))
        {
            var player = other.transform.parent.GetComponent<Player>();

            if (player != null)
            {
                player.GrabLedge();
            }

        }
    }
}
