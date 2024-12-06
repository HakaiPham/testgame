using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameController : MonoBehaviour
{
    // Start is called before the first frame update
    public GameObject enemy;
    float timeSpawn;
    [SerializeField] float timeSpawnCooldown;
    [SerializeField] private GameObject enemyContainer;
    //Tạo thùng chứa cho Enemy
    bool isPlayerDead;
    [SerializeField] private GameObject[] powerUps;
    [SerializeField] private bool isGameOver;
    UiManager uiManager;
    ControllPlayer playerScore;
    static int _BestScore;
    void Start()
    {
        isPlayerDead = false;
        uiManager = FindObjectOfType<UiManager>();
        playerScore = FindObjectOfType<ControllPlayer>();
        _BestScore = PlayerPrefs.GetInt("BestScore", 0);//Tải dữ liệu khi đã lưu
        //Nếu chưa có dữ liệu để lưu thì sẽ trả về 0
        uiManager.UpdateBestScore(_BestScore);
    }
    public void StartSpwan()
    {
        StartCoroutine(SpawnEnemy());
        StartCoroutine(SpawnPowerUp());
    }
    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.R) && isGameOver)
        {
            SceneManager.LoadScene(1);
            int _CurrentScore = 0;
            uiManager.UpdateScore(_CurrentScore);
        }
        else if (Input.GetKeyDown(KeyCode.Escape))
        {
            SceneManager.LoadScene(0);
        }
        BestScore();
    }
    public void BestScore()
    {
       int _CurrentScore = playerScore.CurrentScore();
        uiManager.UpdateScore(_CurrentScore);
        if (isPlayerDead)
        {
            if (_CurrentScore > _BestScore)
            {
                _BestScore = _CurrentScore;
                PlayerPrefs.SetInt("BestScore", _BestScore);//Lưu dữ liệu
            }
        }
        uiManager.UpdateBestScore(_BestScore);
    }
    IEnumerator SpawnEnemy()
    {
        while (!isPlayerDead)
        {
            float spawnPos = Random.Range(-7.51f, 7.39f);
            GameObject newEnemy = Instantiate(enemy, new Vector2(spawnPos, 6.16f), Quaternion.identity);
            newEnemy.transform.parent = enemyContainer.transform;
            //Đặt newEnemy thành đối tượng con của enemyContainer
            //có tác dụng dễ quản lý
            yield return new WaitForSeconds(timeSpawnCooldown);
        }
    }
    IEnumerator SpawnPowerUp()
    {
        while (!isPlayerDead)
        {
            yield return new WaitForSeconds(Random.Range(3, 11));
            float spawnPos = Random.Range(-9, 8.8f);
            int randomPowerup = Random.Range(0, 3);
            Instantiate(powerUps[randomPowerup], new Vector2(spawnPos, 7.3f), Quaternion.identity);
        }
    }
    public void IsPlayerDead()
    {
        isPlayerDead = true;
    }
    public void GameOver()
    {
        isGameOver = true;
    }
}
