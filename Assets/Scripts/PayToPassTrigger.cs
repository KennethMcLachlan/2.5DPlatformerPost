using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PayToPassTrigger : MonoBehaviour
{
    [SerializeField]
    private int _requiredPowerCells = 10;

    private UIManager _uiManager;

    [SerializeField]
    private GameObject _ladderTrigger;

    private Player _player;

    private bool _canPass;

    [SerializeField]
    private GameObject _employeeConvo;

    [SerializeField]
    private GameObject _employeeThanks;

    private void Start()
    {
        _uiManager = GameObject.Find("Canvas").GetComponent<UIManager>();
        _player = GameObject.Find("Player").GetComponent<Player>();
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Return) && _player.PowerCellTally() >= _requiredPowerCells && _canPass == true)
        {
            _ladderTrigger.SetActive(true);
            _employeeConvo.SetActive(false);
            _employeeThanks.SetActive(true);
            Debug.Log("ladder trigger is set to true");
        }
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player")
        {
            _uiManager.UpdateEmployee();
            _canPass = true;
            Debug.Log("Player has entered the Pay to Pass trigger");
        }
    }

    public void OnTriggerExit(Collider other)
    {
        _uiManager.EndEmployeeConvo();
    }
}
