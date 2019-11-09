using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MouseLook : MonoBehaviour {
    [SerializeField] float XSensitivity = 2f;
	[SerializeField] float YSensitivity = 2f;
	[SerializeField] bool clampVerticalRotation = true;
	[SerializeField] float MinimumX = -90F;
	[SerializeField] float MaximumX = 90F;
	[SerializeField] bool smooth;
	[SerializeField] float smoothTime = 5f;

    [SerializeField] bool useWorldMachine = true;

    Quaternion m_CharacterTargetRot;
	Quaternion m_CameraTargetRot;
	Transform character;
	public Transform cameraTransform {
		get {
			if (useWorldMachine) {
				return WorldMachine.World.CurrentCameraTransform;
			}
			else {
				return Camera.main.transform;
			}
		}
	}

    void Awake() {
        Cursor.visible = false;
        Cursor.lockState = CursorLockMode.Locked;
        character = gameObject.transform;

		// get the location rotation of the character and the camera
		m_CharacterTargetRot = character.localRotation;
		m_CameraTargetRot = cameraTransform.localRotation;
    }

    // Update is called once per frame
    void Update(){
        //get the y and x rotation based on the Input manager
		float yRot = Input.GetAxis("Mouse X") * XSensitivity;
		float xRot = Input.GetAxis("Mouse Y") * YSensitivity;

		// calculate the rotation
		m_CharacterTargetRot *= Quaternion.Euler (0, yRot, 0f);
		m_CameraTargetRot *= Quaternion.Euler (-xRot, 0f, 0f);

		// clamp the vertical rotation if specified
		if(clampVerticalRotation)
			m_CameraTargetRot = ClampRotationAroundXAxis (m_CameraTargetRot);

		// update the character and camera based on calculations
		if(smooth) {
			character.localRotation = Quaternion.Slerp (character.localRotation, m_CharacterTargetRot,
			                                            smoothTime * Time.deltaTime);
			cameraTransform.localRotation = Quaternion.Slerp (cameraTransform.localRotation, m_CameraTargetRot,
			                                         smoothTime * Time.deltaTime);
		}
		else {
			character.localRotation = m_CharacterTargetRot;
			cameraTransform.localRotation = m_CameraTargetRot;
		}
    }

    Quaternion ClampRotationAroundXAxis(Quaternion q) {
		q.x /= q.w;
		q.y /= q.w;
		q.z /= q.w;
		q.w = 1.0f;
		
		float angleX = 2.0f * Mathf.Rad2Deg * Mathf.Atan (q.x);
		
		angleX = Mathf.Clamp (angleX, MinimumX, MaximumX);
		
		q.x = Mathf.Tan (0.5f * Mathf.Deg2Rad * angleX);
		
		return q;
    }
}
