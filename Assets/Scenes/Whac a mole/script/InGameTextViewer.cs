using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class InGameTextViewer : MonoBehaviour
{
    [SerializeField]
    private GameController gameController;
    [SerializeField]
    private TextMeshProUGUI textScore;
    [SerializeField]
    private TextMeshProUGUI textPlayTime;
    [SerializeField]
    private Slider sliderPlayTime;
    [SerializeField]
    private TextMeshProUGUI textCombo;

    private void Update()
    {
        textScore.text = "Score : " + gameController.Score;

        textPlayTime.text = gameController.CurrentTime.ToString("F1");
        sliderPlayTime.value = gameController.CurrentTime / gameController.MaxTime;

        if (gameController.Combo == 0)
        {
            textCombo.text = "";
        }
        else if (gameController.Combo > 0)
        {
            textCombo.text = gameController.Combo + " Combo!!";
        }
    }
}
