using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class GameManager : MonoBehaviour
{
    //Create gameobjects to hold the three types of enemies we will be instantiating.
    public GameObject enemy;

    private Transform position;

    //Wave Timer for counting down the 2 minutes inbetween waves.
    private float WTimer = 7200;

    //Set up WaveNumber we are on.
    //Initially Wave Number is set to zero at start because we wait two minutes before the first wave.
    private int WaveNumber = 0;

    //Set up int for the number of enemies we are spawning.
    private int NumberEnemiesToSpawn = 0;

    //Set up private int value for the current number of enemies we have spawned.
    private int NumberEnemiesCurrentlySpawned = 1;

    //Creates a new event.
    public UnityEvent m_Death = new UnityEvent();
    public UnityEvent m_Messages = new UnityEvent();

    void Start()
    {

        //Invoke the Spawner
        InvokeRepeating ("Wave", WTimer, WTimer);

        //Listeners for generic functions
        m_Death.AddListener(MyAction);
        m_Messages.AddListener(MyMessages);

    }


    //The private float value of scrap parts the player collects to fix things.
    private float ScrapCount;

    //The four private floats for Turrets.
    private float Turret1;
    private float Turret2;
    private float Turret3;
    private float Turret4;

    //The private float value of player's health.
    private float PlayerHealth;

    //The private float value of the castle health.
    private float CastleHealth;

    //The public float value which gets the private float value of ScrapCount.
    public float scrpcont { get { return ScrapCount; }}

    //The public float value which gets the private float value of Turrets 1-4.
    public float turr1 { get { return Turret1; } }
    public float turr2 { get { return Turret2; } }
    public float turr3 { get { return Turret3; } }
    public float turr4 { get { return Turret4; } }

    //The public float value which gets the private float value of PlayerHealth.
    public float PlyrHealth { get { return PlayerHealth; } }

    //The public float value which gets the private float value of PlayerHealth.
    public float CstlHealth { get { return CastleHealth; } }

//-------------------------------------------------------------------------------------

    void MyAction()
    {
        //Handle the death screen popup here.

    }

    void MyMessages()
    {
        //handle all the messages in the game here.
    }

    //Function called by other scripts to increase the value of ScrapCount.
    public void AddScrapToCount(float Num)
    {
        ScrapCount += Num;
    }

    //Function called by other scripts to decrease the value of ScrapCount.
    public void SubtractScrapFromCount(float Num)
    {
        ScrapCount -= Num;
    }

    public void Wave()
    {
            //work on all spawning enemies and waves here.

            //Increase the number of the wave we are currently on.
            WaveNumber+=1;

            //calculate the new number of Enemies to spawn based on the algorthym provided by Brandon, Eric, and Justin.
            NumberEnemiesToSpawn = WaveNumber * 4 + 2;

            position.position = new Vector3(Random.Range(-10.0f, 10.0f), 0, Random.Range(-10.0f, 10.0f));

            //Spawn the enemy using the calculations we have just done.
            while(NumberEnemiesCurrentlySpawned <= NumberEnemiesToSpawn){
                Instantiate (enemy, position);
                NumberEnemiesCurrentlySpawned += 1;
            }
            
            NumberEnemiesCurrentlySpawned = 1;

    }





}