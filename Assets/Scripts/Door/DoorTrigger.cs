using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoorTrigger : MonoBehaviour
{

    private AudioSource audio;
    private Animator anim;

    // Start is called before the first frame update
    void Start()
    {
        anim = GetComponentInParent<Animator>();
        audio = GetComponent<AudioSource>();
        Debug.Log("Start DoorTrigger");
    }

    void OnTriggerEnter(Collider other)
    {
        if(other.CompareTag("Player")) {
            Debug.Log("collision white player");
            anim.SetBool("Character_nearby", true);
        }
    }

    void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            Debug.Log("Player exit");
            anim.SetBool("Character_nearby", false);
        }
    }
}
