using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using UniRx;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    [SerializeField] PlayerInput playerInput;
    const string HIGH_SCORE = "highScore";
    private const int characterTypeSize = 3;
    public const float maxGameTimer = 180;
    public static float gameTimer;
    public static int score;
    public static int highScore = 0;
    public static int[] tulipBloomNumbers;
    public static ReactiveProperty<bool> isGame;
    public static ReactiveProperty<bool> isResult;
    AudioSource audioSource;
    [SerializeField] AudioClip startSE;
    // Start is called before the first frame update
    void Start()
    {
        gameTimer = maxGameTimer;
        score = 0;
        highScore = LoadHighScore();
        tulipBloomNumbers = new int[characterTypeSize];
        isGame = new ReactiveProperty<bool>(false);
        isResult = new ReactiveProperty<bool>(false);
        playerInput.OnStartButtonObservable.Subscribe(flag => OnPlayButtonDown()).AddTo(gameObject);
        audioSource = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
        if (isGame.Value)
        {
            gameTimer -= Time.deltaTime;
            if (gameTimer <= 0)
            {
                isGame.Value = false;
                isResult.Value = true;
            }
        }
        if (Input.GetKeyDown(KeyCode.P))
        {
            if (isResult.Value)
            {
                if (score > highScore) SaveHighScore(score);
            }
            SceneTransition.Instance.ChangeScene(SceneManager.GetActiveScene().name);
        }
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (isResult.Value)
            {
                if (score > highScore) SaveHighScore(score);
            }
            Application.Quit();
        }
    }

    void OnPlayButtonDown()
    {
        if (isResult.Value)
        {
            if (score > highScore) SaveHighScore(score);
            SceneTransition.Instance.ChangeScene(SceneManager.GetActiveScene().name);
        }
        else
        {
            audioSource.PlayOneShot(startSE);
            isGame.Value = true;
        }
    }

    void SaveHighScore(int highScore)
    {
        PlayerPrefs.SetInt(HIGH_SCORE, score);
        PlayerPrefs.Save();
    }

    int LoadHighScore()
    {
        return PlayerPrefs.GetInt(HIGH_SCORE, 0);
    }
#if UNITY_EDITOR
    private void OnGUI()
    {
        GUI.Label(new Rect(0, 100, 200, 60), string.Format("Time:{0}\nScore:{1}", gameTimer, score));
        for (int i = 0; i < characterTypeSize; i++)
        {
            GUI.Label(new Rect(0, 160 + i * 30, 200, 30), string.Format("{0}:{1}", (CharacterType)Enum.ToObject(typeof(CharacterType), i), tulipBloomNumbers[i]));
        }
    }
#endif
}
