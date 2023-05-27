using UnityEngine;
using UnityEngine.UI;
using TMPro;
public class MyScoreText : MonoBehaviour
{
    public TextMeshProUGUI scoreText;
    public TextMeshProUGUI accuracyText;
    public int thrownVegi_count = 0;
    public int CuttenVegi_count = 0;
    private void Start()
    {
        thrownVegi_count = 0;
        CuttenVegi_count = 0;
    }
    public void SetText()
    {
        scoreText.text = CuttenVegi_count.ToString() + " / " + thrownVegi_count.ToString();
        if(thrownVegi_count>0)
            accuracyText.text = ((float)CuttenVegi_count * 100 / thrownVegi_count).ToString("0.0") + "%";
    }
}
