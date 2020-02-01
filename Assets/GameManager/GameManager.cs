using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class GameManager : MonoBehaviour
{

    public float OriginalCastleHealth;

    public Transform center;

    //Create gameobjects to hold the three types of enemies we will be instantiating.
    public GameObject enemy;

    private Vector3 TempPosition; 

    //Wave Timer for counting down the 2 minutes inbetween waves.
    public float WTimer = 10;

    private float originalWTimer;

    //Set up WaveNumber we are on.
    //Initially Wave Number is set to zero at start because we wait two minutes before the first wave.
    private int WaveNumber = 0;

    //Set up int for the number of enemies we are spawning.
    private int NumberEnemiesToSpawn = 0;

    //Set up private int value for the current number of enemies we have spawned.
    private int NumberEnemiesCurrentlySpawned = 0;

    public enum WaveState
    {
        Resting,
        WaveStart,
        Spawning,
        WaveEnd,
        None
    };
    
    [SerializeField] private WaveState waveState;

    public int SpawnCap = 40;
    //Creates a new event.
    public UnityEvent m_Death = new UnityEvent();
    public UnityEvent m_Messages = new UnityEvent();

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

    void Start()
    {
        //Listeners for generic functions
        m_Death.AddListener(MyAction);
        m_Messages.AddListener(MyMessages);

        waveState = WaveState.Resting;
        WaveNumber = 20;
        originalWTimer = WTimer;

        CastleHealth = OriginalCastleHealth;
    }


    void Update() 
    {
        StateChanger();
    }

//-------------------------------------------------------------------------------------

    void StateChanger()
    {
        switch (waveState)
        {
            case WaveState.Resting:
                StateResting();
                break;

            case WaveState.WaveStart:
                StaeWaveStart();
                break;

            case WaveState.Spawning:
                SateSpawning();
                break;

            case WaveState.WaveEnd:
                SateWaveEnd();
                break;
        }
    }

    void StateResting()
    {
        WTimer -= Time.deltaTime;

        if(WTimer < 0)
        {
            waveState = WaveState.WaveStart;
            WTimer = originalWTimer;
        }
    }

    void StaeWaveStart()
    {
        // 
        NumberEnemiesToSpawn = WaveNumber * 4 + 2;
        waveState = WaveState.Spawning;
    }

    void SateSpawning()
    {
        Spawn();
    }

    void SateWaveEnd()
    {
        WaveNumber++;

        waveState = WaveState.Resting;
    }

    void Spawn()
    {
        while(NumberEnemiesToSpawn > 0 && NumberEnemiesCurrentlySpawned < SpawnCap)
        {            
            TempPosition  = center.position + new Vector3(Random.Range(-25.0f, 25.0f), 0, Random.Range(-75.0f, 75.0f));

            Instantiate (enemy, TempPosition, Quaternion.identity);
            NumberEnemiesCurrentlySpawned++;
            NumberEnemiesToSpawn--;
        }

        if(NumberEnemiesCurrentlySpawned <= 0)
        {
            waveState = WaveState.WaveEnd;
        }
    }

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

    public void OnAIDeath()
    {
        NumberEnemiesCurrentlySpawned--;
    }

    public void PlayerDamageTaken(float num)
    {
        PlayerHealth -= num;
    }

        public void CastleHealthRestored(float num)
    {
        CastleHealth += num;
    }

        public void CastleDamageTaken(float num)
    {
        CastleHealth -= num;
    }
}