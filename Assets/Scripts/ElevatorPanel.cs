using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ElevatorPanel : MonoBehaviour
{
    private Elevator _elevator;

    [SerializeField]
    private MeshRenderer _display;

    [SerializeField]
    private GameObject _text;

    private bool _inTriggerZone;

    private void Start()
    {
        _elevator = GameObject.Find("ElevatorContainer").GetComponent<Elevator>();
    }
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.E) && _inTriggerZone == true)
        {
            _display.material.color = Color.green;
            _text.SetActive(true);
            StartCoroutine(ElevatorPause());
        }
    }
    //private void OnTriggerStay(Collider other)
    //{
    //    if (other.tag == "Player")
    //    {
    //        _inTriggerZone = true;
    //        Debug.Log("In Trigger Zone = true");
    //    }
    //    else _inTriggerZone = false; Debug.Log("Is not in trigger Zone");
    //}

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player")
        {
            _inTriggerZone = true;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.tag == "Player")
        {
            _inTriggerZone = false;
        }
    }

    IEnumerator ElevatorPause()
    {
        yield return new WaitForSeconds(3);
        _elevator.ActivateElevator();
    }

}
