using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerStackManager : MonoBehaviour
{
    [HideInInspector] public List<GameObject> _stacks = new List<GameObject>();
    [SerializeField] private Transform _stackParent;

    private Vector3 heightIncrease = new Vector3(0f, 0.1f, 0f);

    public void AddStack(GameObject newStack)
    {
        foreach (var stack in _stacks)
        {
            stack.transform.position += heightIncrease;
        }
        _stacks.Add(newStack);
        StackPlacement(newStack);
    }

    private void StackPlacement(GameObject newStack)
    {
        newStack.transform.SetParent(_stackParent);
        newStack.GetComponent<BoxCollider>().enabled = false;
        newStack.transform.localPosition = Vector3.zero;
        newStack.transform.localRotation = Quaternion.identity;
    }
}
