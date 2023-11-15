using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting.ReorderableList;
using UnityEngine;
using UnityEngine.UIElements;

public class Elevator : MonoBehaviour
{
    [SerializeField]
    private Transform _targetA, _targetB;

    [SerializeField]
    private float _speed = 3.0f;

    [SerializeField]
    private bool _switching;

    private void FixedUpdate()
    {
        if (_switching == false)
        {
            transform.position = Vector3.MoveTowards(transform.position, _targetB.position, _speed * Time.deltaTime);

            Debug.Log("Moving toward B");
        }
        else if (_switching == true)
        {
            transform.position = Vector3.MoveTowards(transform.position, _targetA.position, _speed * Time.deltaTime);

            Debug.Log("Moving toward A");
        }

        if (transform.position ==  _targetB.position)
        {
            Debug.Log("Switching is true");
            //StartCoroutine(ElevatorPause());
            _switching = true;
        }
        else if (transform.position == _targetA.position)
        {
            Debug.Log("Switching is false");
            //StartCoroutine(ElevatorPause());
            _switching = false;
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player")
        {
            other.transform.parent = this.transform;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.tag == "Player")
        {
            other.transform.parent = null;
        }
    }

    IEnumerator ElevatorPause()
    {
        yield return new WaitForSeconds(5);
    }
}
