﻿using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;

[RequireComponent(typeof(CharacterController))]
public class PlayerMovement : MonoBehaviour
{

    public float speed = 6.0F;
    public float jumpSpeed;
    public float gravity = 20.0F;
    public float rotateSpeed = 6;
    private Vector3 moveDirection = Vector3.zero;
    public GameObject cameraObject;
	MouseLook mouseLook;
    CharacterController controller;

    void Start()
    {
		mouseLook = cameraObject.GetComponent<MouseLook> ();
        controller = GetComponent<CharacterController>();
    }
    void Update()
    {

		transform.rotation = Quaternion.Euler(0, mouseLook.getY(), 0);

        if (controller.isGrounded)
        {
            moveDirection = new Vector3(Input.GetAxis("Horizontal"), 0, Input.GetAxis("Vertical"));
            moveDirection = transform.TransformDirection(moveDirection);
			moveDirection = moveDirection.normalized;
            moveDirection *= speed;

            if (Input.GetButton("Jump"))
                moveDirection.y = jumpSpeed;

        }
		if (Input.GetButton ("Cancel")) {
			SceneManager.LoadScene ("Menu");
		}
        moveDirection.y -= gravity * Time.deltaTime;
        controller.Move(moveDirection * Time.deltaTime);
    }
}
