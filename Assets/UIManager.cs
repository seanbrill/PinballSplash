using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

public class UIManager : MonoBehaviour
{
    public GameObject multiballMeterObj;
    public Meter multiballMeter;
    public GameObject powerMeterObj;
    public Meter powerMeter;
    public GameObject[] lifeSymbols;
    public GameObject altLives;
    public GameObject altLifeText;
    public TextMeshProUGUI scoreText;
    public Canvas screenCanvas;
    public GameObject popupPrefab;
    public GameObject gameMaster;

    public GameObject gameOverPanel;
    public GameObject gameOverScore;
    public GameObject uiPanel;
    public Camera camera;



    public bool isGameOver = false;
    public bool gameEnding = false;
    public int Lives
    {
        get {
            return _lives;
        }
        set {
            _lives = value;
            switch(value)
            {
                case 0:
                    lifeSymbols[0].SetActive(false);
                    lifeSymbols[1].SetActive(false);
                    lifeSymbols[2].SetActive(false);
                    altLives.SetActive(false);
                    gameEnding = true;
                    break;
                case 1:
                    lifeSymbols[0].SetActive(true);
                    lifeSymbols[1].SetActive(false);
                    lifeSymbols[2].SetActive(false);
                    altLives.SetActive(false);
                    break;
                case 2:
                    lifeSymbols[0].SetActive(true);
                    lifeSymbols[1].SetActive(true);
                    lifeSymbols[2].SetActive(false);
                    altLives.SetActive(false);
                    break;
                case 3:
                    lifeSymbols[0].SetActive(true);
                    lifeSymbols[1].SetActive(true);
                    lifeSymbols[2].SetActive(true);
                    altLives.SetActive(false);
                    break;
                default:
                    lifeSymbols[0].SetActive(false);
                    lifeSymbols[1].SetActive(false);
                    lifeSymbols[2].SetActive(false);
                    altLives.SetActive(true);
                    altLifeText.GetComponent<TMP_Text>().text = string.Format("x{0:d}", value);
                    break;
            }
        }
    }

    private int _lives;
    private int _score;

    public float popupFade;
    public float popupTime;
    // Start is called before the first frame update
    void Start()
    {
        Lives = 3;
        multiballMeter = multiballMeterObj.GetComponent<Meter>();
        powerMeter = powerMeterObj.GetComponent<Meter>();
        multiballMeter.Value = 0.0f;
        powerMeter.Value = 0.0f;
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.G)) {
            AddScore(100);
        }
        if(multiballMeter.Value >= multiballMeter.maxVal)
        {
            multiballMeter.Value = 0.0f;
            Lives += 1;
        }
        powerMeter.Value = pinballLauncherController.shotPower;
        if(gameEnding)
        {
            gameOverPanel.SetActive(true);
            uiPanel.SetActive(false);
            TMP_Text gameOverScoreText = gameOverScore.GetComponent<TMP_Text>();
            gameOverScoreText.text = string.Format("Score: {0:D8}", _score);
            gameEnding = false;
            isGameOver = true;
        }
    }

    public void AddScore(int score)
    {
        _score += score;
        scoreText.SetText(_score.ToString("D8"));
        multiballMeter.Value += score;
    }

    public void AddScore(int score, Vector3 position)
    {
        AddScore(score);
        CreateScorePopup(position, score);

    }

    public int GetScore()
    {
        return _score;
    }

    public void CreateScorePopup(Vector3 worldPos, int score)
    {
        StartCoroutine(_CreateScorePopup(worldPos, score));
    }
    IEnumerator _CreateScorePopup(Vector3 worldPos, int score)
    {
        GameObject popup = Instantiate(popupPrefab, screenCanvas.transform);
        popup.transform.position = worldPos;
        TMP_Text text = popup.GetComponent<TMP_Text>();
        text.text = score.ToString();

        yield return new WaitForSeconds(popupTime);
        float startTime = Time.time;
        float startAlpha = text.color.a;
        while(Time.time - startTime < popupFade)
        {
            float fadeProgress = 1 - ((Time.time - startTime) / popupFade);
            text.color = new Color(text.color.r, text.color.g, text.color.b, fadeProgress * startAlpha);
            yield return null;
        }
        Destroy(popup);
    }

    public void GoToTitleMenu()
    {
        SceneManager.LoadScene("TitleScene");
    }
}
