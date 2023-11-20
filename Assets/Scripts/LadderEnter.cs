using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LadderEnter : MonoBehaviour
{
    private Vector3 _startPosition;

    [SerializeField]
    private GameObject _endPosition;

    [SerializeField]
    private GameObject _playerPosition;

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "LadderChecker")
        {
            var player = other.transform.parent.GetComponent<Player>();

            if (player != null)
            {
                player.OnLadder();
                Debug.Log("Player is on the ladder");
            }
        }
       
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.tag == "LadderChecker")
        {
            var player = other.transform.parent.GetComponent<Player>();

            if (player != null)
            {
                //_startPosition = _playerPosition.transform.position;
                //player.OffLadder(_startPosition, this);
                player.OffLadder();
            }
        }
    }

    public Vector3 TopOfPosition()
    {
        Debug.Log("TopOfPosition() Ran");
        return _endPosition.transform.position;
    }
}
