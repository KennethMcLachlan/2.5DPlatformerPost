using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GunTurret : MonoBehaviour
{
    [SerializeField]
    private GameObject _missile;

    [SerializeField]
    private GameObject _smoke;

    [SerializeField]
    private float _canFire = -1f;

    [SerializeField]
    private float _fireRate = 3.0f;

    //public GunTurretProjectile _missile;
    void Start()
    {
        //_missile = GetComponent<GunTurretProjectile>();
    }

    void Update()
    {
        if (Time.time > _canFire)
        {
            _canFire = Time.time + _fireRate;
            SpawnMissile();
        }
    }

    private void SpawnMissile()
    {
        Instantiate(_missile, transform.position + new Vector3(-2.0f, 1.34f, 0.0f), Quaternion.identity);
        Instantiate(_smoke, transform.position + new Vector3 (-1.45f, 1.297f, 0.0f), Quaternion.Euler (0, -90, 0));
    }
}
