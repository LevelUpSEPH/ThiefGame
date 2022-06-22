using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
public class EnemyScript : MonoBehaviour // ADD : ENEMY FOV and check if detected
{
    public Transform player;
    public Animator anim;
    public Animator playeranim;
    private EnemyMovementScript enemymove;
    public GameObject enemy;
    public GameObject enemyrag;
    public Rigidbody ragdollBody;
    private bool isAlive = true;
    private int timeToCall = 5;
    private bool sawPlayer = false;
    private bool calledCops;

    void Start()
    {
        enemymove = gameObject.GetComponent<EnemyMovementScript>();
    }

    // Update is called once per frame
    void Update()
    {
        if (sawPlayer)
        {
            enemymove.isWalking = false;
        }
        
        

    }
    public void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("PlayerAttackBox") && isAlive)
        {
            playeranim.SetTrigger("Attacking");
            isAlive = false;
            enemy.SetActive(false);
            enemyrag.SetActive(true);
            Vector3 direction = player.position - enemyrag.transform.position;
            ragdollBody.AddForce((Vector3.up + direction) * Time.deltaTime * 5000f, ForceMode.Impulse);
            gameObject.GetComponent<EnemyMovementScript>().isWalking = false;

        }
    }
    private void callCops()
    {
        // Enemy calls the cops and 10 sec countdown starts, after 10 seconds if player is still inside house spawn police and lose game

    }
    private IEnumerator timeToCallDecrement()
    {
        WaitForSeconds wait = new WaitForSeconds(1f);
        yield return wait;
        timeToCall--;
    }
    private bool InSight()
    {
        if (false)
            return false;
        else
            return true;
    }
    private bool UnObstructed()
    {
        if (!Physics.Raycast(enemy.transform.position, player.position - transform.position, 3))
        {
            return true;
        }
        else
            return false;
    }
    private void OnTriggerStay(Collider other)
    {
        if (other.gameObject.tag == "Player" && UnObstructed() && InSight())
        {
            sawPlayer = true;
            timeToCallDecrement();
            transform.LookAt(player.position);
            if (InSight() && UnObstructed() && timeToCall <= 0 && isAlive && !calledCops)
                callCops();

        }
    }
}