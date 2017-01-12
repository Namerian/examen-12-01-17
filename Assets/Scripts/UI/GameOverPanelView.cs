using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class GameOverPanelView : MonoBehaviour
{
    private CanvasGroup _canvasGroup;
    private Text _scoreText;

    // Use this for initialization
    void Start()
    {
        GameManager.Instance._GameOverPanelView = this;

        _canvasGroup = this.GetComponent<CanvasGroup>();
        _scoreText = this.transform.FindChild("ScoreText").GetComponent<Text>();
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void SetVisible(bool visible)
    {
        if (visible)
        {
            _canvasGroup.alpha = 1;
        }
        else
        {
            _canvasGroup.alpha = 0;
        }
    }

    public void SetScore(int score)
    {
        _scoreText.text = "Score = " + score;
    }
}
