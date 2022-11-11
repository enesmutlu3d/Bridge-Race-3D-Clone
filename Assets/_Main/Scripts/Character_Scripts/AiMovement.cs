using System.Collections;
using UnityEngine;
using UnityEngine.AI;
using Random = UnityEngine.Random;

public class AiMovement : MonoBehaviour
{
    [SerializeField] private NavMeshAgent _agent;
    [HideInInspector] public BrickSpawner _brickSpawner;
    [SerializeField] private Animator _animator;

    private PlayerStackManager _stackManager;
    private PlayerCollision _playerCollision;
    private Transform _finishLine;
    
    private int _random;
    private int _random2;
    private int _checkLimiter;
    private Vector3 _destinationPos;
    private Transform _temp;
    private WaitForSeconds _checkDelay;
    private Coroutine MoveCoroutine;
    private Coroutine AnimatorStateCoroutine;
    
    private void Start()
    {
        //Injections
        _brickSpawner = GameObject.FindWithTag("FirstFloorBrickSpawner").GetComponent<BrickSpawner>();
        _finishLine = GameObject.FindWithTag("FinishLine").transform;
        _finishLine.GetComponentInParent<FinishManager>()._players.Add(transform.gameObject);

        //Caching
        _checkDelay = new WaitForSeconds(0.2f);
        _stackManager = GetComponent<PlayerStackManager>();
        _playerCollision = GetComponent<PlayerCollision>();
        _destinationPos = transform.position;
        
        //Start Routines
        MoveCoroutine = StartCoroutine("AiMoveCo");
        AnimatorStateCoroutine = StartCoroutine("AnimatorStateCo");
    }

    public void BrickCollector(BrickSpawner brickSpawner)
    {
        _brickSpawner = brickSpawner;
        DestinationPick();
    }

    private void DestinationPick()
    {
        do
        {
            _random = Random.Range(0, _brickSpawner.transform.childCount - 1);
            if (_brickSpawner.transform.childCount < 1)
                return;
            _temp = _brickSpawner.transform.GetChild(_random);
            _checkLimiter++;
        } while (_temp.GetComponent<CollectibleBrick>()._brickType != _playerCollision._brickData && _checkLimiter < 15);

        _checkLimiter = 0;
        _destinationPos = _temp.transform.position;
    }

    private IEnumerator AiMoveCo()
    {
        while (true)
        {
            _random2 = Random.Range(2, 21);

            do
            {
                _agent.SetDestination(_destinationPos);
                yield return new WaitForSeconds(Random.Range(1.25f,2.25f));
                DestinationPick();
            } while (_stackManager._stacks.Count < _random2);
        
            _agent.SetDestination(_finishLine.position);
            
            do
            {
                yield return _checkDelay;
            } while (_stackManager._stacks.Count > 0);
        }
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
        StopCoroutine(MoveCoroutine);
        StopCoroutine(AnimatorStateCoroutine);
        _agent.enabled = false;
    }
    
}
