using System.Collections;
using TMPro;
using UnityEngine;

public class UiManager : MonoBehaviour
{
    // Start is called before the first frame update
    [SerializeField]
    private TextMeshProUGUI scoreText;
    [SerializeField]
    private Sprite[] livesSprite;
    [SerializeField]
    private UnityEngine.UI.Image liveimg;
    [SerializeField] private GameObject gameoverText;
    [SerializeField] private GameObject restartLevelText;
    GameController gameManager;
    [SerializeField] private TextMeshProUGUI _BestScoreText;
    void Start()
    {
        scoreText.text = "Score: " + 0;
        gameManager = FindObjectOfType<GameController>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void UpdateBestScore(int playerBestScore)
    {
        _BestScoreText.text = "Best: " + playerBestScore;
    }
    public void UpdateScore(int playerScore)
    {
        scoreText.text = "Score: " + playerScore;
    }
    public void UpdateLivePlayer(int currentPlayer)
    {
       liveimg.sprite = livesSprite[currentPlayer];
    }
    public void GameOverActive()
    {
        gameoverText.SetActive(true);
        StartCoroutine(RestartLevelActive());
    }
    IEnumerator RestartLevelActive()
    {
        yield return new WaitForSeconds(1f);
        restartLevelText.SetActive(true);
        gameManager.GameOver();
    }
}
