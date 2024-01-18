using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PoolFloatObstacle : MonoBehaviour
{

    [SerializeField]
    private AudioClip triggerSound;

    [SerializeField]
    private float obstaclePower;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void playSound(AudioClip sound) {
        var audioSource = transform.gameObject.GetComponent<AudioSource>();
        audioSource.clip = sound;
        audioSource.Play();
    }


    private void OnCollisionEnter(Collision collision)
    {

    }
}
