using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BrickPoolManager : MonoBehaviour
{
    [SerializeField] public GameObject Brick;
    [SerializeField] private int PoolSize;
    

    private void Start()
    {
        for (int i = 0; i < PoolSize; i++)
        {
            Instantiate(Brick, transform).SetActive(false);
        }
    }
    
}

public static class BrickPool
{
    public static void SpawnBrickFromPool()
    {
        
    }

    public static void DestroyBrick()
    {
        
    }
}
