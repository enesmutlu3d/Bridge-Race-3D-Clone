using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.AI;
using Random = UnityEngine.Random;

public class AiMovement : MonoBehaviour
{
    private readonly int walkingHash = Animator.StringToHash("isWalking");
    
    [SerializeField] private NavMeshAgent _agent;
    [SerializeField] private Animator _animator;
    [SerializeField] private bool _showLog;

    private PlayerStackManager _stackManager;
    private Transform _finishLine;

    private readonly Dictionary<Type, AiStateBase> _aiStates = new Dictionary<Type, AiStateBase>();
    private AiStateBase _currentState;
    
    public BrickSpawner BrickSpawner { get; private set; }
    public BrickType BrickType { get; private set; }
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
            BrickType = playerCollision._brickData;
        
        //Adding States
        _aiStates.Add(typeof(AiIdleState), new AiIdleState(this));
        _aiStates.Add(typeof(AiLootingState), new AiLootingState(this));
        _aiStates.Add(typeof(AiBuildingStairs), new AiBuildingStairs(this));
        
        ChangeState(typeof(AiIdleState));
    }

    private void Update()
    {
        _currentState?.OnUpdate();
        if (_showLog)
            Debug.Log(_currentState + " - " + gameObject.name);
    }

    public void SetWalking(bool state)
    {
        _animator.SetBool(walkingHash, state);
    }

    public void ClearDestination() => _agent.ResetPath();

    public void FinishState()
    {
        _agent.enabled = false;
    }

    public void SetDestination(Vector3 destinationPos) => _agent.SetDestination(destinationPos);

    public void SpawnerSet(BrickSpawner brickSpawner) => BrickSpawner = brickSpawner;

    public float RemaningDistance() => _agent.remainingDistance;

    public bool CheckPathComplete() => _agent.pathStatus == NavMeshPathStatus.PathComplete;
    
    public Transform GetFinishLine() => _finishLine;

    public void ChangeState(Type type)
    {
        if (_currentState != null && _currentState.GetType().Equals(type))
            return;

        if (_aiStates.TryGetValue(type, out AiStateBase newState))
        {
            if (_currentState != null)
                _currentState.OnExit();
            
            _currentState = newState;
            newState.OnEnter();
        }
    }
}
