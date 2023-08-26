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
        
    }

    // Update is called once per frame
    void Update()
    {
        navMeshAgent.SetDestination(PlayerController.instance.transform.position);
        if(navMeshAgent.velocity == new Vector3(0,0,0)){
            animator.SetBool("IsWalking", false);
        }else{
            animator.SetBool("IsWalking", true);
        }
    }
    void OnCollisionEnter(Collision collision) {
        Debug.Log("Collided with: " + collision.gameObject.name);
    }
}
