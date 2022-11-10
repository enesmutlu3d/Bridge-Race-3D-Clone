using System;
using System.Collections;
using System.Collections.Generic;
using Cinemachine;
using UnityEngine;
using UnityEngine.AI;

public class FinishManager : MonoBehaviour, IInteractible
{
    [SerializeField] private Transform[] _ladderLocations;
    [SerializeField] private CinemachineVirtualCamera _cam;
    [SerializeField] private ParticleSystem _confetti1;
    [SerializeField] private ParticleSystem _confetti2;
    
    public List<GameObject> _players = new List<GameObject>();
    private List<GameObject> _playersInOrder = new List<GameObject>();
    
    private GameObject _temp;


    public void OnInteract(BrickType brickType, Transform interactor)
    {
        transform.GetComponent<BoxCollider>().enabled = false;
        ComparePlayers();
        LevelEnd();
    }

    private void ComparePlayers()
    {
        do
        {
            _temp = _players[0];
            foreach (var player in _players)
            {
                if (_temp.transform.position.z < player.transform.position.z)
                {
                    _temp = player;
                }
            }
            _playersInOrder.Add(_temp);
            _players.Remove(_temp);
        } while (_players.Count != 0);
    }

    private void LevelEnd()
    {
        for (int i = 0; i < _playersInOrder.Count; i++)
        {
            //Player Movement Stop
            if (_playersInOrder[i].TryGetComponent<AiMovement>(out AiMovement aiMovement))
            {
                aiMovement.FinishState();
            }
            if (_playersInOrder[i].TryGetComponent<PlayerMovement>(out PlayerMovement playerMovement))
            {
                playerMovement.enabled = false;
            }
            
            //Player Animations
            if (i == 0)
                _playersInOrder[i].GetComponentInChildren<Animator>().SetTrigger("EndAnimationWin");
            else if (i > 0)
                _playersInOrder[i].GetComponentInChildren<Animator>().SetTrigger("EndAnimationLose");
            
            //Place Players
            _playersInOrder[i].GetComponent<Rigidbody>().isKinematic = true;
            _playersInOrder[i].transform.position = _ladderLocations[i].transform.position;
            _playersInOrder[i].transform.rotation = Quaternion.Euler(0,180,0);
        }
        
        _cam.Priority = 2;
        _confetti1.Play();
        _confetti2.Play();
        
    }
}
