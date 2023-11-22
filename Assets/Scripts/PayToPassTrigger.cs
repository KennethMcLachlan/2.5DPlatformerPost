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

    private void Start()
    {
        _uiManager = GameObject.Find("Canvas").GetComponent<UIManager>();
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player")
        {
            _uiManager.UpdateEmployee();

            if (Input.GetKeyDown(KeyCode.Return) && other.GetComponent<Player>().PowerCellTally() >= _requiredPowerCells)
            {
                _ladderTrigger.SetActive(true);
                Debug.Log("ladder trigger is set to true");
            }
        }
    }
}
