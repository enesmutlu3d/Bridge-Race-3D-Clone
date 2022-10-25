using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerGravityCheck : MonoBehaviour
{
    [SerializeField] private Transform rayPos;
    [SerializeField] private Rigidbody _rigidbody;
    private WaitForSeconds _checkerPeriod;
    private RaycastHit _hit;
    
    private void Start()
    {
        _checkerPeriod = new WaitForSeconds(0.1f);
        StartCoroutine("GravityChecker");
    }

    private IEnumerator GravityChecker()
    {
        while (true)
        {
            if (Physics.Raycast(rayPos.transform.position, rayPos.transform.forward, out _hit, 0.1f))
            {
                _rigidbody.useGravity = false;
            }
            else
            {
                _rigidbody.useGravity = true;
            }
            yield return _checkerPeriod;
        }
    }
}
