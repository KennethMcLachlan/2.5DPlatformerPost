using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GunTurretProjectile : MonoBehaviour
{
    [SerializeField]
    private float _missileSpeed = 8.0f;

    [SerializeField]
    private float _missileLifeTime = 10.0f;

    void Start()
    {
    }

    void Update()
    {
        FireMissile();
    }

    public void FireMissile()
    {
        transform.Translate(Vector3.left * _missileSpeed * Time.deltaTime);
        Destroy(gameObject, _missileLifeTime);
        Debug.Log("MissileFired");
    }
    
}
