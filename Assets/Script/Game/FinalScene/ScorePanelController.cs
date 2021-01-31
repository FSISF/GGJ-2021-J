using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ScorePanelController : MonoBehaviour
{
    public Text scoreText;

    private int targetScore = 0;

    private int currScore = 0;

    // Update is called once per frame
    void Update()
    {
        if (targetScore != currScore)
        {
            currScore = Mathf.CeilToInt(Mathf.Lerp(currScore, targetScore, 5f * Time.deltaTime));
            scoreText.text = $"{currScore:D8}";
        }
    }

    public void UpdateScore(int score)
    {
        targetScore = score;
    }
}
