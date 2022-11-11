using System;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using Random = UnityEngine.Random;

public class PlayerStackManager : MonoBehaviour
{
    [HideInInspector] public List<GameObject> _stacks = new List<GameObject>();
    [SerializeField] private Transform _stackParent;
    private GameObject _stackToRemove;
    private BrickPoolManager _poolManager;

    private Vector3 heightIncrease = new Vector3(0f, 0.1f, 0f);
    private Vector3 _spreadForce;
    private Rigidbody brickRb;

    private void Start()
    {
        _poolManager = GameObject.FindWithTag("BrickPool").GetComponent<BrickPoolManager>();
    }

    public void AddStack(GameObject newStack)
    {
        _stacks.Add(newStack);
        StackPlacement(newStack);
    }

    public void RemoveStack(Vector3 _dropLocation, bool isDestroyed)
    {
        _stackToRemove = _stacks[_stacks.Count - 1];
        _stackToRemove.transform.SetParent(null);
        _stacks.Remove(_stacks[_stacks.Count - 1]);
        
        if (isDestroyed)
            _poolManager.DestroyBrick(_stackToRemove); 
        
        //Animated Stair Brick Placing
        /*_stackToRemove.transform.DOJump(_dropLocation, 2,1,0.5f).
            OnComplete(()=> 
            { 
                if (isDestroyed)
                    _poolManager.DestroyBrick(_stackToRemove); 
            });*/
    }

    private void StackPlacement(GameObject newStack)
    {
        newStack.transform.SetParent(_stackParent);
        newStack.GetComponent<BoxCollider>().enabled = false;
        newStack.GetComponent<CollectibleBrick>()._brickSpawner = null;
        newStack.transform.DOLocalJump(Vector3.zero + (heightIncrease * (_stacks.Count - 1)), 1.5f, 1,0.5f);
        //newStack.transform.localPosition = Vector3.zero + (heightIncrease * (_stacks.Count - 1));
        newStack.transform.localRotation = Quaternion.identity;
    }

    
    // Spread Bricks
    /*public void StackSpread()
    {
        _spreadForce = new Vector3(Random.Range(0.5f, 2f), Random.Range(0.5f, 2f), Random.Range(0.5f, 2f));
        
        foreach (GameObject stack in _stacks)
        {
            stack.transform.SetParent(null);
            _stacks.Remove(stack);
        }
    }*/
    
}
