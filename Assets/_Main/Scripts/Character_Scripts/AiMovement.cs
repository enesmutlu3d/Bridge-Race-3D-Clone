using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.AI;
using Random = UnityEngine.Random;

public class AiMovement : MonoBehaviour
{
    [SerializeField] private NavMeshAgent _agent;
    [SerializeField] private Animator _animator;

    private PlayerStackManager _stackManager;
    private Transform _finishLine;
    private BrickType _brickType;

    private WaitForSeconds _checkDelay = new WaitForSeconds(0.1f);
    private Coroutine AnimatorStateCoroutine;
    private readonly Dictionary<Type, AiStateBase> _aiStates = new Dictionary<Type, AiStateBase>();
    private AiStateBase _currentState;
    
    public BrickSpawner BrickSpawner { get; private set; }
    public int StacksCount => _stackManager._stacks.Count;
    
    private void Start()
    {
        //Injections
        BrickSpawner = GameObject.FindWithTag("FirstFloorBrickSpawner").GetComponent<BrickSpawner>();
        _finishLine = GameObject.FindWithTag("FinishLine").transform;
        _finishLine.GetComponentInParent<FinishManager>()._players.Add(transform.gameObject);

        //Caching
        _stackManager = GetComponent<PlayerStackManager>();
        
        if (TryGetComponent(out PlayerCollision playerCollision))
            _brickType = playerCollision._brickData;
        
        //Start Routines
        AnimatorStateCoroutine = StartCoroutine("AnimatorStateCo");
        
        //Adding States
        _aiStates.Add(typeof(AiLootingState), new AiLootingState(this));
        _aiStates.Add(typeof(AiBuildingStairs), new AiBuildingStairs(this));
        
        ChangeState(typeof(AiLootingState));
    }

    private void Update()
    {
        _currentState?.OnUpdate();
        Debug.Log(_currentState);
    }

    private IEnumerator AnimatorStateCo()
    {
        while (true)
        {
            _animator.SetBool("isWalking", !(_agent.remainingDistance < 0.5f));
            yield return _checkDelay;
        }
    }

    public void FinishState()
    {
        StopCoroutine(AnimatorStateCoroutine);
        _agent.enabled = false;
    }

    public bool CheckBrickType(BrickType brickType) => _brickType == brickType;

    public void SetDestination(Vector3 destinationPos) => _agent.SetDestination(destinationPos);

    public void SpawnerSet(BrickSpawner brickSpawner) => BrickSpawner = brickSpawner;

    public float RemaningDistance()
    {
        return _agent.remainingDistance;
    }
    
    public Transform GetFinishLine() => _finishLine;

    public void ChangeState(Type type)
    {
        if (_currentState != null && _currentState.GetType().Equals(type))
            return;

        if (_aiStates.TryGetValue(type, out AiStateBase newState))
        {
            if (_currentState != null)
                _currentState.OnExit();
            
            newState.OnEnter();
            _currentState = newState;
        }
    }
}
