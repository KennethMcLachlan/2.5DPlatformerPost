using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Health : MonoBehaviour
{
    [SerializeField]
    public AudioClip healthAudio;
    private void Start()
    {
        
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player")
        {
            Player player = other.GetComponent<Player>();

            if (player != null)
            {
                player.Heal();
                AudioSource.PlayClipAtPoint(healthAudio, transform.position);
            }
            Destroy(this.gameObject);
        }
    }
}
