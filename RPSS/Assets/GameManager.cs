using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum GameStates{INMENU, HOWTO, PREGAME, INGAME, PAUSED, GAMELOSE, NULL }
public class GameManager : MonoBehaviour
{
    public GameStates currentState;
    private GameStates waitingStateToChangeTo;
    private float timer = 0f;
    private bool waitingForTimer;
    [SerializeField] CanvasManager MainCanvas;

    void Start()
    {
        if (MainCanvas == null)
            Debug.LogError("Canvas hasn't been set");
        ChangeState(GameStates.INMENU);
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
        timer = 2f;
        MainCanvas.Transition(TransitionStates.FADEOUT);
    }    
    
    public void ChangeStateWithTransition(int stateToChangeTo)
    {
        if (waitingForTimer)
            return;
        waitingStateToChangeTo = (GameStates)stateToChangeTo;
        waitingForTimer = true;
        timer = 2f;
        MainCanvas.Transition(TransitionStates.FADEOUT);
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
}
