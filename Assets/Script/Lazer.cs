using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Lazer : MonoBehaviour
{
    // Start is called before the first frame update
    [SerializeField] float speed;
    private bool _IsLazerEnemy;
    ControllPlayer _player;
    private bool _IsPlayerLazer;
    Enemy enemy;
    void Start()
    {
        _player = FindObjectOfType<ControllPlayer>();
         enemy = FindObjectOfType<Enemy>();
    }

    // Update is called once per frame
    void Update()
    {
        if (!_IsLazerEnemy)
        {
            MoveUp();
        }
        else
        {
            MoveDown();
        }
    }
    public void MoveUp()
    {
        transform.Translate(Vector3.up * speed * Time.deltaTime);
    }
    public void MoveDown()
    {
        transform.Translate(Vector3.down * speed * Time.deltaTime);
        if(transform.position.y <= -6.77f) Destroy(gameObject);
    }
    private void OnTriggerEnter2D(Collider2D other)
    {
        if(other.tag == "Player" && _IsLazerEnemy)
        {
            bool isShieldActive = _player.IsShieldActive();
            if(isShieldActive) { _player.OffShieldPowerUp() ; return; }
            else if (!isShieldActive)
            {
                _player.PlayerTakeDame();
                Destroy(gameObject);
            }
        }
    }
    public void AssignEnemyLazer()
    {
        _IsLazerEnemy = true;
    }
}
