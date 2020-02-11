using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{

    private CharacterController characterController;
    private Animator animator;

    public float moveSpeed = 7;
    public float turnSpeed = 150;

    private void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
    }


    void Update()
    {

        characterController = GetComponent<CharacterController>();
        animator = GetComponentInChildren<Animator>();

        var horizontal = Input.GetAxis("Horizontal");
        var vertical = Input.GetAxis("Vertical");

        var movement = new Vector3(horizontal, 0, vertical);

        animator.SetFloat("Speed", vertical);

        transform.Rotate(Vector3.up, horizontal * turnSpeed * Time.deltaTime);

        
            characterController.SimpleMove(transform.forward * moveSpeed * vertical);
        
    }
}
