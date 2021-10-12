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

    public bool showLight;
    bool lightInput;

    public SpriteRenderer itemHolder;
    public Item itemScript;

    public bool insideVendorRange;
    public VendorBehaviour whichVendor;

    public bool insideContainerRange;
    public ContainerBehaviour WhichContainer;

    public GeneralSounds generalSounds;
    private Stats playerStats;

    public float staminaDepleteTime;
    public float staminaRegenTime;

    void Start()
    {
        playerStats = GetComponent<Stats>();
        spriteRenderer = GetComponent<SpriteRenderer>();
    }

    void Update()
    {
        horizontalInput = Input.GetAxisRaw("Horizontal");
        verticalInput = Input.GetAxisRaw("Vertical");
        runInput = Input.GetButton("Run");
        lightInput = Input.GetButtonDown("Jump");
        interactInput = Input.GetButtonDown("Interact");

        if(lightInput)
        {
            showLight = !showLight;
            transform.GetChild(1).gameObject.SetActive(!showLight);
            transform.GetChild(2).gameObject.SetActive(showLight);
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
            float staminaDecrease = Time.deltaTime / staminaDepleteTime;
            Debug.Log(staminaDecrease);
            playerStats.stamina -= staminaDecrease;

            if(playerStats.stamina > 0)
            {
                GetComponent<CharacterController>().Move(velocity * speed * runSpeed * Time.deltaTime);
            } else
            {
                GetComponent<CharacterController>().Move(velocity.normalized * speed * Time.deltaTime);
            }
        }
        else
        {
            GetComponent<CharacterController>().Move(velocity.normalized * speed * Time.deltaTime);
            float staminaIncrease = Time.deltaTime / staminaRegenTime;
            playerStats.stamina += staminaIncrease;
        }

        playerStats.stamina = Mathf.Clamp01(playerStats.stamina);
        playerStats.UpdateStaminaUI();

        if(interactInput)
        {
            if(insideVendorRange)
            {
                generalSounds.PlayItemGrabbed();
                itemHolder.sprite = whichVendor.item.sprite;
                itemScript.whichItemShouldFill = whichVendor.item.whichItemShouldFill;
            } else if(insideContainerRange)
            {
                if(itemHolder.sprite != null)
                {
                    generalSounds.PlayItemDropped();
                    WhichContainer.items[itemScript.whichItemShouldFill] = true;
                    itemHolder.sprite = null;
                    Debug.Log("Sprite deleted");
                    WhichContainer.CheckForAllItems();
                }
            }
        }
    }


    private void OnTriggerEnter(Collider collision)
    {
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
