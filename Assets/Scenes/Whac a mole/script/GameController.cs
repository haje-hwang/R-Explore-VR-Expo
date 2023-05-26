using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameController : MonoBehaviour
{
    [SerializeField]
    private CountDown countDown;
    [SerializeField]
    private MoleSpawner moleSpawner;
    private int score;
    private float currentTime;
    private int combo;

    public int Score
    {
        set => score = Mathf.Max(0,value);
        get => score;
    }
    public int Combo
    {
        set 
        { 
            combo=Mathf.Max(0,value);

            if (combo <= 70)
            {
                moleSpawner.MaxSpawnMole = 1 + (combo + 10) / 20;
            }
            if(combo > maxCombo)
            {
                maxCombo=combo;
            }
        }
        get => combo;
    }

    public int maxCombo { private set; get; }
    public int NormalMoleHitCount {  set; get;}
    public int RedMoleHitCount { set; get; }
    public int BlueMoleHitCount { set; get; }
    public int GreenMoleHitCount { set; get; }

    [field:SerializeField]
    public float MaxTime { private set; get; }
    //public float CurrentTime { private set; get; }
    public float CurrentTime 
    { 
        set=> currentTime = Mathf.Clamp(value,0,MaxTime);
        get => currentTime;
    }

    private void Start()
    {
        countDown.StartCountDown(GameStart);
    }
    private void GameStart()
    {
        moleSpawner.SetUp();

        StartCoroutine("OnTimeCount");
    }

    private IEnumerator OnTimeCount()
    {
        CurrentTime = MaxTime;

        while(CurrentTime > 0)
        {
            CurrentTime-= Time.deltaTime;
            yield return null; 
        }

        GameOver();
    }

    private void GameOver()
    {
        PlayerPrefs.SetInt("CurrentScore", Score);
        PlayerPrefs.SetInt("CurrentMaxCombo", maxCombo);
        PlayerPrefs.SetInt("CurrentNormalMoleHitCount", NormalMoleHitCount);
        PlayerPrefs.SetInt("CurrentRedMoleHitCount", RedMoleHitCount);
        PlayerPrefs.SetInt("CurrentGreenMoleHitCount", GreenMoleHitCount);
        PlayerPrefs.SetInt("CurrentBlueMoleHitCount", BlueMoleHitCount);

        SceneManager.LoadScene("W-A-M GameOver");
    }
}
