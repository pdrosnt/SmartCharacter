using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ColisionSensor : MonoBehaviour
{
    public Collider colider;

    public List<Collision> colliding = new List<Collision>();

    public int contactCount = 0;

    public float minYdirection = .8f;

    public int i;

    private void Awake()
    {

        colider = GetComponent<Collider>();

    }

    private void Update() {
        
    i = 0;

    }

    private void OnCollisionStay(Collision other)
    {
        if(i == 0){
            colliding.Clear();
        }
        ContactPoint[] contacts = new ContactPoint[other.contactCount];

        colliding.Add(other);

        other.GetContacts(contacts);

        foreach (ContactPoint contact in contacts)
        {     
            Debug.DrawRay(contact.point, contact.normal * 10, Color.white);
        }

        i++;
    }   

}
