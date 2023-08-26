using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class FollowingMinionController : MonoBehaviour
{
    public CharacterData characterData;
    NavMeshAgent navMeshAgent;
    Animator animator;

    void Awake()
    {
        // NavMeshHit closestHit;
        // NavMesh.SamplePosition(  transform.position, out closestHit, 500, 1 );
        // transform.position = closestHit.position;
        animator = GetComponentInChildren<Animator>();
        navMeshAgent = GetComponent<NavMeshAgent>();
        navMeshAgent.speed = characterData.SPEED;
        
    }

    void Start() {
        navMeshAgent.SetDestination(new Vector3(0,0,0));
        navMeshAgent.enabled = true;
    }

    // Update is called once per frame
    void Update()
    {
    if(navMeshAgent.velocity == new Vector3(0,0,0)){
            animator.SetBool("IsWalking", false);
        }else{
            animator.SetBool("IsWalking", true);
        }
    }
}
