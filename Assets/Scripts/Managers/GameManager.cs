using System;
using Data;
using Unity.Cinemachine;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;

    [SerializeField] private UIManager UImanager;
    [SerializeField] private HealthBar healthBar;

    [SerializeField] private AudioSource noteSuccessSound;
    [SerializeField] private AudioSource noteMissSound;

    [SerializeField] private CinemachineImpulseSource listener;

    [SerializeField] private int score = 0;
    [SerializeField] private int combo = 0;
    [SerializeField] private float scoreMultiplier;
    [SerializeField] private float scoreMultiplierLimit;

    public event Action OnNoteSuccess;
    public event Action OnUIUpdate;
    public event Action OnNoteMiss;
    public event Action OnHitTaken;
    public event Action OnHealthChanged;
    public event Action OnSongBeat;
    public event Action OnSongEnd;

    public AudioSource SuccessNoteSound => noteSuccessSound;
    public AudioSource MissNoteSound => noteMissSound;

    public HealthBar HealthBar => healthBar;

    public float Multiplier { get => scoreMultiplier; set => scoreMultiplier = value; }
    public int Combo { get => combo; set => combo = value; }
    public int ScoreScale { get => scoreScale; set => scoreScale = value; }
    public int Score {  get => score; set => score = value;}


    [SerializeField] private float scoreMultiplierIncrease = 0.25f;
    [SerializeField] private float scoreMultiplerDefault = 1.00f;
    [SerializeField] private int scoreScale = 4;

    public SoGameController gamedata;

    //test

    private float gradeNumber = 0;
    private float occurenceNumber = 0;

    private void Start()
    {
        scoreMultiplier = scoreMultiplerDefault;
    }

    private void Awake()
    { 
        Instance = this;
    }

    public void LoadScene(string scene)
    {
        StartCoroutine(LoadSceneAsync(scene));
    }

    private System.Collections.IEnumerator LoadSceneAsync(string scene)
    {
        AsyncOperation asyncLoad = SceneManager.LoadSceneAsync(scene);

        while (!asyncLoad.isDone)
        {
            yield return null;
        }
    }

    public void SongBeat()
    {
        OnSongBeat?.Invoke();
    }

    public void ApplyImpulse()
    {
        //listener.GenerateImpulse();
    }

    public void ApplyPulse()
    {
        OnNoteSuccess?.Invoke();
    }
    
    public void InvokeOnHealthChange()
    {
        OnHealthChanged?.Invoke();
    }

    public void InvokeOnHitTaken()
    {
        occurenceNumber++;
        OnHitTaken?.Invoke();
    }

    public void InvokeSongEnd(bool dnf)
    {
        //if dnf show score but do not store it

        //otherwise show the score normally.

        if(dnf == true)
        {
            gamedata.currentSongData.grade = 0f;
        }
        else
        {
            gamedata.currentSongData.grade = Mathf.Round((gradeNumber / occurenceNumber) * 100);
        }

        OnSongEnd?.Invoke();
    }

    public void IncrementScore(int hit)
    {
        gradeNumber += 1;
        occurenceNumber++;

        score = score + (int)MathF.Round(hit * Multiplier * ScoreScale);
        OnUIUpdate?.Invoke();
    }

    public void ResetCombo()
    {
        combo = 0;
        scoreMultiplier = scoreMultiplerDefault;
        OnUIUpdate?.Invoke();
    }

    public void IncrementCombo() { combo++; OnUIUpdate?.Invoke(); }

    public void IncrementMultiplier()
    {
        if(scoreMultiplier >= scoreMultiplierLimit)
        {
            return;
        }
        scoreMultiplier += scoreMultiplierIncrease;
    }

    private void OnDisable()
    {
        Debug.Log((gradeNumber / occurenceNumber) * 100);
    }

}
