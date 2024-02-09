using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyController : MonoBehaviour
{
    [SerializeField] float moveSpeed = 1f;
    Rigidbody2D myR2d;

    // Start is called before the first frame update
    void Start()
    {
        myR2d = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        myR2d.velocity = new Vector2(moveSpeed, 0f);
    }

    
    private void OnTriggerExit2D(Collider2D collision)
    {
        //Change move speed to the opposite
        moveSpeed = -moveSpeed;
        FlipEnemy();
    }


    private void FlipEnemy()
    {
        //Switch movement around on x axis
        float xScale = -transform.localScale.x;

        transform.localScale = new Vector2(xScale, transform.localScale.y);

    }
}
