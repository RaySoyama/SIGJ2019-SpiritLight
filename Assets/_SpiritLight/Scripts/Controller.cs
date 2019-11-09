using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Controller : MonoBehaviour
{
    [SerializeField] float speed = 5f;
    CharacterController controller;

    Vector3 movement = Vector3.zero;

    void Awake() {
        controller = GetComponent<CharacterController>();
    }

    // Update is called once per frame
    void Update() {
        Vector3 movementX = Input.GetAxis("Horizontal") * Vector3.right;
        Vector3 movementZ = Input.GetAxis("Vertical") * Vector3.forward;

        Vector3 movementXZ = movementX + movementZ;

        if (movementXZ.magnitude > 1) {
            movementXZ.Normalize();
        }

        movementXZ *= speed;

        movement.x = movementXZ.x;
        movement.z = movementXZ.z;
        
        controller.Move(movement * Time.deltaTime);
    }
}
