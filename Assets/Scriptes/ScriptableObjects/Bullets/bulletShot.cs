using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class bulletShot : MonoBehaviour
{
    private float speed = 5f;
    private Rigidbody2D RB;
    private Vector2 velocity;
    public int direction { get; private set; }
    // Update is called once per frame
    private void Start()
    {
        transform.position = GetComponentInParent<Transform>().position;
        velocity = new Vector2(speed, 0);
        RB = GetComponent<Rigidbody2D>();

    }
    private void Update()
    {
        RB.velocity = velocity*direction;
    }
    private void OnCollisionEnter2D(Collision2D _collison)
    {
        if (_collison.transform.CompareTag("Player"))
        {
            Debug.Log("Touche Player");
            Player player = _collison.transform.GetComponent<Player>();
            player.TakeDamage(2);
            Destroy(gameObject);
        }
        if(_collison.transform.CompareTag("Ground") || _collison.transform.CompareTag("Enemy"))
        {
            Destroy(gameObject);
        }

    }

    public void setDirection(int direction)
    {
        this.direction = direction;
    }

}
