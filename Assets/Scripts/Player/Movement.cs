using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Stats))]
[RequireComponent(typeof(CharacterController))]
public class Movement : MonoBehaviour
{
    private float horizontalInput;
    private float verticalInput;
    private bool runInput;
    private bool interactInput;

    private Vector3 velocity;

    public float speed;
    public float runSpeed;

    private SpriteRenderer spriteRenderer;

    private bool showCandle;
    private bool candleInput;

    public SpriteRenderer itemHolder;
    public Item itemScript;

    public bool insideVendorRange;
    public VendorBehaviour vendor;

    public bool insideContainerRange;
    public ContainerBehaviour container;

    public GeneralSounds generalSounds;
    private Stats playerStats;

    public float staminaDepleteTime;
    public float staminaRegenTime;

    public float candleDepleteTime;
    public float candleRegenTime;

    private CharacterController controller;

    public GameMaster gm;

    SpriteRenderer[] renderers;

    void Start()
    {
        controller = GetComponent<CharacterController>();
        playerStats = GetComponent<Stats>();
        renderers = GetComponentsInChildren<SpriteRenderer>();
    }

    void Update()
    {
        if(!gm.IsGamePaused())
        {
            GetAllInputs();
            HandleCandleInput();

            float lastCandleTime = playerStats.candleTime;
            if (showCandle && playerStats.candleTime > 0)
            {
                DecreaseCandleTime();
            }
            else
            {
                IncreaseCandleTime();
            }
            playerStats.candleTime = Mathf.Clamp01(playerStats.candleTime);
            playerStats.UpdateCandleUI();

            if (lastCandleTime > 0 && playerStats.candleTime == 0)
            {
                ToggleCandle();
            }

            HandleMovementInput();
            HandleRunInput();
            HandleInteractInput();
        }

    }

    private void HandleCandleInput()
    {
        if (candleInput)
        {
            ToggleCandle();
        }
    }

    private void ToggleCandle()
    {
        showCandle = !showCandle;
        transform.GetChild(1).gameObject.SetActive(!showCandle);
        transform.GetChild(2).gameObject.SetActive(showCandle);
    }

    private void DecreaseCandleTime()
    {
        float candleDecrease = Time.deltaTime / staminaDepleteTime;
        playerStats.candleTime -= candleDecrease;
    }

    private void IncreaseCandleTime()
    {
        float candleIncrease = Time.deltaTime / candleRegenTime;
        playerStats.candleTime += candleIncrease;
    }

    private void GetAllInputs()
    {
        horizontalInput = Input.GetAxisRaw("Horizontal");
        verticalInput = Input.GetAxisRaw("Vertical");
        runInput = Input.GetButton("Run");
        candleInput = Input.GetButtonDown("Jump");
        interactInput = Input.GetButtonDown("Interact");
    }

    private void HandleMovementInput()
    {
        Vector3 currentScale = gameObject.transform.localScale;
        if (horizontalInput == 1)
        {
            currentScale.x = -1;
            gameObject.transform.localScale = currentScale;
        }
        else if (horizontalInput == -1)
        {
            currentScale.x = 1;
            gameObject.transform.localScale = currentScale;
            
        }

        velocity.x = horizontalInput;
        velocity.y = verticalInput;
    }

    private void HandleRunInput()
    {

        if (runInput && playerStats.stamina > 0)
        {
            DecreaseStamina();
            MoveCharacter(true);
        }
        else
        {
            IncreaseStamina();
            MoveCharacter(false);
        }

        playerStats.stamina = Mathf.Clamp01(playerStats.stamina);
        playerStats.UpdateStaminaUI();
    }

    private void HandleInteractInput()
    {

        if (interactInput)
        {
            if (insideVendorRange)
            {
                GrabItemFromVendor();
            }
            else if (insideContainerRange && itemHolder.sprite != null)
            {
                DropItemIntoContainer();
            }
        }
    }

    private void GrabItemFromVendor()
    {
        generalSounds.PlayItemGrabbed();
        itemHolder.sprite = vendor.item.sprite;
        itemScript.whichItemShouldFill = vendor.item.whichItemShouldFill;
    }
    private void DropItemIntoContainer()
    {
        generalSounds.PlayItemDropped();
        container.items[itemScript.whichItemShouldFill] = true;
        itemHolder.sprite = null;
        container.CheckForAllItems();
    }
    private void DecreaseStamina()
    {
        float staminaDecrease = Time.deltaTime / staminaDepleteTime;
        playerStats.stamina -= staminaDecrease;
    }

    private void IncreaseStamina()
    {
        float staminaIncrease = Time.deltaTime / staminaRegenTime;
        playerStats.stamina += staminaIncrease;
    }

    private void MoveCharacter(bool isRunning)
    {
        if(isRunning)
        {
            controller.Move(velocity.normalized * speed * runSpeed * Time.deltaTime);
        }
        else
        {
            controller.Move(velocity.normalized * speed * Time.deltaTime);
        }
    }


    private void OnTriggerEnter(Collider collision)
    {
        if (collision.gameObject.CompareTag("Vendor"))
        {
            insideVendorRange = true;
            vendor = collision.gameObject.GetComponent<VendorBehaviour>();
        }
        else if (collision.gameObject.CompareTag("Container"))
        {
            container = collision.gameObject.GetComponent<ContainerBehaviour>();
            insideContainerRange = true;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        Debug.Log(other.gameObject.name);

        if (other.gameObject.CompareTag("Vendor"))
        {
            vendor = null;
            insideVendorRange = false;
        } else if(other.gameObject.CompareTag("Container")) {
            container = null;
            insideContainerRange = false;
        }
    }
}
