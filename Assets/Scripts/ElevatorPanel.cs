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

    [SerializeField]
    private GameObject _panelSFX;

    [SerializeField]
    private GameObject _elevatorSFX;

    private void Start()
    {
        _elevator = GameObject.Find("ElevatorContainer").GetComponent<Elevator>();
        _player = GameObject.Find("Player").GetComponent<Player>();
    }
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Return) && _inTriggerZone == true && _player.PowerCellTally() >= _requiredPowerCells)
        {
            _elevatorSFX.SetActive(true);
            _panelSFX.SetActive(true);
            _display.material.color = Color.green;
            _text.SetActive(true);
            StartCoroutine(ElevatorPause());
        }
    }

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
