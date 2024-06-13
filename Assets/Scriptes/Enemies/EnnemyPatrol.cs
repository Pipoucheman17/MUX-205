using UnityEngine;

public class EnnemyPatrol : MonoBehaviour
{
    public float speed;
    public Transform[] wayPoints;

    public SpriteRenderer graphics;
    private Transform target;
    private int destPoint;

    public int damageOnCollision = 1;
    
    void Start()
    {
        target = wayPoints[0];
    }

    void Update()
    {
        Vector3 dir = target.position - transform.position;
        transform.Translate(dir.normalized * speed * Time.deltaTime, Space.World);


        //Si l'ennemie est presque arrivé à sa destination
       // Debug.Log(Vector3.Distance(transform.position, target.position));
        if (Vector3.Distance(transform.position, target.position) < 0.3f)
        {
            
            destPoint = (destPoint + 1) % wayPoints.Length;
            target = wayPoints[destPoint];
            graphics.flipX=!graphics.flipX;
        }
    }

    private void OnCollisionEnter2D(Collision2D _collison)
    {
        if(_collison.transform.CompareTag("Player"))
        {
            Player player = _collison.transform.GetComponent<Player>(); 
            player.TakeDamage(damageOnCollision);
        }

    }
    
}
