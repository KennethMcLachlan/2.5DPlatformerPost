using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GunTurretProjectile : MonoBehaviour
{
    [SerializeField]
    private float _missileSpeed = 8.0f;

    [SerializeField]
    private float _missileLifeTime = 10.0f;

    [SerializeField]
    private GameObject _explosion;

    private bool _setExplosion;

    //[SerializeField]
    //AudioSource _explosionSFX;

    public AudioClip explosionSFX;

    void Start()
    {
        //_explosionSFX = GetComponent<AudioSource>();
    }

    void Update()
    {
        FireMissile();
    }

    public void FireMissile()
    {
        transform.Translate(Vector3.left * _missileSpeed * Time.deltaTime);
        Invoke("Explosion", _missileLifeTime);
        Destroy(gameObject, _missileLifeTime);
    }

    private void Explosion()
    {
        Instantiate(_explosion, transform.position, Quaternion.identity);
        AudioSource.PlayClipAtPoint(explosionSFX, transform.position);
    }

    private void OnTriggerEnter(Collider other)
    {
        Player player = other.GetComponent<Player>();

        if (other.tag == "Player")
        {
            if (player != null)
            {
                Debug.Log("Player was hit");
                player.PlayerDamage();
                Explosion();
                
            }
            Destroy(gameObject);
        }
    }

}
