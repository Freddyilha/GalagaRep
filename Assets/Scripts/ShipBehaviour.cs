using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShipBehaviour : MonoBehaviour {

    Vector2 position;
    [SerializeField] private GameObject bulletPreFab;

	// Use this for initialization
	void Start () {
        
	}
	
	// Update is called once per frame
	void Update () {
        ShipMovement();
        if (Input.GetKeyDown(KeyCode.Mouse0))
            ShipShoot();

    }

    public void ShipMovement()
    {
        Vector2 newPoint = (Camera.main.ScreenToWorldPoint(Input.mousePosition));

        newPoint.y = -4.75f;

        transform.position = newPoint;

    }

    public void ShipShoot()
    {
        GameObject bullet;
        bullet = Instantiate<GameObject>(bulletPreFab);
        //Debug.Log("bullet before: " + bullet.transform.position);
        bullet.transform.position = GetComponent<Transform>().position;
        //Debug.Log("bullet: " + bullet.transform.position + "ship: " + GetComponent<Transform>().position);

    }

    private void OnTriggerEnter2D(Collider2D collider)
    {
        if (collider.tag == "BulletEnemie")
        {
            GameManangerBehaviour.instance.onPlayerHit();
            //Debug.Log("Bati com: " + collider.tag);
        }
    }
}
