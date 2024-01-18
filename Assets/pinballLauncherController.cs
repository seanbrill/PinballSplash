using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class pinballLauncherController : MonoBehaviour
{
    [SerializeField]
    public GameObject launcher;

    public static pinballLauncherController instance;

    public static float shotPower = 0f;
    private float launcherShotSpeed = 100f;
    private float intialLauncherZPos;
    public static bool shotReleased = false;



    // Start is called before the first frame update
    void Start()
    {
        intialLauncherZPos = launcher.transform.position.z;
        instance = this;
    }

    // Update is called once per frame
    void Update()
    {

        shotCharge();
    }


    private void FixedUpdate()
    {
       // shotCharge();
    }




    private void OnCollisionEnter(Collision collision)
    {
        
     
        print(collision);
        print("collide");
        if (collision.transform.tag == "pinball" && shotReleased)
        {
           
           // collision.gameObject.GetComponent<Rigidbody>().AddForce(-launcher.transform.position * shotPower, ForceMode.Impulse);
        }
    }


    void shotCharge() {

        

  

        if (Input.GetKey(KeyCode.Space) && launcher.transform.position.z > -70f) 
        {

            shotReleased = false;
            shotPower += 10f * Time.deltaTime;
            launcher.transform.Translate(new Vector3(0f,0f,-1f) * 50f * Time.deltaTime, Space.World );



        }

       
        
        if(!Input.GetKey(KeyCode.Space) && launcher.transform.position.z < intialLauncherZPos)
        {

            launcher.transform.Translate(new Vector3(0f, 0f, 1f) * launcherShotSpeed * Time.deltaTime, Space.World);
            //launcher.GetComponent<Rigidbody>().AddForce(Vector3.forward*shotPower);
           

        }


        if (!Input.GetKey(KeyCode.Space))
        {
            shotReleased = true;
        }

        
    
        
    
    }
}
