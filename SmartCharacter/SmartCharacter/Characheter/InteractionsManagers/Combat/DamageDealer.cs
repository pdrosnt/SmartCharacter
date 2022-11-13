using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DamageDealer : MonoBehaviour
{
    public Collider col;

    public List<GameObject> colliding;

    private void Awake() {
        
        if(col == null){col = gameObject.AddComponent<CapsuleCollider>();}
        col.isTrigger = true;

    }

    private void OnTriggerEnter(Collider other) {
        
        colliding.Add(other.gameObject);

    }

    private void OnTriggerExit(Collider other) {
        
        colliding.Remove(other.gameObject);

    }
    
}
