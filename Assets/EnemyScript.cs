using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyScript : MonoBehaviour
{
    [SerializeField]
    private float moveSpeed;
    private Rigidbody2D rb;
    public float angle = Mathf.PI / 3;
    private float previousXVel = 0;
    private float previousYVel = 0;
    public GameObject player;
    public GameObject flashlightLink;

    //distance between the player and enemy at which the enemy would charge directly at the player;
    [SerializeField]
    private float attackDistanceTrigger = 10;
    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        Movements();
    }
    void Movements()
    {
        float xSpeed = Time.deltaTime * moveSpeed * Mathf.Cos(angle);
        float ySpeed = Time.deltaTime * moveSpeed * Mathf.Sin(angle);
        Vector2 moveDirection = new Vector2(xSpeed, ySpeed);
        rb.AddForce(moveDirection, ForceMode2D.Impulse);
        previousXVel = rb.velocity.x;
        previousYVel = rb.velocity.y;
    }
    void OnCollisionEnter2D(Collision2D coll)
    {
        if (Vector3.Distance(player.transform.position, transform.position) < attackDistanceTrigger)
        {
            rb.AddForce(new Vector2(rb.velocity.x * -1, rb.velocity.y * -1), ForceMode2D.Impulse);
            angle = Mathf.Atan2((player.transform.position.y - transform.position.y), (player.transform.position.x - transform.position.x));
            Debug.Log("I'm chasing the player");
        }
        else if (coll.gameObject == flashlightLink) 
        {
            Debug.Log("here");
            angle += Mathf.PI;
            rb.AddForce(new Vector2(rb.velocity.x * -10, rb.velocity.y * -10), ForceMode2D.Impulse);
        }
        else
        {
            Debug.Log("I'm looking around.");
            //Changed angle for enemyMovement
            float deltaAngle = 0;
            if (System.Math.Abs(rb.velocity.x) < 0.001)
            {
                if (previousXVel < 0)
                {
                    deltaAngle = Mathf.PI / -2;
                }
                else
                {
                    deltaAngle = Mathf.PI / 2;
                }

                if (rb.velocity.y < 0)
                {
                    deltaAngle *= -1;
                }
                rb.AddForce(new Vector2(0, rb.velocity.y * -1), ForceMode2D.Impulse);
            }
            else if (System.Math.Abs(rb.velocity.y) < 0.001)
            {
                if (previousYVel < 0)
                {
                    deltaAngle = Mathf.PI / -2;
                }
                else
                {
                    deltaAngle = Mathf.PI / 2;
                }
                if (rb.velocity.x > 0)
                {
                    deltaAngle *= -1;
                }
                rb.AddForce(new Vector2(rb.velocity.x * -1, 0), ForceMode2D.Impulse);
            }
            angle += deltaAngle;
        }
    }
}