using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CanvasManager : MonoBehaviour
{
    [SerializeField] private GameObject MainMenuCanvas;
    [SerializeField] private GameObject PreGameCanvas;
    [SerializeField] private GameObject InGameCanvas;
    [SerializeField] private GameObject GameOverCanvas;
    [SerializeField] private GameObject HowToCanvas;
    [SerializeField] public GameObject TempCanvas;
    private List<GameObject> canvases = new List<GameObject>();

    [SerializeField] private TransitionController transitionController;
    private GameManager gameManager;

    private void Start()
    {
        gameManager = GameObject.FindGameObjectWithTag("GameManager").GetComponent<GameManager>();

        canvases.Add(MainMenuCanvas);
        canvases.Add(InGameCanvas);
        canvases.Add(GameOverCanvas);
        canvases.Add(HowToCanvas);
        canvases.Add(PreGameCanvas);
    }
    public void ChangeCanvas(GameStates state)
    {
        foreach (var item in canvases)
        {
            item.SetActive(false);
        }
        switch (state)
        {
            case GameStates.HOWTO:
                HowToCanvas.gameObject.SetActive(true);
                break;
            case GameStates.INMENU:
                MainMenuCanvas.gameObject.SetActive(true);
                break;
            case GameStates.PREGAME:
                PreGameCanvas.gameObject.SetActive(true);
                break;
            case GameStates.INGAME:
                InGameCanvas.gameObject.SetActive(true);
                break;
            case GameStates.GAMELOSE:
                GameOverCanvas.gameObject.SetActive(true);
                break;
            case GameStates.NULL:
                break;
            default:
                break;
        }
    }

    public void Transition(TransitionStates states)
    {
        transitionController.transitionChange(states);
    }

    public void ChangeGameState(GameStates state)
    {
        gameManager.ChangeState(state);
    }    
    public void ChangeGameState(int state)
    {
        gameManager.ChangeState((GameStates)state);
    }
    public void ChangeGameStateTransition(int state)
    {
        gameManager.ChangeStateWithTransition((GameStates)state);

    }    
    public void ChangeGameStateTransition(GameStates state)
    {
        gameManager.ChangeStateWithTransition(state);

    }

    public void QuitGame()
    {
        gameManager.QuitGame();
    }
}
