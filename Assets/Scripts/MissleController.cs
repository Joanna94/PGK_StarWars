using UnityEngine;
using System.Collections;

public class MissleController : MonoBehaviour {

    public float missleSpeed = 1.0f, missleAcceleration = 1.0f;
    public RaycastHit hit;
    bool left;

	// Use this for initialization
	void Start () {
	    StartCoroutine(AutoDestruct(7)); 
	}
	
	// Update is called once per frame
	void Update () {
        this.transform.Translate(this.transform.up * missleSpeed * Time.deltaTime, Space.World);
        missleSpeed += missleAcceleration * Time.deltaTime;
        Debug.DrawRay(this.gameObject.transform.position, Vector3.up);

        if(Physics.Raycast(this.gameObject.transform.position, this.gameObject.transform.up, out hit, 0.3f))
        {
            Explosion();
            if(hit.collider.gameObject.name == "SpaceShip")
            {
                GameObject.Find("SpaceShip").GetComponent<SpaceShipController>().Hit();
            }
            else if (hit.collider.gameObject.name == "EnemySpaceShip")
            {
                GameObject.Find("EnemySpaceShip").GetComponent<EnemySpaceShipController>().Hit();
            }
        }
	}

    void Explosion()
    {
        Destroy(this.gameObject);
    }

    IEnumerator AutoDestruct(int time)
    {
        yield return new WaitForSeconds(time);
        Explosion();
    }
}
