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
    public float moveSpeed = 7;
    public float turnSpeed = 150;

    [HideInInspector]
    public bool canMove = true;

    private void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
        characterController = GetComponent<CharacterController>();
        animator = GetComponentInChildren<Animator>();
    }


    void Update()
    {
        if (canMove)
        {
            Move();
        }
        Controls();
    }

    private void Move()
    {
        var horizontal = Input.GetAxis("Horizontal");
        var vertical = Input.GetAxis("Vertical");

        var movement = new Vector3(horizontal, 0, vertical);
        animator.SetFloat("Speed", vertical);

        transform.Rotate(Vector3.up, horizontal * turnSpeed * Time.deltaTime);
        characterController.SimpleMove(transform.forward * moveSpeed * vertical);
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

        if(Input.GetKeyDown(KeyCode.Z))
        {
            inventory.UseQuickInventory(0);
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


