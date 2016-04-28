using UnityEngine;
using System.Collections;

public class MouseLook : MonoBehaviour
{

    float lookSensitivity = 3;
    float yRotation;
    float xRotation;
    float currentXRotation;
    float currentYRotation;
    float yRotationV;
    float xRotationV;
    float lookSmoothness = 0.1f;

    // Use this for initialization
    void Start()
    {
		currentYRotation = transform.rotation.eulerAngles.y;
        yRotation = currentYRotation;
        xRotation = currentXRotation;
    }

	public float getY() {
		return currentYRotation;
	}

    // Update is called once per frame
    void Update()
    {
        yRotation += Input.GetAxis("Mouse X") * lookSensitivity;
        xRotation -= Input.GetAxis("Mouse Y") * lookSensitivity;

        xRotation = Mathf.Clamp(xRotation, -80, 100); //Locking how far you can look down and up

        currentXRotation = Mathf.SmoothDamp(currentXRotation, xRotation, ref xRotationV, lookSmoothness);
        currentYRotation = Mathf.SmoothDamp(currentYRotation, yRotation, ref yRotationV, lookSmoothness);

        transform.rotation = Quaternion.Euler(xRotation, yRotation, 0);
    }
}
