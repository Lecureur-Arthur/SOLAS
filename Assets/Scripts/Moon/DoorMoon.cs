using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoorMoon : MonoBehaviour
{
    public AudioClip OpenDoor;
    public AudioClip CloseDoor;

    private AudioSource audioSource;
    private Animator anim;

    // Start is called before the first frame update
    void Start()
    {
        anim = GetComponentInParent<Animator>();
        audioSource = GetComponent<AudioSource>();
        Debug.Log("Start DoorTrigger");
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            Debug.Log("collision white player");
            anim.SetBool("isOpen", true);
            audioSource.PlayOneShot(OpenDoor);
        }
    }

    void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            Debug.Log("Player exit");
            anim.SetBool("isOpen", false);
            audioSource.PlayOneShot(CloseDoor);
        }
    }
}
