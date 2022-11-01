using System;
using System.Collections;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.AI;
using Random = UnityEngine.Random;

public class AiMovement : MonoBehaviour
{
    [SerializeField] private NavMeshAgent _agent;
    [SerializeField] private BrickSpawner _brickSpawner;
    [SerializeField] private Animator _animator;
    [SerializeField] private Transform _finishLine;

    private PlayerStackManager _stackManager;
    
    private int _random;
    private Vector3 _destinationPos;
    private WaitForSeconds _checkDelay;
    
    private void Start()
    {
        _checkDelay = new WaitForSeconds(0.2f);
        _stackManager = GetComponent<PlayerStackManager>();
        _destinationPos = transform.position;
        StartCoroutine(nameof(AiMoveCo));
    }
    
    private void FixedUpdate()
    {
        _animator.SetBool("isWalking", !(_agent.remainingDistance < 1f));
        if (_stackManager._stacks.Count > Random.Range(3, 21))
            StartCoroutine(nameof(GoStairsCo));
    }

    public void BrickCollector(BrickSpawner brickSpawner)
    {
        _brickSpawner = brickSpawner;
        DestinationPick();
    }

    private void DestinationPick()
    {
        _random = Random.Range(0, _brickSpawner.transform.childCount - 1);
        _destinationPos = _brickSpawner.transform.GetChild(_random).transform.position;
    }

    private IEnumerator AiMoveCo()
    {
        while (true)
        {
            _agent.SetDestination(_destinationPos);
            yield return new WaitForSeconds(Random.Range(1.25f, 3.5f));
            DestinationPick();
        }
    }

    private IEnumerator GoStairsCo()
    {
        StopCoroutine(nameof(AiMoveCo));
        _agent.SetDestination(_finishLine.position);
        
        while (true)
        {
            if (_stackManager._stacks.Count <= 0)
            {
                StopCoroutine(nameof(GoStairsCo));
                StartCoroutine(nameof(AiMoveCo));
            }

            yield return _checkDelay;
        }
    }
    
}
