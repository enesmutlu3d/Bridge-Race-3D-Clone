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
            GameObject temp = _players[0];
            foreach (var player in _players)
            {
                if (temp.transform.position.z < player.transform.position.z)
                {
                    temp = player;
                }
            }
            _playersInOrder.Add(temp);
            _players.Remove(temp);
        } while (_players.Count != 0);
    }

    private void LevelEnd()
    {
        for (int i = 0; i < _playersInOrder.Count; i++)
        {
            GameObject player = _playersInOrder[i];
            
            //Ai State
            if (player.TryGetComponent<AiStateMachine>(out AiStateMachine aiStateMachine))
            {
                if (i == 0)
                    aiStateMachine.ChangeState(typeof(AiWinnerState));
                else
                    aiStateMachine.ChangeState(typeof(AiLoserState));
            }
            
            //Player State
            if (player.TryGetComponent<PlayerMovement>(out PlayerMovement playerMovement))
            {
                playerMovement.PlayerFinishState(i);
                player.GetComponent<Rigidbody>().isKinematic = true;
            }
            
            //Player Stack Hide
            player.GetComponentInChildren<PlayerStackParent>().transform.gameObject.SetActive(false);
            
            //Place Players
            //player.GetComponent<Rigidbody>().isKinematic = true;
            player.transform.position = _ladderLocations[i].transform.position;
            player.transform.rotation = Quaternion.Euler(0,180,0);
        }
        
        _cam.Priority = 2;
        _confetti1.Play();
        _confetti2.Play();
        
    }
}
