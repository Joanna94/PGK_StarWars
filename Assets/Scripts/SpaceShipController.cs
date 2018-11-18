using UnityEngine;
using System.Collections;

public class SpaceShipController : MonoBehaviour {

    public MissleController missle;
    private float axisX = 0f, axisY = 0f;
    public float maxForce = 0f;
    public ParticleSystem upperLeft, upperRight, upperMiddleLeft, upperMiddleRight,
        downMiddleLeft, downMiddleRight, downLeft, downRight;
    private bool canShoot = true;
        private int live = 100;


	// Use this for initialization
	void Start () {
        upperLeft.enableEmission = false;
        upperRight.enableEmission = false;
        upperMiddleLeft.enableEmission = false;
        upperMiddleRight.enableEmission = false;
        downMiddleLeft.enableEmission = false;
        downMiddleRight.enableEmission = false;
        downLeft.enableEmission = false;
        downRight.enableEmission = false;
    }
	
	// Update is called once per frame
	void Update () {
        axisX = Input.GetAxisRaw("Horizontal") * Time.deltaTime;
        axisY = Input.GetAxisRaw("Vertical") * Time.deltaTime;
        float timeDelay = 1f;

        if(axisX == 0 || axisY == 0)
        {
            this.GetComponent<Rigidbody>().AddForce(new Vector3(axisX, axisY, 0) * maxForce);
        }
        if(Input.GetKeyDown(KeyCode.LeftArrow))
        {
            upperMiddleRight.enableEmission = true;
            downMiddleRight.enableEmission = true;
        }
        if (Input.GetKeyUp(KeyCode.LeftArrow))
        {
            upperMiddleRight.enableEmission = false;
            downMiddleRight.enableEmission = false;
        }

        if (Input.GetKeyDown(KeyCode.RightArrow))
        {
            upperMiddleLeft.enableEmission = true;
            downMiddleLeft.enableEmission = true;
        }
        if (Input.GetKeyUp(KeyCode.RightArrow))
        {
            upperMiddleLeft.enableEmission = false;
            downMiddleLeft.enableEmission = false;
        }


        if (Input.GetKeyDown(KeyCode.UpArrow))
        {
            downLeft.enableEmission = true;
            downRight.enableEmission = true;
        }
        if (Input.GetKeyUp(KeyCode.UpArrow))
        {
            downLeft.enableEmission = false;
            downRight.enableEmission = false;
        }

        if (Input.GetKeyDown(KeyCode.DownArrow))
        {
            upperLeft.enableEmission = true;
            upperRight.enableEmission = true;
        }
        if (Input.GetKeyUp(KeyCode.DownArrow))
        {
            upperLeft.enableEmission = false;
            upperRight.enableEmission = false;
        }

        if(Input.GetKeyDown(KeyCode.T))
        {
            if(canShoot){
                Instantiate(missle, this.transform.position + new Vector3(0, 0.1f , 0), Quaternion.Euler(0, 0, 0));
                StartCoroutine(WaitForNextShoot(timeDelay));
                }
        }

        if (Input.GetKeyDown(KeyCode.V))
        {
            if(canShoot){
                Instantiate(missle, this.transform.position + new Vector3(0, -0.1f, 0), Quaternion.Euler(180, 90, 0));
                StartCoroutine(WaitForNextShoot(timeDelay));
            }
        }

        if (Input.GetKeyDown(KeyCode.G))
        {
            if(canShoot){
                Instantiate(missle, this.transform.position + new Vector3(0.1f, 0, 0), Quaternion.Euler(90, 90, 0));
                StartCoroutine(WaitForNextShoot(timeDelay));
            }
        }

        if (Input.GetKeyDown(KeyCode.F))
        {
            if(canShoot){
                Instantiate(missle, this.transform.position + new Vector3(-0.1f, 0, 0), Quaternion.Euler(270, 90, 0));
                StartCoroutine(WaitForNextShoot(timeDelay));
            }
        }
    }

    IEnumerator WaitForNextShoot(float time)
    {
        canShoot = false;
        yield return new WaitForSeconds(time);
        canShoot = true;
    }

    public void Hit()
    {
        live -= 10;
        Debug.Log("Your live: " + live);

        if(live < 1)
        {
            Destroy(this.gameObject);
            Debug.Log("You DIED");
        }

    }
}
