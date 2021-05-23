using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShowCharacter : MonoBehaviour
{
    [SerializeField]
   GameObject[] characters;

    int i = 0;

    private void Start() {
        characters[i].SetActive(true);    
        characters[i].GetComponent<Animator>().SetBool("Show",true);
    }
   
    public void CharacterLeft()
    {
        characters[i].GetComponent<Animator>().SetBool("Show",false);
         characters[i].SetActive(false);    
        i--;
        if(i <= 0)
        {
            i = 0;
             characters[i].SetActive(true);    
            characters[i].GetComponent<Animator>().SetBool("Show",true);
        }
        else
        {
          
             characters[i].SetActive(true);    
            characters[i].GetComponent<Animator>().SetBool("Show",true);
        }





       

    }

      public void CharacterRight()
    {
    
        characters[i].GetComponent<Animator>().SetBool("Show",false);
         characters[i].SetActive(false); 
        
        if(i == 7)
        {
            i = 7;


            
        }
        else
        {
            i++;
        }
    
           
        characters[i].SetActive(true);    
        characters[i].GetComponent<Animator>().SetBool("Show",true);
        Debug.Log("Es " + i);
        





       

    }
}
