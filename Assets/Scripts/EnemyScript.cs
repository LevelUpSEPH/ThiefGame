using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.UI;
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
    public int timeToCall = 5;
    private bool sawPlayer = false;
    public bool calledCops = false;
    private SensorScript sensor;
    public PlayerMovement playermove;
    public Text countdown;
    private int countdownTime = 9;
    
    private float countdownInterval, countdownCounter;
    private float countdownFrequency=1;
    void Start()
    {
        enemymove = gameObject.GetComponent<EnemyMovementScript>();
        sensor = enemy.GetComponent<SensorScript>();
        countdownInterval = 1f / countdownFrequency;
        countdownCounter = countdownInterval;
    }

    // Update is called once per frame
    void Update()
    {
        if (sawPlayer)
        {
            enemymove.isWalking = false;
        }
        if (sensor.isInSight(player.gameObject))
            canSeePlayer();


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
        calledCops = true;
        float callCopsInterval, callCopsCounter;
        callCopsInterval = 1;
        callCopsCounter = callCopsInterval;
        while (countdownTime > 0)
        {
            callCopsCounter -= Time.deltaTime;
            if (callCopsCounter < 0)
            {
                countdown.gameObject.SetActive(true);
                playermove.MovementSpeed = 3;
                countdown.text = countdownTime.ToString();
                countdownTime--;
                callCopsCounter = callCopsInterval;
            }
        }
        //cops arrive
    }
    private void timeToCallDecrement()
    {
        
        timeToCall--;
        print("Decrement");
        
        
    }
    private void canSeePlayer()
    {
        sawPlayer = true;
        countdownCounter -= Time.deltaTime;
        if(countdownCounter < 0)
        {
            timeToCallDecrement();
            countdownCounter = countdownInterval;
        }
        if (sensor.isInSight(player.gameObject) && isAlive)
            transform.LookAt(player.position);
        if (sensor.isInSight(player.gameObject) && timeToCall <= 0 && isAlive && !calledCops)
            callCops();

    }
}
