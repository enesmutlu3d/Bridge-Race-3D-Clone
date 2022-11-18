using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.AI;
using Random = UnityEngine.Random;

public class AiStateMachine : MonoBehaviour
{
    private readonly int walkingHash = Animator.StringToHash("isWalking");
    private readonly int winnerHash = Animator.StringToHash("EndAnimationWin");
    private readonly int loserHash = Animator.StringToHash("EndAnimationLose");
    private readonly int hitHash = Animator.StringToHash("HitAnimation");
    
    [SerializeField] private NavMeshAgent _agent;
    [SerializeField] private Animator _animator;

    private PlayerStackManager _stackManager;
    private Transform _finishLine;

    private readonly Dictionary<Type, AiStateBase> _aiStates = new Dictionary<Type, AiStateBase>();
    private AiStateBase _currentState;
    
    public BrickSpawner BrickSpawner { get; private set; }
    public BrickType BrickType { get; private set; }
    public int StacksCount => _stackManager._stacks.Count;

    public event Action OnHit;
    
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
        _aiStates.Add(typeof(AiWinnerState), new AiWinnerState(this));
        _aiStates.Add(typeof(AiLoserState), new AiLoserState(this));
        _aiStates.Add(typeof(AiHitState), new AiHitState(this));
        _aiStates.Add(typeof(AiIdleState), new AiIdleState(this));
        _aiStates.Add(typeof(AiLootingState), new AiLootingState(this));
        _aiStates.Add(typeof(AiBuildingStairState), new AiBuildingStairState(this));
        
        ChangeState(typeof(AiIdleState));
    }

    private void Update()
    {
        _currentState?.OnUpdate();
    }

    public void SetWalking(bool state) => _animator.SetBool(walkingHash, state);

    public void SetWinner() => _animator.SetTrigger(winnerHash);
    
    public void SetLoser() => _animator.SetTrigger(loserHash);

    public void SetHit() => _animator.SetTrigger(hitHash);

    public void ClearDestination() => _agent.ResetPath();

    public void SetAgent(bool state) => _agent.enabled = state;

    public void SetDestination(Vector3 destinationPos) => _agent.SetDestination(destinationPos);

    public void SetSpawner(BrickSpawner brickSpawner) => BrickSpawner = brickSpawner;

    public float RemaningDistance() => _agent.remainingDistance;

    public bool CheckPathComplete() => _agent.pathStatus == NavMeshPathStatus.PathComplete;
    
    public Transform GetFinishLine() => _finishLine;
    
    public void TriggerHit() => OnHit?.Invoke();

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
