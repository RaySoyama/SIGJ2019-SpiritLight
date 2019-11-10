using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Controller : MonoBehaviour
{
    [SerializeField] float speed = 5f;
    public bool canMove = true;
    CharacterController controller;

    Vector3 movement = Vector3.zero;

    void Awake() {
        controller = GetComponent<CharacterController>();
    }

    // Update is called once per frame
    void Update() {

        if (!canMove) {
            return;
        }


        Vector3 movementX = Input.GetAxis("Horizontal") * Vector3.right;
        Vector3 movementZ = Input.GetAxis("Vertical") * Vector3.forward;

        Vector3 movementXZ = transform.TransformDirection(movementX + movementZ);

        if (movementXZ.magnitude > 1) {
            movementXZ.Normalize();
        }

        movementXZ *= speed;

        movement.x = movementXZ.x;
        movement.z = movementXZ.z;
        
        controller.Move(movement * Time.deltaTime);
    }

    private void OnTriggerEnter(Collider other)
    {
        // If this is not the player currently being controlled, do nothing
        if (WorldMachine.World.CurrentCameraTransform.tag != transform.tag) {
            return;
        }

        if (other.CompareTag("EventTrigger"))
        {
            WorldMachine.World.OnEventTriggerEnter(other.gameObject);
        }
        else if (transform.tag == "RealityCharacter" && other.CompareTag("Tar")) {
            WorldMachine.World.OnTarTrigger(other.gameObject);
        }
    }
}
