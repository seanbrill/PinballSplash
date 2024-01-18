using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class pinball : MonoBehaviour
{   
    [SerializeField]
    private GameObject gravityTarget;

    [SerializeField]
    private float gravityModifier = 50f;

    [SerializeField]
    private float poolRingPower;

    private AudioSource RollingAudio;
    public AudioClip ballRollingSound_1;
    public AudioClip bounceSound_1;
    public AudioClip bounceSound_2;
    private List<AudioClip> possibleSounds;

    private Vector3 startPosition;

    public static bool leftForceAdded = false;

    public static bool rightForceAdded = false;

    public GameObject uiObject;
    private UIManager ui;
    // Start is called before the first frame update
    void Start()
    {
        startPosition = transform.position;
        possibleSounds = new List<AudioClip>();
        RollingAudio = transform.gameObject.AddComponent<AudioSource>();
        possibleSounds.Add(bounceSound_1);
        //  possibleSounds.Add(bounceSound_2);

        ui = uiObject.GetComponent<UIManager>();
    }

    // Update is called once per frame
    void Update()
    {
        ballRollingSound();
        artificalGravity();
        resetBall();
        
    }


    void artificalGravity()
    {

      
        if (transform.gameObject.GetComponent<Rigidbody>().velocity.magnitude < gravityModifier)
        {
            Vector3 dir = transform.position - gravityTarget.transform.position;
            dir.x = 0;

            transform.gameObject.GetComponent<Rigidbody>().AddForce(-dir, ForceMode.Force);
        }
        

    }


    void resetBall()
    {

        if (transform.position.z <= gravityTarget.transform.position.z)
        {
            transform.position = startPosition;
            transform.gameObject.GetComponent<Rigidbody>().velocity = Vector3.zero;
            ui.Lives -= 1;
        }
            

        
        
    }

    void playBounceSound() {

        System.Random r = new System.Random();

        int randomIndex = r.Next(0, possibleSounds.Count);

        AudioClip randomSoundEffect = possibleSounds[randomIndex];

        AudioSource audio = transform.gameObject.GetComponent<AudioSource>();
        audio.loop = false;
        audio.clip = randomSoundEffect;
        audio.Play();
        
        


    }

    void ballRollingSound()
    {
            if (transform.GetComponent<Rigidbody>().velocity != Vector3.zero) {
                RollingAudio.loop = true;
                RollingAudio.clip = ballRollingSound_1;
                RollingAudio.volume = 0.3f;
                RollingAudio.Play();
        }
        else
        {
            RollingAudio.Stop();
        }
            
    }


    private void OnCollisionEnter(Collision collision)
    {


        if (collision.gameObject.tag == "launcher" && pinballLauncherController.shotReleased)
        {
            transform.gameObject.GetComponent<Rigidbody>().AddForce(- pinballLauncherController.instance.launcher.transform.position * pinballLauncherController.shotPower, ForceMode.Impulse);
            pinballLauncherController.shotPower = 0f;
        }
   

        if (collision.transform.tag == "leftFlipper" && flipperController.leftFlipperIsEngaged && !leftForceAdded)
        {
            Vector3 forceDirection = new Vector3(collision.transform.position.x, collision.transform.position.y, collision.transform.position.z);

            playBounceSound();

            transform.gameObject.GetComponent<Rigidbody>().AddForce( -forceDirection * flipperController.leftFlipperPower, ForceMode.Impulse);

            Debug.Log("Flipper Power: " + flipperController.leftFlipperPower);

            leftForceAdded = true;
        }

        if (collision.transform.tag == "rightFlipper" && flipperController.rightFlipperIsEngaged && !rightForceAdded)
        {
            Vector3 forceDirection = new Vector3(collision.transform.position.x, collision.transform.position.y, collision.transform.position.z);

            playBounceSound();

            transform.gameObject.GetComponent<Rigidbody>().AddForce( -forceDirection * flipperController.rightFlipperPower, ForceMode.Impulse);

            Debug.Log("Flipper Power: " + flipperController.rightFlipperPower);

            rightForceAdded = true;
        }


        if (collision.transform.tag == "pool-ring")
        {
            playBounceSound();
            ui.AddScore(25, collision.transform.position);
            transform.gameObject.GetComponent<Rigidbody>().AddForce(-(transform.position) * poolRingPower, ForceMode.Impulse);
        }





    }
}
