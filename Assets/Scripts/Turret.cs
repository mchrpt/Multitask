using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Turret : MonoBehaviour
{
    public GameObject leftBarrel, rightBarrel, laserProjectile;
    public float setMinRange, setMaxRange;
    public Rigidbody2D rb;
    public bool canFire;
    public Ship ship;
    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(fireAtInterval());
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        
        Vector3 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition); //get mouse pos
        Quaternion rot = Quaternion.LookRotation(transform.position - mousePosition, Vector3.forward); //Z axis
        //print(rot.eulerAngles.z);

        if(setMaxRange > 360 && (setMinRange <= rot.eulerAngles.z && rot.eulerAngles.z <= 360) || (setMaxRange - 360 >= rot.eulerAngles.z && rot.eulerAngles.z >= 0))
        {
            canFire = true;
            transform.rotation = Quaternion.Lerp(transform.rotation, rot, .1f);
            transform.eulerAngles = new Vector3(0, 0, transform.eulerAngles.z);
        }

        else if (rot.eulerAngles.z > setMinRange && rot.eulerAngles.z < setMaxRange) {
            canFire = true;
            transform.rotation = Quaternion.Lerp(transform.rotation, rot, .1f);
            transform.eulerAngles = new Vector3(0, 0, transform.eulerAngles.z);
        }
        else
        {
            canFire = false;
        }
        
        /*if (Input.GetKeyDown(KeyCode.Mouse0) && canFire)
        {
            Instantiate(laserProjectile, leftBarrel.transform.position, leftBarrel.transform.rotation);
            Instantiate(laserProjectile, rightBarrel.transform.position, rightBarrel.transform.rotation);
        }*/
    }
    IEnumerator fireAtInterval()
    {
        yield return new WaitForSeconds(ship.gunFireInterval());
        
        //print("running " + ship.gunFireInterval());
        if (Input.GetKey(KeyCode.Mouse0) && canFire)
        {
           GameObject leftLaser = Instantiate(laserProjectile, leftBarrel.transform.position, leftBarrel.transform.rotation) as GameObject;
           GameObject rightLaser = Instantiate(laserProjectile, rightBarrel.transform.position, rightBarrel.transform.rotation) as GameObject;
           leftLaser.GetComponent<LaserMove>().engineAmount = ship.engineAmount;
           rightLaser.GetComponent<LaserMove>().engineAmount = ship.engineAmount;
        }
        StartCoroutine(fireAtInterval()); //Keep firing over and over again!

    }
 
}
