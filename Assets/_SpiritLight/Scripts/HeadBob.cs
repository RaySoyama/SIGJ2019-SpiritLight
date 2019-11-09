using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HeadBob : MonoBehaviour {

	[SerializeField] float bobbingSpeed = 0.18f;
	[SerializeField] float bobbingAmount = 0.2f;
	float midpoint;
	[SerializeField] float bobTimer = 0.0f;

    Transform CameraTransform {
        get {
            return WorldMachine.World.CurrentCameraTransform;
        }
    }

    void Awake() {
        midpoint = CameraTransform.localPosition.y;
    }

    void Update() {
        Debug.Log(CameraTransform.parent.name);
		float waveslice = 0.0f; 
		float horizontal = Input.GetAxis("Horizontal"); 
		float vertical = Input.GetAxis("Vertical"); 
		
        if (Mathf.Abs(horizontal) == 0 && Mathf.Abs(vertical) == 0) { 
			bobTimer = 0.0f; 
		} 
		
        else { 
			waveslice = Mathf.Sin(bobTimer); 
			bobTimer = bobTimer + bobbingSpeed;

			if (bobTimer > Mathf.PI * 2) { 
				bobTimer = bobTimer - (Mathf.PI * 2); 
			} 
		} 

        if (waveslice != 0) { 
			float translateChange = waveslice * bobbingAmount; 
			float totalAxes = Mathf.Abs(horizontal) + Mathf.Abs(vertical); 
			totalAxes = Mathf.Clamp (totalAxes, 0.0f, 1.0f); 
			translateChange = totalAxes * translateChange; 
			CameraTransform.localPosition = new Vector3(CameraTransform.localPosition.x, 
														midpoint + translateChange, 
														CameraTransform.localPosition.z);
		} 
		
        else { 
			CameraTransform.localPosition = new Vector3(CameraTransform.localPosition.x,
														midpoint,
														CameraTransform.localPosition.z);
		} 
    }
}