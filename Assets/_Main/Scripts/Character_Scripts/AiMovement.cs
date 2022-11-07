using System.Collections;
using UnityEngine;
using UnityEngine.AI;
using Random = UnityEngine.Random;

public class AiMovement : MonoBehaviour
{
    [SerializeField] private NavMeshAgent _agent;
    [SerializeField] public BrickSpawner _brickSpawner;
    [SerializeField] private Animator _animator;
    [SerializeField] private Transform _finishLine;

    private PlayerStackManager _stackManager;
    private PlayerCollision _playerCollision;
    
    private int _random;
    private int _random2;
    private int _checkLimiter;
    private Vector3 _destinationPos;
    private Transform _temp;
    private WaitForSeconds _checkDelay;
    
    private void Start()
    {
        _checkDelay = new WaitForSeconds(0.2f);
        _stackManager = GetComponent<PlayerStackManager>();
        _playerCollision = GetComponent<PlayerCollision>();
        _destinationPos = transform.position;
        StartCoroutine("AiMoveCo");
    }
    
    private void FixedUpdate()
    {
        _animator.SetBool("isWalking", !(_agent.remainingDistance < 1f));
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
        _random2 = Random.Range(5, 21);
        
        while (true)
        {
            _agent.SetDestination(_destinationPos);
            yield return new WaitForSeconds(Random.Range(1.25f, 2.5f));
            if (_stackManager._stacks.Count > _random2)
            {
                StartCoroutine("GoStairsCo");
                break;
            }
            else
            {
                DestinationPick();
            }
        }
    }

    private IEnumerator GoStairsCo()
    {
        _agent.SetDestination(_finishLine.position);
        
        while (true)
        {
            if (_stackManager._stacks.Count <= 0)
            {
                StartCoroutine("AiMoveCo");
                break;
            }
            
            yield return _checkDelay;
        }
    }
    
}
