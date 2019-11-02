using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Test : MonoBehaviour
{
    float speed = 10;
    float horizontalInput;
    bool verticalInput;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        verticalInput = Input.GetKey(KeyCode.Space);
        horizontalInput = Input.GetAxisRaw("Horizontal");
    }

    private void FixedUpdate()
    {
        float jumpForce = 0;
        if (verticalInput)
        {
            jumpForce = 3; 
        }
        Vector2 force = new Vector2(horizontalInput, jumpForce) * Time.deltaTime * speed;
        GetComponent<Rigidbody2D>().AddRelativeForce(force,ForceMode2D.Impulse);
    }
}
