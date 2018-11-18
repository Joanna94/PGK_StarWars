using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpaceShipController : MonoBehaviour {

    public float horizontalDim = 30.0f;
    public float verticalDim = 15.0f;
    public ParticleSystem upperLeft, upperRight, upperMiddleLeft, upperMiddleRight,
        downMiddleLeft, downMiddleRight, downLeft, downRight;
    public float maxForce = 0f;
    public float maxSpeed = 0f;
    public GameObject playerShip;
    public GameObject missile;

    public GameObject dest;

    private Vector2 speed = new Vector2(0f, 0f);
    private Vector2 destination;
    private bool canShoot = true;
    private int live = 100;

    private enum AI_States
    {
        AIS_IDLE,
        AIS_ATTACK,
        AIS_ESCAPE
    }

    private AI_States state = AI_States.AIS_IDLE;

	// Use this for initialization
	void Start () {
        destination = new Vector2(-horizontalDim, verticalDim);
	}
	
	// Update is called once per frame
	void Update () {
		StopEngines();
        MoveToDestionation();
        ShootThePlayer();

        if(state == AI_States.AIS_IDLE)
        {
            

        }
        else if(state == AI_States.AIS_ATTACK)
        {
            if(live < 30)
                state = AI_States.AIS_ESCAPE;

        }
        else if(state == AI_States.AIS_ESCAPE)
        {

        }

	}

    void StopEngines()
    {
        upperLeft.enableEmission = false;
        upperRight.enableEmission = false;
        upperMiddleLeft.enableEmission = false;
        upperMiddleRight.enableEmission = false;
        downMiddleLeft.enableEmission = false;
        downMiddleRight.enableEmission = false;
        downLeft.enableEmission = false;
        downRight.enableEmission = false;	
    }

    void MoveLeft()
    {
        if(speed[0] < maxSpeed)
        {
            upperMiddleRight.enableEmission = true;
            downMiddleRight.enableEmission = true;

            speed[0] += Time.deltaTime;
            GetComponent<Rigidbody>().AddForce(new Vector3(-1 * maxForce * speed[0] * Time.deltaTime, 0f, 0f));
        }else
            speed[0] -= Time.deltaTime;
            GetComponent<Rigidbody>().AddForce(new Vector3(-1 * maxForce * speed[0] * Time.deltaTime, 0f, 0f));
    }

    void MoveRight()
    {
        if(speed[0] < maxSpeed)
        {
            upperMiddleLeft.enableEmission = true;
            downMiddleLeft.enableEmission = true;

            speed[0] += Time.deltaTime;
            GetComponent<Rigidbody>().AddForce(new Vector3(maxForce * speed[0] * Time.deltaTime, 0f, 0f));
        }
        else
        {
            speed[0] -= Time.deltaTime;
            GetComponent<Rigidbody>().AddForce(new Vector3(maxForce * speed[0] * Time.deltaTime, 0f, 0f));
        }
    }

    void MoveDown()
    {
        if(speed[1] < maxSpeed)
        {
            downLeft.enableEmission = true;
            downRight.enableEmission = true;

            speed[1] += Time.deltaTime;
            GetComponent<Rigidbody>().AddForce(new Vector3(0f, -1 * maxForce * speed[0] * Time.deltaTime, 0f));
        }else
            speed[1] -= Time.deltaTime;
            GetComponent<Rigidbody>().AddForce(new Vector3(0f, -1 * maxForce * speed[0] * Time.deltaTime, 0f));
    }

    void MoveUp()
    {
        if(speed[1] < maxSpeed)
        {
            upperLeft.enableEmission = true;
            upperRight.enableEmission = true;

            speed[1] += Time.deltaTime;
            GetComponent<Rigidbody>().AddForce(new Vector3(0f, maxForce * speed[0] * Time.deltaTime, 0f));
        }else
            speed[1] -= Time.deltaTime;
            GetComponent<Rigidbody>().AddForce(new Vector3(0f, maxForce * speed[0] * Time.deltaTime, 0f));
    }

    void MoveToDestionation()
    {
        float distance = Mathf.Sqrt(Mathf.Pow(destination[0] - this.transform.position.x, 2) + Mathf.Pow(destination[1] - this.transform.position.y,2));
        
        destination = new Vector2(Random.value * 10, Random.value * 10 * (-1));

        dest.gameObject.transform.SetPositionAndRotation(new Vector3(destination[0], destination[1], 0f), new Quaternion());

        if(distance < 25)
            destination[0] *= -1;

        if(this.transform.position.x > destination[0])
            MoveLeft();
        else if(this.transform.position.x < destination[0])
            MoveRight();

        if(distance < 15)
            destination[1] *= -1;

        if(this.transform.position.y > destination[1])
            MoveDown();
        else if(this.transform.position.y < destination[1])
            MoveUp();

    }

    void ShootThePlayer()
    {
        float timeDelay = 1.2f;

        if(canShoot){
            //down
            if(playerShip.transform.position.x > this.transform.position.x - 2 && playerShip.transform.position.x < this.transform.position.x + 2 && playerShip.transform.position.y < this.transform.position.y)
            {
                Instantiate(missile, this.transform.position + new Vector3(0, -0.1f, 0), Quaternion.Euler(180, 90, 0));
                StartCoroutine(WaitForNextShoot(timeDelay));
            }
            //up
            else if(playerShip.transform.position.x > this.transform.position.x - 2 && playerShip.transform.position.x < this.transform.position.x + 2 && playerShip.transform.position.y > this.transform.position.y)
            {
                Instantiate(missile, this.transform.position + new Vector3(0, 0.1f , 0), Quaternion.Euler(0, 0, 0));
                StartCoroutine(WaitForNextShoot(timeDelay));
            }
            //left
            else if ( playerShip.transform.position.x < this.transform.position.x && playerShip.transform.position.y > this.transform.position.y - 2 && playerShip.transform.position.y < this.transform.position.y + 2)
            {
                Instantiate(missile, this.transform.position + new Vector3(-0.1f, 0, 0), Quaternion.Euler(270, 90, 0));
                StartCoroutine(WaitForNextShoot(timeDelay));
            }
            //right
            else if ( playerShip.transform.position.x > this.transform.position.x && playerShip.transform.position.y > this.transform.position.y - 2 && playerShip.transform.position.y < this.transform.position.y + 2)
            {
                Instantiate(missile, this.transform.position + new Vector3(0.1f, 0, 0), Quaternion.Euler(90, 90, 0));
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
        Debug.Log("Enemy live: " + live);

        if(live < 1)
        {
            Destroy(this.gameObject);
            Debug.Log("Enemy DIED");
        }

    }
}
