using System.Collections;
using System.Collections.Generic;
using FMOD.Studio;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public enum GameStates{INMENU, HOWTO, PREGAME, INGAME, PAUSED, GAMELOSE, NULL }
public class GameManager : Singleton<GameManager>
{
    public GameStates currentState;
    private GameStates waitingStateToChangeTo;
    private float timer = 0f;
    private float preGameTimer = 4.5f;
    private bool waitingForTimer;
    [SerializeField] CanvasManager MainCanvas;

    private EventInstance music;
    private string musicPath = "event:/Ambience/Music/Music";

    void Start()
    {
        music = FMODUnity.RuntimeManager.CreateInstance(musicPath);
        MainCanvas = GameObject.FindGameObjectWithTag("MainCanvas").GetComponent<CanvasManager>();
        ChangeState(GameStates.INMENU);
        music.start();
        MainCanvas.Transition(TransitionStates.FADEIN);
    }

    private void Update()
    {
        if(waitingForTimer)
        {
            if(timer <= 0f)
            {
                waitingForTimer = false;
                currentState = waitingStateToChangeTo;
                MainCanvas.ChangeCanvas(currentState);
                MainCanvas.Transition(TransitionStates.FADEIN);
            }
            else
            {
                timer -= Time.deltaTime;
            }
        }

        roundNumber.text = aiController.currentWave.ToString();

        if (currentState != GameStates.PREGAME) return;
        preGameTimer -= Time.deltaTime;

        if (preGameTimer > 0) return;
        preGameTimer = 4.5f;
        ChangeState(GameStates.INGAME);

    }

    public void ChangeState(GameStates stateToChangeTo)
    {
        currentState = stateToChangeTo;
        MainCanvas.ChangeCanvas(currentState);

    }    
    public void ChangeState(int stateToChangeTo)
    {
        currentState = (GameStates)stateToChangeTo;
        MainCanvas.ChangeCanvas(currentState);

    }

    public void ChangeStateWithTransition(GameStates stateToChangeTo)
    {
        if (waitingForTimer)
            return;
        waitingStateToChangeTo = stateToChangeTo;
        waitingForTimer = true;
        timer = 1f;
        MainCanvas.Transition(TransitionStates.FADEOUT);
        
        if (currentState == GameStates.PREGAME)
            GameObject.FindWithTag("Player").GetComponent<Player>().Setup();

    }    
    
    public void ChangeStateWithTransition(int stateToChangeTo)
    {
        if (waitingForTimer)
            return;
        waitingStateToChangeTo = (GameStates)stateToChangeTo;
        waitingForTimer = true;
        timer = 2f;
        MainCanvas.Transition(TransitionStates.FADEOUT);
        
        if (currentState == GameStates.PREGAME)
            GameObject.FindWithTag("Player").GetComponent<Player>().Setup();
    }

    public void PauseGameSpeed(bool pause)
    {
        if (pause)
            Time.timeScale = 0f;
        else
            Time.timeScale = 1f;
    }

    public void QuitGame()
    {
        Application.Quit();
    }


    public void ResetScene()
    {
        SceneManager.LoadScene(0);
    }

    public TextMeshProUGUI roundNumber;
    public AiController aiController;

    void UpdateRoundNum()
    {
        Debug.Log(roundNumber.text + " " + aiController.currentWave);
    }
}
