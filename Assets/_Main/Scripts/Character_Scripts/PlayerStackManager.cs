using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Object = System.Object;

public class PlayerStackManager : MonoBehaviour
{
    [HideInInspector] public List<GameObject> _stacks = new List<GameObject>();
    [SerializeField] private Transform _stackParent;
    private GameObject _stackToRemove;

    private Vector3 heightIncrease = new Vector3(0f, 0.1f, 0f);

    public void AddStack(GameObject newStack)
    {
        _stacks.Add(newStack);
        StackPlacement(newStack);
    }

    public void RemoveStack()
    {
        _stackToRemove = _stacks[_stacks.Count - 1];
        _stackToRemove.transform.SetParent(null);
        _stacks.Remove(_stacks[_stacks.Count - 1]);
        Destroy(_stackToRemove);
    }

    private void StackPlacement(GameObject newStack)
    {
        newStack.transform.SetParent(_stackParent);
        newStack.GetComponent<BoxCollider>().enabled = false;
        newStack.transform.localPosition = Vector3.zero + (heightIncrease * (_stacks.Count - 1));
        newStack.transform.localRotation = Quaternion.identity;
    }
}
