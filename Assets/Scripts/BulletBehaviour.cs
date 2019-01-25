using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletBehaviour : MonoBehaviour {

    Vector2 position;


    // FAZER A ANIMAÇÃO DE MOVIMENTO BASEADA EM QUEM TA ATIRANDO
    // SE O JOGARDOR ATIRAR DEVE SOMAR A POSIÇÃO
    // SE O INIMIGO ATIRAR DEVE SUBTRAIR A POSIÇÃO


    // Use this for initialization
    void Start () {

        ////position.y = -4.7f;
        position = GetComponent<Transform>().transform.position;
        //Debug.Log("Start position: " + GetComponent<Transform>().transform.position);
        //Debug.Log("Bullet position: " + transform.position);

    }
	
	// Update is called once per frame
	void Update () {
        //if (GetComponent<SpriteRenderer>().sprite.ToString == "BulletDown")
        //{

        //}
        if (transform.localPosition.y > 6)
            Destroy(gameObject);
        else if (transform.localPosition.y < -6)
            Destroy(gameObject);
        if (gameObject.tag == "Bullet")
        {
            transform.position = this.position;
            position.y += 0.1f;
        }
        else if ((gameObject.tag == "BulletEnemie"))
        {
            transform.position = this.position;
            position.y -= 0.1f;
        }
        
        //Debug.Log("position: " +  );
    }

    
}
