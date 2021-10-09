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

    [SerializeField]
    bool interactInput;

    public Vector3 velocity;

    public float speed;
    public float runSpeed;

    private SpriteRenderer spriteRenderer;

    public bool light;
    bool lightInput;

    public SpriteRenderer itemHolder;

    public bool insideVendorRange;
    public VendorBehaviour whichVendor;

    public bool insideContainerRange;
    public ContainerBehaviour WhichContainer;

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
        lightInput = Input.GetButtonDown("Jump");
        interactInput = Input.GetButtonDown("Interact");

        if(lightInput)
        {
            light = !light;
            transform.GetChild(1).gameObject.SetActive(!light);
            transform.GetChild(2).gameObject.SetActive(light);
        }

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

        if(interactInput)
        {
            if(insideVendorRange)
            {
                itemHolder.sprite = whichVendor.whichItemToGive;
            } else if(insideContainerRange)
            {
                Debug.Log(itemHolder.sprite);
                if(itemHolder.sprite != null)
                {
                    itemHolder.sprite = null;
                    Debug.Log("Sprite deleted");
                }
            }
        }
    }


    private void OnTriggerEnter(Collider collision)
    {
        Debug.Log(collision.gameObject.name);

        if (collision.gameObject.CompareTag("Vendor"))
        {
            insideVendorRange = true;
            whichVendor = collision.gameObject.GetComponent<VendorBehaviour>();
        }
        else if (collision.gameObject.CompareTag("Container"))
        {
            WhichContainer = collision.gameObject.GetComponent<ContainerBehaviour>();
            insideContainerRange = true;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        Debug.Log(other.gameObject.name);

        if (other.gameObject.CompareTag("Vendor"))
        {
            whichVendor = null;
            insideVendorRange = false;
        } else if(other.gameObject.CompareTag("Container")) {
            WhichContainer = null;
            insideContainerRange = false;
        }
    }
}
