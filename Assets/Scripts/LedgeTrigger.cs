using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LedgeTrigger : MonoBehaviour
{
    private Vector3 _handPosition;

    [SerializeField]
    private GameObject _standingPosition;

    [SerializeField]
    private GameObject _handHoldPosition;
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("LedgeGrabChecker"))
        {
            var player = other.transform.parent.GetComponent<Player>();

            if (player != null)
            {
                _handPosition = _handHoldPosition.transform.position;
                player.GrabLedge(_handPosition, this);
            }
        }
    }

    public Vector3 GetStandPosition()
    {
        return _standingPosition.transform.position;
    }
}
