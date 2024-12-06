using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class ControllPlayer : MonoBehaviour
{
    // Start is called before the first frame update
    public float speed;
    public float right;
    public float left;
    public float up;
    public float down;
    public GameObject bullet;
    public Transform posShooter;
    [SerializeField] float fireCooldown;
    float fireTime;
    [SerializeField]
    private int playerLive = 3;
    bool isDead = false;
    GameController gameController;
    [SerializeField] GameObject tripleShoot;
    bool isTripleShootActive = false;
    float tripleShootTime;
    bool isSpeedPowerActive = false;
    [SerializeField] private float SpeedPlayerPower;
    [SerializeField] private GameObject shieldPowerUp;
    bool isShieldActive = false;
    [SerializeField] private  int _score;
    UiManager uiManager;
    [SerializeField] private GameObject right_Engine;
    [SerializeField] private GameObject left_Engine;
    bool isLeftEngineActive;
    bool isRightEngineActive;
    AudioSource audioSource;
    [SerializeField] private AudioClip fireSound;
    [SerializeField] private AudioClip explosionSound;
    private bool isPlayerLazer;
    void Start()
    {
        transform.position = Vector3.zero;
        gameController = GameObject.Find("GameController").GetComponent<GameController>();
        uiManager = FindObjectOfType<UiManager>();
        if(gameController == null)
        {
            Debug.LogError("GameController is Null.");
        }
        if (uiManager == null)
        {
            Debug.LogError("UiManager is Null.");
        }
        audioSource = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
        MovePlayer();
        PlayerShooter();
        if (playerLive <= 0 && isDead == false)
        {
            gameController.IsPlayerDead();
            isDead = true;
            Debug.Log("Player Dead");
            uiManager.GameOverActive();
            Destroy(gameObject);
        }
    }
    public void MovePlayer()
    {
        var horizontalInput = Input.GetAxis("Horizontal");
        var verticalInput = Input.GetAxis("Vertical");
        Vector3 playerTransform = transform.position;
        transform.position = playerTransform;
        if (playerTransform.x >= right && horizontalInput > 0 || playerTransform.x <= left && horizontalInput < 0
            || playerTransform.y >= up && verticalInput > 0 || playerTransform.y <= down && verticalInput < 0) return;
        if (isSpeedPowerActive)
        {
            transform.position += new Vector3(horizontalInput, verticalInput, 0) * speed 
                *SpeedPlayerPower * Time.deltaTime;
        }
        else
        {
            transform.position += new Vector3(horizontalInput, verticalInput, 0) 
                * speed * Time.deltaTime;
        }
    }
    public void PlayerShooter()
    {
        if (Input.GetKeyDown(KeyCode.Space)) {
            StartCoroutine(LazerPlayer());
        }
    }
    private void OnTriggerEnter2D(Collider2D other)
    {
        if(other.tag == "Enemy"||other.tag == "Astorid")
        {
            if (isShieldActive)
            {
                shieldPowerUp.SetActive(false);
                return;
            }
            else
            {
                PlayerTakeDame();
            }
        }
    }
    public void TripleShootActive()
    {
        isTripleShootActive = true;
        StartCoroutine(TimeTripleshootActive());
    }
    IEnumerator TimeTripleshootActive()
    {
        yield return new WaitForSeconds(5f);
        isTripleShootActive = false;
    }
    public void SpeedPowerActive()
    {
        isSpeedPowerActive = true;
        StartCoroutine(TimeSpeedPowerActive());
    }
    IEnumerator TimeSpeedPowerActive()
    {
        yield return new WaitForSeconds(5f);
        isSpeedPowerActive = false;
    }
    public void ShieldPowerActive()
    {
        isShieldActive = true;
        shieldPowerUp.SetActive(true);
        StartCoroutine(TimeShieldPowerActive());
    }
    IEnumerator TimeShieldPowerActive()
    {
        yield return new WaitForSeconds(5f);
        isShieldActive = false;
        shieldPowerUp.SetActive(false);
    }
    public void Score(int point)
    {
        _score += point;
        uiManager.UpdateScore(_score);
    }
    public void PlayerTakeDame()
    {
        playerLive -= 1;
        audioSource.PlayOneShot(explosionSound);
        if (playerLive == 2)
        {
            int randomEngine = Random.Range(0, 2);
            if (randomEngine == 0)
            {
                isRightEngineActive = true;
                right_Engine.SetActive(true);
            }
            else
            {
                isLeftEngineActive = true;
                left_Engine.SetActive(true);
            }
        }
        else if (isRightEngineActive && playerLive == 1)
        {
            left_Engine.SetActive(true);
        }
        else if (isLeftEngineActive && playerLive == 1)
        {
            right_Engine.SetActive(true);
        }

        uiManager.UpdateLivePlayer(playerLive);
    }
    public bool IsShieldActive()
    {
        return isShieldActive;
    }
    public void OffShieldPowerUp()
    {
        shieldPowerUp.SetActive(false);
    }
    public bool isPlayerLazerActive()
    {
        return isPlayerLazer;
    }
    IEnumerator LazerPlayer()
    {
        if (isTripleShootActive == true)
        {
            if (tripleShootTime < Time.time)
            {
                audioSource.PlayOneShot(fireSound);
                tripleShootTime = Time.time + fireCooldown;
                GameObject newtripleShoot = Instantiate(tripleShoot,
                transform.position, Quaternion.identity);
                yield return new WaitForSeconds(0.01f);
                Destroy(newtripleShoot, 2f);
            }
        }
        else if (fireTime < Time.time && isTripleShootActive == false)
        {
            audioSource.PlayOneShot(fireSound);
            fireTime = Time.time + fireCooldown;
            GameObject createBullet = Instantiate(bullet, posShooter.position, Quaternion.identity);
            yield return new WaitForSeconds(0.01f);
            Destroy(createBullet, 2f);
        }
    }
    public int CurrentScore()
    {
        return _score;
    }
}   
