using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMoveAlt : MonoBehaviour
{

    private Animator anim;

    private void Start()
    {;
        anim = GetComponentInChildren<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        anim.SetFloat("vertical", Input.GetAxis("Vertical"));
    }
}
