using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AlienBehaviour : MonoBehaviour {

    private string alienType;
    private float randomNumber;
    private float randomNumberAnim;
    private Animator anim;
    //public Animation[] animationList;
    [SerializeField]
    private GameObject bulletPreFab;
    


    // Use this for initialization
    void Start () {
        //Debug.Log("comecei");
        anim = GetComponent<Animator>();
        InvokeRepeating("maybeAttack", 2.0f, 3.0f); /* Chama a função a cada 3 segundos*/
        InvokeRepeating("playAttackAnimation", 5.0f, 10.0f);

    }
	
	// Update is called once per frame
	void Update () {
        
    }

    //private void OnCollisionEnter2D(Collision2D collision)
    //{
    //    if (collision.collider.tag == "Bullet")
    //    {
    //        Debug.Log("Bati com: " + collision.collider);
    //    }
    //}

    private void OnTriggerEnter2D(Collider2D collider)
    {
        if (collider.tag == "Bullet")
        {
            alienType = GetComponent<SpriteRenderer>().sprite.ToString();
            GameManangerBehaviour.instance.onEnemieDeath(alienType);
            Destroy(gameObject);
            //Debug.Log("Bati com bla: " + collider);

        }
    }

    public void maybeAttack()
    { 
        randomNumber = Random.Range(0f, 100f);
        //Debug.Log("randomNumber: " + randomNumber);
        if (randomNumber < 50.0f) /* 50% de chance de atirar */
        {
            GameObject bullet;
            bullet = Instantiate<GameObject>(bulletPreFab);
            bullet.transform.position = GetComponent<Transform>().position;
        }

    }

    public void playAttackAnimation()
    {
        //Debug.Log("chamada");
        randomNumberAnim = Random.Range(0f, 100f);
        //anim.SetFloat("randNumber", randomNumberAnim);
        if (randomNumberAnim < 50.0f) /* 50% de chance de atirar */
            anim.SetTrigger("AnimOne");
        //anim.Play("AlienAttackOneAnimation");
        else
            anim.SetTrigger("AnimTwo");

    }



}
