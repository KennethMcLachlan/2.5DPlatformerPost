using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ElevatorPanel : MonoBehaviour
{
    private Elevator _elevator;

    private Player _player;

    [SerializeField]
    private MeshRenderer _display;

    [SerializeField]
    private GameObject _text;

    private bool _inTriggerZone;

    [SerializeField]
    private int _requiredPowerCells = 5;

    private void Start()
    {
        _elevator = GameObject.Find("ElevatorContainer").GetComponent<Elevator>();
        _player = GameObject.Find("Player").GetComponent<Player>();
    }
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.E) && _inTriggerZone == true && _player.PowerCellTally() >= _requiredPowerCells)
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
