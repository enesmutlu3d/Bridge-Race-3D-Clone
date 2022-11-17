using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BrickPoolManager : MonoBehaviour
{
    [SerializeField] public GameObject Brick;
    [SerializeField] private int PoolSize;

    private List<GameObject> _bricks = new List<GameObject>();


    private void Start()
    {
        FillPool(PoolSize);
    }

    private void FillPool(int amount)
    {
        for (int i = 0; i < amount; i++)
        {
            GameObject temp = Instantiate(Brick, transform);
            _bricks.Add(temp);
            temp.SetActive(false);
        }
    }
    
    public GameObject SpawnBrickFromPool(Transform transform)
    {
        if (_bricks.Count == 0)
        {
            FillPool(1);
        }
        GameObject brick = _bricks[0];
        _bricks.Remove(brick);
        brick.transform.SetParent(transform);
        return brick;
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
