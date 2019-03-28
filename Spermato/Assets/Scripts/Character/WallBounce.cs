using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WallBounce : MonoBehaviour {

    private float pushForce = 10.0f;
    private Rigidbody2D rb;

	// Use this for initialization
	void Start ()
    {
        rb = GetComponent<Rigidbody2D>();
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Left_Wall")
        {
            Debug.Log("pushRight");
            rb.AddForce(Vector2.right * pushForce, ForceMode2D.Impulse);
        }
        else if (collision.gameObject.tag == "Right_Wall")
        {
            Debug.Log("pushLeft");
            rb.AddForce(Vector2.left * pushForce, ForceMode2D.Impulse);
        }
    }
}
