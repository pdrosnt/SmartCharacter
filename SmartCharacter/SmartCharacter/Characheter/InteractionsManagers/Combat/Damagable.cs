using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Damagable : MonoBehaviour
{
    public int Health = 10;
    

    public void SimpleDamage(int damage){

        Health -= damage;

        
    }
}
