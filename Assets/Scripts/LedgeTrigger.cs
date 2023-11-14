using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LedgeTrigger : MonoBehaviour
{
    [SerializeField]
    private Vector3 _handPosition, _standPosition;
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("LedgeGrabChecker"))
        {
            var player = other.transform.parent.GetComponent<Player>();

            if (player != null)
            {
                player.GrabLedge(_handPosition, this);
            }
        }
    }

    public Vector3 GetStandPosition()
    {
        return _standPosition;
    }
}
