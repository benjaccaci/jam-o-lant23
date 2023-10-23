using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyScript : MonoBehaviour
{
    [SerializeField]
    private float moveSpeed;
    private Rigidbody2D rb;
    private float previousXVel = 0;
    private float previousYVel = 0;
    public GameObject player;
    public GameObject flashlightLink;
    //local value of rotation in radians
    public float angle = 0;

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
        transform.rotation = Quaternion.Euler(0,0,angle*180/Mathf.PI);
    }
    void OnCollisionEnter2D(Collision2D coll)
    {
        if (coll.gameObject == flashlightLink)
        {
            Debug.Log("Hit by Light");
            angle += Mathf.PI;
            rb.AddForce(new Vector2(rb.velocity.x * -2, rb.velocity.y * -2), ForceMode2D.Impulse);
        }
        else if(Vector3.Distance(player.transform.position, transform.position) < attackDistanceTrigger)
        {
            rb.AddForce(new Vector2(rb.velocity.x * -1, rb.velocity.y * -1), ForceMode2D.Impulse);
            angle = Mathf.Atan2((player.transform.position.y - transform.position.y), (player.transform.position.x - transform.position.x));
            Debug.Log("I'm chasing the player");
        }
        else
        {
            Debug.Log("I'm looking around.");
            rb.AddForce(new Vector2(rb.velocity.x / -2, rb.velocity.y / -2), ForceMode2D.Impulse);
            angle = Mathf.Atan2(rb.velocity.y, rb.velocity.x);
        }
    }
}