using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class GameManager : MonoBehaviour
{
    #region Data

    public static GameManager Instance;

    public enum TypeStateGame
    {
        Menu,
        Gameplay,
        Endgame
    }

    [Header("Other")]

    public TypeStateGame stateGame = TypeStateGame.Menu;
    public PlayerController playerController;
    public LevelMangaer levelManager;

    [Header("Camera")]

    public Transform pivotCamera;
    public float smoothLerpCam;

    [Space]

    [SerializeField] private Vector3 posCamMenu;
    [SerializeField] private Vector3 rotCamMenu;

    [Space]

    [SerializeField] private Vector3 posCamGameplay;
    [SerializeField] private Vector3 rotCamGameplay;

    [Space]

    [SerializeField] private Vector3 posCamEndgame;
    [SerializeField] private Vector3 rotCamEndgame;

    [Header("UI")]
    public GameObject windowMenu;
    public GameObject windowGameplay;
    public GameObject windowEndmenu;

    public TextMeshProUGUI txtScore;

    [Header("Session")]
    public int score;

    #endregion

    #region Unity

    private void Awake()
    {
        Instance = this;
    }

    public void Update()
    {
        GameControl();   
    }

    #endregion

    #region Core

    private void GameControl()
    {
        switch(stateGame)
        {
            case TypeStateGame.Menu:
                pivotCamera.localPosition = Vector3.Lerp(pivotCamera.localPosition, posCamMenu, smoothLerpCam * Time.deltaTime);
                pivotCamera.localRotation = Quaternion.Lerp(pivotCamera.localRotation, Quaternion.Euler(rotCamMenu), smoothLerpCam * Time.deltaTime);
                break;

            case TypeStateGame.Gameplay:
                pivotCamera.localPosition = Vector3.Lerp(pivotCamera.localPosition, posCamGameplay, smoothLerpCam * Time.deltaTime);
                pivotCamera.localRotation = Quaternion.Lerp(pivotCamera.localRotation, Quaternion.Euler(rotCamGameplay), smoothLerpCam * Time.deltaTime);

                playerController.CoreUpdate();
                levelManager.CoreUpdate();
                break;

            case TypeStateGame.Endgame:
                pivotCamera.localPosition = Vector3.Lerp(pivotCamera.localPosition, posCamEndgame, smoothLerpCam * Time.deltaTime);
                pivotCamera.localRotation = Quaternion.Lerp(pivotCamera.localRotation, Quaternion.Euler(rotCamEndgame), smoothLerpCam * Time.deltaTime);
                break;
        }
    }

    public void StartGame()
    {
        playerController.Initialization(2f);
        playerController.StartGame();

        levelManager.ResetManager();

        stateGame = TypeStateGame.Gameplay;

        txtScore.text = (score = 0).ToString();

        CloseMenu();
        OpenGameplay();
    }

    public void StopGame()
    {
        stateGame = TypeStateGame.Endgame;

        playerController.StopGame();

        CloseGameplay();
        OpenEndgame();
    }

    public void RepeatGame()
    {
        CloseEndgame();

        StartCoroutine(DelayRerpeat());
    }

    private IEnumerator DelayRerpeat()
    {
        playerController.Initialization(2f);

        levelManager.ResetManager();

        stateGame = TypeStateGame.Gameplay;

        txtScore.text = (score = 0).ToString();

        CloseMenu();
        OpenGameplay();

        yield return new WaitWhile(() => Mathf.Abs((pivotCamera.localPosition - posCamGameplay).magnitude) > 4f);

        playerController.StartGame();
    }

    public void CrashGame()
    {
        StopGame();
        print("Crash");
    }

    public void AddScore()
    {
        txtScore.text = (++score).ToString();
        playerController.speed += 1f;
        print("AddScore");
    }

    #region UI

    private void OpenMenu()
    {
        windowMenu?.SetActive(true);
    }

    private void CloseMenu()
    {
        windowMenu?.SetActive(false);
    }

    private void OpenEndgame()
    {
        windowEndmenu?.SetActive(true);
    }

    private void CloseEndgame()
    {
        windowEndmenu?.SetActive(false);
    }

    private void OpenGameplay()
    {
        windowGameplay?.SetActive(true);
    }

    private void CloseGameplay()
    {
        windowGameplay?.SetActive(false);
    }

    #endregion

#endregion
}
