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
        if (Input.GetMouseButtonDown(0)) {
            RaycastHit hit;
            bool didHit = Physics.Raycast(transform.position, transform.forward, out hit, candleHitRange, candleMask);
            if (drawRaycast) {
                Debug.DrawRay(transform.position, transform.forward * candleHitRange, Color.green);
            }
            if (didHit) {
                Candle candle = hit.collider.GetComponent<Candle>();

                if (candle != null) {
                    if (WorldMachine.World.CurrentCameraTransform.tag == transform.tag) {
                        candle.Extinguish();
                    }
                }
            }
        }
        
    }
}
