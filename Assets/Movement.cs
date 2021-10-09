using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Movement : MonoBehaviour
{
    [SerializeField]
    float horizontalInput;

    [SerializeField]
    float verticalInput;

    [SerializeField]
    bool runInput;

    public Vector3 velocity;

    public float speed;
    public float runSpeed;

    private SpriteRenderer spriteRenderer;
    // Start is called before the first frame update
    void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
    }

    // Update is called once per frame
    void Update()
    {
        horizontalInput = Input.GetAxisRaw("Horizontal");
        verticalInput = Input.GetAxisRaw("Vertical");
        runInput = Input.GetButton("Run");
        

        if(horizontalInput == 1)
        {
            spriteRenderer.flipX = true;
        } else if(horizontalInput == -1)
        {
            spriteRenderer.flipX = false;
        }

        velocity.x = horizontalInput;
        velocity.y = verticalInput;

        if(runInput)
        {
            GetComponent<CharacterController>().Move(velocity * speed * runSpeed * Time.deltaTime);
            //transform.Translate(velocity.normalized * speed * runSpeed * Time.deltaTime);
        }
        else
        {
            GetComponent<CharacterController>().Move(velocity.normalized * speed * Time.deltaTime);
            //transform.Translate(velocity.normalized * speed * Time.deltaTime);
        }


    }
}
