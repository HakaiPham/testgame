using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PowerUp : MonoBehaviour
{
    // Start is called before the first frame update
    [SerializeField] private float speed = 3.0f;
    ControllPlayer player;
    [SerializeField] private int powerUpId;
    [SerializeField] private AudioClip powerUpSound;
    void Start()
    {
        player = FindObjectOfType<ControllPlayer>();
    }

    // Update is called once per frame
    void Update()
    {
        transform.Translate(Vector2.down * speed*Time.deltaTime);
        if (transform.position.y <= -6.8f) Destroy(gameObject);
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Player")
        {
            AudioSource.PlayClipAtPoint(powerUpSound,transform.position);
            switch(powerUpId)
            {
                case 0: player.TripleShootActive();break;
                case 1: player.SpeedPowerActive();break;
                case 2: player.ShieldPowerActive();break;
            }
            Destroy(gameObject);
        }
    }
}
