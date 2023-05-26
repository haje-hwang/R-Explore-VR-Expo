using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class RankSystem : MonoBehaviour
{
    [SerializeField]
    private int maxRankCount = 10;
    [SerializeField]
    private GameObject textPrefab;
    [SerializeField]
    private Transform panelRankInfo;

    private RankData[] rankDataArray;
    private int currentIndex = 0;

    private void Awake()
    {
        rankDataArray= new RankData[maxRankCount];

        LoadRankData();

        CompareRank();

        PrintRankData();

        SaveRankData();
    }

    private void LoadRankData()
    {
        for(int i = 0; i < maxRankCount; ++i)
        {
            rankDataArray[i].score = PlayerPrefs.GetInt("RankScore" + i);
            rankDataArray[i].maxCombo = PlayerPrefs.GetInt("RankMaxCombo" + i);
            rankDataArray[i].normalMoleHitCount = PlayerPrefs.GetInt("RankNormalMoleHitCount" + i);
            rankDataArray[i].RedMoleHitCount = PlayerPrefs.GetInt("RankRedMoleHitCount" + i);
            rankDataArray[i].GreenMoleHitCount = PlayerPrefs.GetInt("RankGreenMoleHitCount" + i);
            rankDataArray[i].BlueMoleHitCount = PlayerPrefs.GetInt("RankBlueMoleHitCount" + i);
        }
    }

    private void SpawnText(string print, Color color)
    {
        GameObject clone = Instantiate(textPrefab);
        TextMeshProUGUI text = clone.GetComponent<TextMeshProUGUI>();

        clone.transform.SetParent(panelRankInfo);
        clone.transform.localScale = Vector3.one;

        text.text = print;
        text.color = color;

    }

    private void CompareRank()
    {
        RankData currenData = new RankData();
        currenData.score = PlayerPrefs.GetInt("CurrentScore");
        currenData.maxCombo = PlayerPrefs.GetInt("CurrentMaxCombo");
        currenData.normalMoleHitCount = PlayerPrefs.GetInt("CurrentNormalMoleHitCount");
        currenData.RedMoleHitCount = PlayerPrefs.GetInt("CurrentRedMoleHitCount");
        currenData.GreenMoleHitCount = PlayerPrefs.GetInt("CurrentGreenMoleHitCount");
        currenData.BlueMoleHitCount = PlayerPrefs.GetInt("CurrentBlueMoleHitCount");

        for(int i = 0; i < maxRankCount; ++i)
        {
            if (currenData.score > rankDataArray[i].score)
            {
                currentIndex=i;
                break;
            }
        }
        for(int i = maxRankCount - 1; i > 0; --i)
        {
            rankDataArray[i] = rankDataArray[i-1];
            if (currentIndex == i - 1)
            {
                break;
            }
        }

        rankDataArray[currentIndex] = currenData;
    }

    private void PrintRankData()
    {
        Color color= Color.white;

        for(int i = 0;i<maxRankCount; ++i)
        {
            color = currentIndex != i ? Color.white : Color.yellow;

            SpawnText((i + 1).ToString(), color);
            SpawnText(rankDataArray[i].score.ToString(), color);
            SpawnText(rankDataArray[i].maxCombo.ToString(), color);
            SpawnText(rankDataArray[i].normalMoleHitCount.ToString(), color);
            SpawnText(rankDataArray[i].RedMoleHitCount.ToString(), color);
            SpawnText(rankDataArray[i].GreenMoleHitCount.ToString(), color);
            SpawnText(rankDataArray[i].BlueMoleHitCount.ToString(), color);
        }
    }

    private void SaveRankData()
    {
        for(int i = 0;i<maxRankCount; ++i)
        {
            PlayerPrefs.SetInt("RankScore" + i, rankDataArray[i].score);
            PlayerPrefs.SetInt("RankMaxCombo" + i, rankDataArray[i].maxCombo);
            PlayerPrefs.SetInt("RankNormalMoleHitCount" + i, rankDataArray[i].normalMoleHitCount);
            PlayerPrefs.SetInt("RankRedMoleHitCount" + i, rankDataArray[i].RedMoleHitCount);
            PlayerPrefs.SetInt("RankGreenMoleHitCount" + i, rankDataArray[i].GreenMoleHitCount);
            PlayerPrefs.SetInt("RankBlueMoleHitCount" + i, rankDataArray[i].BlueMoleHitCount);
        }
    }
}

[System.Serializable]
public struct RankData
{
    public int score;
    public int maxCombo;
    public int normalMoleHitCount;
    public int RedMoleHitCount;
    public int GreenMoleHitCount;
    public int BlueMoleHitCount;

}
