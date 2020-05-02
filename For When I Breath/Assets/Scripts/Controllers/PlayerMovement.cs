using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    // I'm just gonna shove all the other control stuff in here cause why not, not gonna change the name or anything cause that ruins everything 
    private CharacterController characterController;
    private Animator animator;

    [Header("Inventory System")]
    public InventoryManager inventory;
    public GameObject inventoryMenu;
    
    bool menuOpen;

    [Header("Move Stats")]
    public float walkSpeed = 7;
    public float runSpeed = 12;
    public float crouchSpeed = 3.5f;
    public float turnSpeed = 150;

    [Header("Jump Stuff")]
    public float jumpSpeed = 8;
    public float gravity = 20;

    [HideInInspector]
    public float moveSpeed;

    [HideInInspector]
    public bool canMove = true;
    public bool isRun = false;
    bool crouched = false;

    private void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
        characterController = GetComponent<CharacterController>();
        animator = GetComponentInChildren<Animator>();
        moveSpeed = walkSpeed;
    }


    void Update()
    {
        if (canMove)
        {
            Movement();
        }
        Controls();
    }

    private void Movement()
    {
        var horizontal = Input.GetAxis("Horizontal");
        var vertical = Input.GetAxis("Vertical");

        var movement = new Vector3(horizontal, 0, vertical);

        if (isRun)
        {
            animator.SetFloat("Run", 1);
        }
        else if (!isRun)
        {
            animator.SetFloat("Run", 0);
            animator.SetFloat("Speed", vertical);
        }


        if(Input.GetButtonDown("Jump"))
        {
            Debug.Log("Jump");
            movement.y = jumpSpeed;
        }

        //gravity
        movement.y -= gravity * Time.deltaTime;

        transform.Rotate(Vector3.up, horizontal * turnSpeed * Time.deltaTime);
        characterController.Move(transform.forward * moveSpeed * vertical * Time.deltaTime);
        //characterController.Move(movement * moveSpeed * Time.deltaTime); //This plays with the controls not being tank controls so maybe?
    }

    void Controls()
    {
        if (Input.GetButtonDown("Inventory"))
        {
            if (!menuOpen)
            {
                inventoryMenu.SetActive(true);
                canMove = false;
                menuOpen = true;
            }
            else
            {
                inventoryMenu.SetActive(false);
                canMove = true;
                menuOpen = false;
            }
        }

        if(Input.GetButtonDown("Interact"))
        {
            if(menuOpen)
            {
                if(inventory.inventoryMenu.active == true)
                {
                    inventory.UseItem();
                }
            }
        }

        if(Input.GetKeyDown(KeyCode.Z))
        {
            inventory.UseQuickInventory(0);
        }

        if(Input.GetButtonDown("Run"))
        {
            moveSpeed = runSpeed;
            isRun = true;
        }
        
        if(Input.GetButtonUp("Run"))
        {
            moveSpeed = walkSpeed;
            isRun = false;
        }

        if(Input.GetButtonDown("Crouch"))
        {
            moveSpeed = crouchSpeed;
            crouched = true;
        }
        
        if(Input.GetButtonUp("Crouch"))
        {
            moveSpeed = walkSpeed;
            crouched = false;
        }
    }

    private void OnTriggerStay(Collider other) //Meant for handling object pick ups
    {
        if (Input.GetButtonDown("Interact"))
        {
            switch (other.tag)
            {
                case "PickUp":
                    inventory.AddToList(other.GetComponent<ItemPickup>().PickUpItem());
                    Destroy(other.gameObject);
                    break;
            }
        }
    }
}


