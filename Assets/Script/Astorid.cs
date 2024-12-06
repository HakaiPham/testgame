using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Astorid : MonoBehaviour
{
    // Start is called before the first frame update
    [SerializeField] private float rotation_speed;
    [SerializeField] private float speed;
    Animator animator;
    GameController gameController;
    AudioSource audioSource;
    [SerializeField] private AudioClip explosionSound;
    CircleCollider2D circleCollider;
    void Start()
    {
        animator = GetComponent<Animator>();
        if(animator == null)
        {
            Debug.LogError("Animation is Null");
        }
        gameController = FindObjectOfType<GameController>();
        audioSource = GetComponent<AudioSource>();
        circleCollider = GetComponent<CircleCollider2D>();
    }

    // Update is called once per frame
    void Update()
    {
        transform.Rotate(Vector3.forward * rotation_speed*Time.deltaTime);
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.tag == "Bullet") {
            Destroy(collision.gameObject);
            circleCollider.enabled = false;
            audioSource.PlayOneShot(explosionSound);
            animator.SetTrigger("AstoridDestroy");
            gameController.StartSpwan();
            Destroy(gameObject, 1f);
        }
        if (collision.tag == "Player")
        {
            circleCollider.enabled = false;
            audioSource.PlayOneShot(explosionSound);
            animator.SetTrigger("AstoridDestroy");
            gameController.StartSpwan();
            Destroy(gameObject, 1f);
        }
    }
}
