using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BrickPoolManager : MonoBehaviour
{
    [SerializeField] public GameObject Brick;
    [SerializeField] private int PoolSize;

    private GameObject _temp;
    private List<GameObject> _bricks = new List<GameObject>();


    private void Start()
    {
        for (int i = 0; i < PoolSize; i++)
        {
            _temp = Instantiate(Brick, transform);
            _bricks.Add(_temp);
            _temp.SetActive(false);
        }
    }
    
    public GameObject SpawnBrickFromPool(Transform transform)
    {
        if (_bricks.Count == 0)
        {
            _temp = Instantiate(Brick, transform);
            _bricks.Add(_temp);
            _temp.SetActive(false);
        }
        _temp = _bricks[0];
        _temp.SetActive(true);
        _bricks.Remove(_bricks[0]);
        _temp.transform.SetParent(transform);
        return _temp;
    }

    public void DestroyBrick(GameObject _brickToDestroy)
    {
        _bricks.Add(_brickToDestroy);
        _brickToDestroy.SetActive(false);
        _brickToDestroy.transform.SetParent(transform);
        _brickToDestroy.GetComponent<BoxCollider>().enabled = true;
        _brickToDestroy.transform.localPosition = Vector3.zero;
        _brickToDestroy.transform.localRotation = Quaternion.identity;
    }
    
}
