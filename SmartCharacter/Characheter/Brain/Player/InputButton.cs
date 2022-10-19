using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class InputButton 
{
    public bool pressed;
    public bool hold;
    public bool release;
    public float timer;

    public List<CharacterBehaviourObject> triggeredBehaviours;


    public void UpdateButton(bool _holdindg) {

        hold = _holdindg;

        if(hold){

            if(timer == 0){pressed = true;}else{pressed = false;}

            timer += Time.deltaTime;
        
        }else{
            
            if(timer > 0){release = true;}else{release = false;}

            timer = 0f;
        }
        
    }
}
