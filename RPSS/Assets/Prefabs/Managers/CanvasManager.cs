using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CanvasManager : MonoBehaviour
{
    [SerializeField] private GameObject MainMenuCanvas;
    [SerializeField] private GameObject InGameCanvas;
    [SerializeField] private GameObject GameOverCanvas;
    [SerializeField] private GameObject HowToCanvas;
    private List<GameObject> canvases = new List<GameObject>();

    [SerializeField] private TransitionController transitionController;

    private void Start()
    {
        canvases.Add(MainMenuCanvas);
        canvases.Add(InGameCanvas);
        canvases.Add(GameOverCanvas);
        canvases.Add(HowToCanvas);
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
}
