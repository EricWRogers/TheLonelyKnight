using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class GameManager : MonoBehaviour
{

    public GameObject playerCamera;
    //Wave Timer for counting down the 2 minutes inbetween waves.
    public float WTimer = 10;

    //To adjust the width and height of the spawn.
    public int SpawnWidth = 75, SpawnHeight = 25;

    //A transform for AI placement purposes.
    public Transform center;

    //Create gameobjects to hold the three types of enemies we will be instantiating.
    public GameObject enemy, enemy2, enemy3;

    //The Number to read when needing to know what wave of enemies we are currently on.
    public int WaveNumber { get { return waveNumber; } }

    //Created an enum to keep track of the state machine.
    public enum WaveState
    {
        Resting,
        WaveStart,
        Spawning,
        WaveEnd,
        None
    };

    //Get Function for the WaveState enum.
    public WaveState WaveStateHolder { get { return waveState; } }

    //The public float value which gets the private float value of PlayerHealth.
    public float playrHealth { get { return PlayerHealth; } }

    //The public float value which gets the private float value of PlayerHealth.
    public float castleHealth { get { return CastleHealth; } }

    //Creates a new event.
    
    [SerializeField]
    public OnDeath onDeath;
    [SerializeField]
    public RestingWaveState restingWaveStateActive;

    public UnityEvent m_ResetingUiUpdate = new UnityEvent();

    //Created Instance of the Game Manager.
    public static GameManager Instance { get; private set; } = null;

    //The int max number of enemy spawns at any given time.
    public int SpawnCap = 40;

    //The public float value which gets the private float value of ScrapCount.
    public float scrapCount { get { return ScrapCount; } }

    //A public variable to retain the max Castle health.
    private float OriginalCastleHealth;

    //A bool to keep track of the moment the player gets hurts.
    private bool plyrHurt;

    //An int to keep track of an index number for randomized enemy placement.
    private int RndEnemy;

    //Three Vector3s to keep Enemy AIs seperate.
    private Vector3 TempPosition, TempPositionTwo, TempPositionThree;

    //A Timer for the Wave that keeps the max amount of Time inbetween waves.
    private float originalWTimer;

    //Set up WaveNumber we are on.
    //Initially Wave Number is set to zero at start because we wait two minutes before the first wave.
    private int waveNumber = 0;

    //Set up int for the number of enemies we are spawning.
    private int NumberEnemiesToSpawn = 0;

    //Set up private int value for the current number of enemies we have spawned.
    private int NumberEnemiesCurrentlySpawned = 0;

    //Serialized the private waveState.
    [SerializeField] private WaveState waveState;

    //The private float value of scrap parts the player collects to fix things.

    private float ScrapCount;

    //A Timer value for the Max number of time before the player starts to heal.
    private float OrigWaitedTime = 10;

    //The timer which counts down the time until the player is ready to heal.
    private float WaitedTimer = 0;

    //The private float value of player's health.
    [SerializeField] private float PlayerHealth;

    //The private float value of the castle health.
    [SerializeField] private float CastleHealth;

    //Destroy the instance.
    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }

        else
        {
            Destroy(this.gameObject);
        }
    }

    //Start Function
    void Start()
    {
        //Listeners for generic functions
        

        waveNumber = 1;
        PlayerHealth = 100f;
        CastleHealth = 100f;

        originalWTimer = WTimer;
        waveState = WaveState.None;
        OriginalCastleHealth=CastleHealth;
        WaitedTimer = OrigWaitedTime;
    }




    //Update Function
    void Update()
    {
        StateChanger();
        PlayerWaitedRestore();
    }

    //-------------------------------------------------------------------------------------

    //the function which changes the state manager using a switch statements.
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

            default:
            break;
        }
    }

    //The Resting State of the game.
    void StateResting()
    {
        restingWaveStateActive.Invoke();
        WTimer -= Time.deltaTime;
        
        if (WTimer < 0)
        {
            waveState = WaveState.WaveStart;
            WTimer = originalWTimer;
        }
    }

    //The Starting state of the wave.
    void StaeWaveStart()
    {
        NumberEnemiesToSpawn = waveNumber * 4 + 2;
        waveState = WaveState.Spawning;
    }

    //The Spawning State of the wave.
    void SateSpawning()
    {
        if(center != null)
        {
            Spawn();
        }else
        {
            waveState = WaveState.None;
        }
        
    }

    //Function where we actually spawn randomized enemies.
    void Spawn()
    {
        while (NumberEnemiesToSpawn > 0 && NumberEnemiesCurrentlySpawned < SpawnCap)
        {
            RndEnemy = Random.Range(1, 4);

            switch (RndEnemy)
            {
                case 1:
                    TempPosition = center.position + new Vector3(Random.Range(-SpawnHeight, SpawnHeight), 0, Random.Range(-SpawnWidth, SpawnWidth));
                    Instantiate(enemy, TempPosition, Quaternion.identity);
                    break;

                case 2:
                    TempPositionTwo = center.position + new Vector3(Random.Range(-SpawnHeight, SpawnHeight), 0, Random.Range(-SpawnWidth, SpawnWidth));
                    Instantiate(enemy2, TempPositionTwo, Quaternion.identity);
                    break;

                case 3:
                    TempPositionThree = center.position + new Vector3(Random.Range(-SpawnHeight, SpawnHeight), 0, Random.Range(-SpawnWidth, SpawnWidth));
                    Instantiate(enemy3, TempPositionThree, Quaternion.identity);
                    break;

                default:
                break;

            }

            //Instantiate(enemy, TempPosition, Quaternion.identity);
            NumberEnemiesCurrentlySpawned++;
            NumberEnemiesToSpawn--;
        }
        if (NumberEnemiesCurrentlySpawned <= 0)
        {
            waveState = WaveState.WaveEnd;
        }
    }


    //The Final State of the wave.
    void SateWaveEnd()
    {
        waveNumber++;
        waveState = WaveState.Resting;
    }

    //Start the resting period.
    public void StartRestingState()
    {
        waveState = WaveState.Resting;
    }

    //An Event for death.
    void MyAction()
    {
        //Handle the death screen popup here.
    }

    //An Event for messages.
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
        if(ScrapCount > Num)
        {
            ScrapCount -= Num;
        }
    }

    //Decrease the value of enemies currently spawned.
    public void OnAIDeath()
    {
        NumberEnemiesCurrentlySpawned--;
    }

    //Player takes damage function.
    public void PlayerDamageTaken(float num)
    {
        if(playrHealth > 0)
        {
            PlayerHealth -= num;
        }else
        {
            onDeath.Invoke();
        }
        plyrHurt = true;
    }

    //Restoring castle health.
    public void CastleHealthRestored(float num)
    {
        CastleHealth += num;
    }

    //Decrease castle health.
    public void CastleDamageTaken(float num)
    {
        CastleHealth -= num;
    }

    //Castle takes damage function.
    public void CastleDamageIsHere(float num)
    {
        if(num > castleHealth)
        {
            //GameOver
            onDeath.Invoke();

        } else if(castleHealth > 0){
            CastleHealth -= num;
        }
    }

    //A function to alot a certain time after the player hasn't gotten hurt to begin to heal and gain health.
    void PlayerWaitedRestore()
    {
        if (plyrHurt == true)
        {
            if(WaitedTimer > 0)
            {
                WaitedTimer -= Time.deltaTime;
            } 
            else 
            {
                      if(PlayerHealth < 100)
                      {
                        PlayerHealth += 0.5f;
                      } else 
                      {
                          plyrHurt = false;
                      }
            }
        } 
        else 
        {
            if(WaitedTimer != OrigWaitedTime)
            {
                WaitedTimer = OrigWaitedTime;
            }
        }
    }

}
[System.Serializable]
public class OnDeath : UnityEngine.Events.UnityEvent{ }
[System.Serializable]
public class RestingWaveState : UnityEngine.Events.UnityEvent{ }