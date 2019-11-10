using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CandleDetection : MonoBehaviour {

    
    [SerializeField] float candleHitRange = 2;
    [SerializeField] bool drawRaycast = false;
    LayerMask candleMask;

    void Awake() {
        candleMask = LayerMask.GetMask("Candle");
    }
    void Update() {
        // If this is not the player currently being controlled, do nothing
        if (WorldMachine.World.CurrentCameraTransform.tag != transform.tag) {
            return;
        }

        if (Input.GetMouseButtonDown(0)) {
            BreathManager.Breath.Blow();

            RaycastHit hit;
            bool didHit = Physics.Raycast(transform.position, transform.forward, out hit, candleHitRange, candleMask);
            if (drawRaycast) {
                Debug.DrawRay(transform.position, transform.forward * candleHitRange, Color.green);
            }
            if (didHit) {
                Candle candle = hit.collider.GetComponent<Candle>();

                if (candle != null) {
                    candle.Extinguish();
                }
            }
        }
        
    }
}
