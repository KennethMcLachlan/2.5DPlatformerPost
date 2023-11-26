using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Playables;
using Cinemachine;

public class EndGame : MonoBehaviour
{
    [SerializeField]
    private PlayableDirector _director;

    [SerializeField]
    private GameObject _finalCamera;

    [SerializeField]
    private GameObject _player;

    public UIManager _uiManager;

    private void Start()
    {
        _uiManager = GameObject.Find("Canvas").GetComponent<UIManager>();
    }
    private void OnTriggerEnter(Collider other)
    {

        if (other.tag == "Player")
        {
            var player = other.GetComponent<Player>();

            if (player != null)
            {
                player.EndMovement();
            }

            _player.SetActive(false);
            _director.Play();
            _finalCamera.GetComponent<CinemachineVirtualCamera>().Priority = 15;
            StartCoroutine(DramaticPauseRoutine());
        }
    }

    IEnumerator DramaticPauseRoutine()
    {
        yield return new WaitForSeconds(35f);
        _uiManager.MissionAccomplished();
    }
}
