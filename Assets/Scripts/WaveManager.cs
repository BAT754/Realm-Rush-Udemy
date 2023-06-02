using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class WaveManager : MonoBehaviour
{
    [Header("Editor Info")]
    [SerializeField] ObjectPool pool;
    [SerializeField] List<WaveInfo> waves = new List<WaveInfo>();

    [Header("UI Elements")]
    [SerializeField] TextMeshProUGUI waveCounterText;
    [SerializeField] TextMeshProUGUI enemiesLeftText;
    [SerializeField] TextMeshProUGUI waveTimerText;

    [Header("Config Variables")]
    [SerializeField] int timeBetweenWaves = 5;

    int currentWaveIndex = 0;
    WaveInfo currentWave = null;
    int enemiesRemaining = 0;
    int waveTimer = 0;

    enum WaveState {
        waiting,
        loadingWave,
        inProgress,
        cleaningUp,
        intermission,
        complete
    }

    WaveState state = WaveState.waiting;
    bool changingState = false;


    // Start is called before the first frame update
    void Start()
    {
        currentWave = waves[currentWaveIndex];
        waveCounterText.text = "Press Enter";
        enemiesLeftText.text = "To start the game!";
    }

    // Update is called once per frame
    void Update()
    {
        if (!changingState)
        {
            switch (state)
            {
                // Game hasn't started yet
                case WaveState.waiting:         
                    if (Input.GetKeyDown(KeyCode.Return))
                    {
                        GiveWaveInfoToPool();
                        waveCounterText.text = "Wave Starting!";
                        enemiesLeftText.text = "";
                        StartCoroutine(SwapWaveState(WaveState.loadingWave, 2f));
                    }
                break;

                // Take the actions that need to happen to load the wave, and then move into inProgress
                case WaveState.loadingWave:
                    StartCoroutine(SwapWaveState(WaveState.inProgress, 1f));
                    StartWave();
                break;

                // Game is active, wave is going
                case WaveState.inProgress:      
                    UpdateEnemyCounter();

                    if (enemiesRemaining <= 0)
                    {
                        StartCoroutine(SwapWaveState(WaveState.cleaningUp, 0.5f));
                    }

                break;

                // Do anything needed to clean up wave, and then swap into intermission
                case WaveState.cleaningUp:
                    pool.ClearEnemyList();

                    currentWaveIndex += 1;

                    if (currentWaveIndex >= waves.Count)    // Game is over
                    {
                        GameOver();
                        return;
                    }


                    currentWave = waves[currentWaveIndex];
                    waveTimer = timeBetweenWaves;
                    GiveWaveInfoToPool();

                    StartCoroutine(SwapWaveState(WaveState.intermission, 0f));
                    StartCoroutine(WaveTimerCountdown());

                    enemiesLeftText.enabled = false;
                    waveTimerText.enabled = true;
                break;

                // Game is active, in between waves
                case WaveState.intermission:    
                    waveTimerText.text = "Next wave in: " + waveTimer;

                    if (waveTimer <= 0)
                    {
                        waveTimerText.enabled = false;
                        enemiesLeftText.enabled = true;
                        StartCoroutine(SwapWaveState(WaveState.loadingWave, 0f));
                    }
                break;

                // Game is complete
                case WaveState.complete:        

                break;

                // Just in case
                default:    
                    Debug.Log("ERROR: Something went really wrong if you're seeing this message");
                break;
            }
        }
    }

    void GameOver()
    {
        waveCounterText.text = "GAME OVER!";
        enemiesLeftText.text = "Great job!";
        state = WaveState.complete;
    }

    void GiveWaveInfoToPool()
    {
        // Give the current wave to the object pool so it knows what to spawn in
        pool.GetCurrentWave(currentWave);
    }

    void StartWave()
    {
        waveCounterText.text = "Wave: " + (currentWaveIndex + 1); 
        enemiesRemaining = pool.ActiveEnemies;
        enemiesLeftText.text = "Enemies Left: " + enemiesRemaining; 
        pool.StartWave();
    }

    void UpdateEnemyCounter()
    {
        enemiesRemaining = pool.ActiveEnemies;
        enemiesLeftText.text = "Enemies Left: " + enemiesRemaining;
    }

    IEnumerator SwapWaveState(WaveState newState, float delay)
    {
        changingState = true;       // Locks up the update method so nothing happens
        yield return new WaitForSeconds(delay);     // Gives a slight delay to the swapping of state
        state = newState;           // Changes the state
        changingState = false;      // Unlocks the update loop
    }

    IEnumerator WaveTimerCountdown()
    {
        while (waveTimer > 0)
        {
            yield return new WaitForSeconds(1f);
            waveTimer -= 1;
        }
    }
}
