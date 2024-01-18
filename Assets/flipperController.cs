using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class flipperController : MonoBehaviour
{

    public GameObject LeftFlipper;
    public GameObject RightFlipper;

    private Quaternion intialLeftRoation;
    private Quaternion initalRightRotation;

    [SerializeField]
    private float flipperRotationSpeed =   10;

    public float flipperPower = 5.0f;

    public static bool leftFlipperIsEngaged;
    public static float leftFlipperPower;

    public static bool rightFlipperIsEngaged;
    public static float rightFlipperPower;

    // Start is called before the first frame update
    void Start()
    {
        this.intialLeftRoation = LeftFlipper.transform.rotation;
        this.initalRightRotation = RightFlipper.transform.rotation;
        Debug.Log(intialLeftRoation.eulerAngles.y);
        Debug.Log(initalRightRotation.eulerAngles.y);
        leftFlipperPower = flipperPower;
        rightFlipperPower = flipperPower;
        Debug.Log(flipperPower);
    }

    // Update is called once per frame
    void Update()
    {

       // onPlayerInput();

    }

    private void FixedUpdate()
    {
        onPlayerInput();
    }

    void onPlayerInput() {



        //LEFT FLIPPER

        if ((LeftFlipper.transform.rotation.eulerAngles.y%360 < 65f ) && (Input.GetKey(KeyCode.LeftArrow) || Input.GetKey(KeyCode.A))) {
           // Debug.Log("Flipper Power: " + leftFlipperPower);

            leftFlipperIsEngaged = true;

            StartCoroutine(leftFlipperPowerFade());

            LeftFlipper.transform.Rotate(new Vector3(this.intialLeftRoation.x , - this.flipperRotationSpeed, this.intialLeftRoation.z) * Time.deltaTime * this.flipperRotationSpeed,Space.World);
        }
       


        if (LeftFlipper.transform.rotation.y < this.intialLeftRoation.y && !Input.GetKey(KeyCode.LeftArrow) && !Input.GetKey(KeyCode.A))
        {
            leftFlipperIsEngaged = false;
            pinball.leftForceAdded = false;
            leftFlipperPower = flipperPower;
            LeftFlipper.transform.Rotate(new Vector3(this.intialLeftRoation.x, this.flipperRotationSpeed, this.intialLeftRoation.z) * Time.deltaTime * this.flipperRotationSpeed, Space.World);
        }


        //RIGHT FLIPPER

        if ((RightFlipper.transform.rotation.eulerAngles.y%360 > 65f) && (Input.GetKey(KeyCode.RightArrow) || Input.GetKey(KeyCode.D)))
        {
           // Debug.Log("Flipper Power: " + rightFlipperPower);

            rightFlipperIsEngaged = true;

            StartCoroutine(rightFlipperPowerFade());

            RightFlipper.transform.Rotate(new Vector3(this.initalRightRotation.x, this.flipperRotationSpeed, this.initalRightRotation.z) * Time.deltaTime * this.flipperRotationSpeed, Space.World);
        }

      
        if (RightFlipper.transform.rotation.y > initalRightRotation.y && !Input.GetKey(KeyCode.RightArrow) && !Input.GetKey(KeyCode.D))
        {
            rightFlipperIsEngaged = false;
            pinball.rightForceAdded = false;
            rightFlipperPower = flipperPower;
            RightFlipper.transform.Rotate(new Vector3(this.initalRightRotation.x, -this.flipperRotationSpeed, this.initalRightRotation.z) * Time.deltaTime * this.flipperRotationSpeed, Space.World);
        }


    }


    IEnumerator leftFlipperPowerFade() {

        while (leftFlipperPower > 0) {

           flipperController.leftFlipperPower -= Time.deltaTime;
            yield return null;
        }

       
           
    }


    IEnumerator rightFlipperPowerFade()
    {

        while (rightFlipperPower > 0)
        {

            flipperController.rightFlipperPower -= Time.deltaTime;
            yield return null;
        }

        
        

    }



}
