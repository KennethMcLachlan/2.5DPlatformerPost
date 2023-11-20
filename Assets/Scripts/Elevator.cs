using System.Collections;
using System.Collections.Generic;
using System.Net;
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

    public bool _elevatorIsActive;

    private void Start()
    {
    }
    private void FixedUpdate()
    {
        if (_elevatorIsActive == true)
        {
            if (_switching == false)
            {
                transform.position = Vector3.MoveTowards(transform.position, _targetB.position, _speed * Time.deltaTime);
            }
            else if (_switching == true)
            {
                transform.position = Vector3.MoveTowards(transform.position, _targetA.position, _speed * Time.deltaTime);
            }

            if (transform.position == _targetB.position)
            {
                StartCoroutine(ElevatorPauseB());
            }
            else if (transform.position == _targetA.position)
            {
                StartCoroutine(ElevatorPauseA());
            }
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player")
        {
            Debug.Log("Player is parented");
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

    IEnumerator ElevatorPauseA()
    {
        yield return new WaitForSeconds(5);
        _switching = false;
    }

    IEnumerator ElevatorPauseB()
    {
        yield return new WaitForSeconds(5);
        _switching = true;
    }

    public void ActivateElevator()
    {
        _elevatorIsActive = true;
    }
}
