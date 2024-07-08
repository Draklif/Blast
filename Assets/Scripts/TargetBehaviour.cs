using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TargetBehaviour : MonoBehaviour
{
    [SerializeField] float speed = 10f;
    [SerializeField] float size = 1f;
    [SerializeField] bool isLeftToRight = true;
    [SerializeField] int score = 20;

    private GameObject player;
    private Rigidbody rb;
    private Vector3 dir;
    private bool isDestroyed = false;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
        player = GameObject.FindGameObjectWithTag("Player");
    }

    void Update()
    {
        // Calcula si debe moverse en lateral o hacia el frente y hacia atrás
        dir = isLeftToRight ? 
            new Vector3(Mathf.Cos(Time.time * speed) * size, Mathf.Sin(Time.time * speed) * size) : 
            new Vector3(0, Mathf.Sin(Time.time * speed) * size, Mathf.Cos(Time.time * speed) * size);
        rb.velocity = dir;
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.transform.tag.Equals("Bullet") && !isDestroyed) {
            player.GetComponent<CanvasController>().AddScore(score);
            isDestroyed = true; // Para que la colisión no se evalue dos veces, se valida esta flag
            Destroy(gameObject);
        }
    }
}
