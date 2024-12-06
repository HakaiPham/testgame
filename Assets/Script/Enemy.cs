using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    // Start is called before the first frame update
    [SerializeField] float speed;
    ControllPlayer player;
    Animator animator;
    AudioSource audioSource;
    [SerializeField] private AudioClip explosionSound;
    BoxCollider2D boxCollider;
    [SerializeField] private GameObject _LazerPrefapt;
    private float _fireRate;
    private float _Canfire;
    [SerializeField] private GameObject _PosFire;
    [SerializeField] private GameObject _ContainerEnemy;
    Lazer lazer;
    void Start()
    {
        animator = GetComponent<Animator>();
        if(animator == null)
        {
            Debug.LogError("Animator is Null.");
        }
        audioSource = GetComponent<AudioSource>();
        boxCollider = GetComponent<BoxCollider2D>();
        player = FindObjectOfType<ControllPlayer>();
        lazer = FindObjectOfType<Lazer>();
    }

    // Update is called once per frame
    void Update()
    {
        EnemyMove();
        EnemyFire();
    }
    public void EnemyFire()
    {
        if(Time.time >= _fireRate)
        {
            _Canfire = Random.Range(3f, 8f);
            _fireRate = Time.time + _Canfire;
            //Debug.Break() Debug này có tác dụng sau khi chạy xong các code có trong hàm
            //khi tới dòng này, game sẽ dừng lại điều này thuận lợi cho việc Debug
            GameObject lazers = Instantiate(_LazerPrefapt, transform.position, Quaternion.identity);
            //lazers.transform.parent = transform;
            Lazer[] listLazer = lazers.GetComponentsInChildren<Lazer>();
            for(int i = 0; i < listLazer.Length; i++)
            {
                listLazer[i].AssignEnemyLazer();
            }
        }
    }
    public void EnemyMove()
    {
        transform.Translate(Vector3.down * speed * Time.deltaTime);
        if(transform.position.y <= -6.8f)
        {
            Destroy(gameObject);
        }
    }
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            boxCollider.enabled = false;
            audioSource.PlayOneShot(explosionSound);
            animator.SetTrigger("EnemyDestroy");
            Destroy(gameObject,1f);
        }
        if (other.tag == "LazerEnemy") return;
        else if (other.CompareTag("Bullet"))
        {
            boxCollider.enabled = false;
            audioSource.PlayOneShot(explosionSound);
            player.Score(10);
            speed = 0;
            animator.SetTrigger("EnemyDestroy");
            Destroy(other.gameObject);
            Destroy(gameObject, 1f);
        }
    }
    public void EnemyDestroy()
    {
        boxCollider.enabled = false;
        audioSource.PlayOneShot(explosionSound);
        player.Score(10);
        speed = 0;
        animator.SetTrigger("EnemyDestroy");
        Destroy(gameObject, 1f);
    }
}
