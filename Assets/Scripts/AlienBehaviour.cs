using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AlienBehaviour : MonoBehaviour {

    private string alienType;
    private float randomNumber;
    private float randomNumberAnim;
    private Animator anim;  
    [SerializeField] private GameObject bulletPreFab;
    
    void Start () {
        anim = GetComponent<Animator>();
        InvokeRepeating("maybeAttack", 2.0f, 3.0f); /* Chama a função a cada 3 segundos*/
        InvokeRepeating("playAttackAnimation", 5.0f, 10.0f);
    }
	
	void Update () {
 
    }
                            
    private void OnTriggerEnter2D(Collider2D collider)
    {
        if (collider.tag == "Bullet")
        {
            alienType = GetComponent<SpriteRenderer>().sprite.ToString();
            GameManangerBehaviour.instance.onEnemieDeath(alienType);
            Destroy(gameObject);
        }
    }

    public void maybeAttack()
    { 
        randomNumber = Random.Range(0f, 100f);
        if (randomNumber < 50.0f) /* 50% de chance de atirar */
        {
            GameObject bullet;
            bullet = Instantiate<GameObject>(bulletPreFab);
            bullet.transform.position = GetComponent<Transform>().position;
            AudioManagerBehaviour.instance.Play("Shot");
        }
    }

    public void playAttackAnimation()
    {
        randomNumberAnim = Random.Range(0f, 100f);
        
        if (randomNumberAnim < 33.0f) /* 33% de chance de ativar uma das duas animações */
            anim.SetTrigger("AnimOne");
        else if (randomNumberAnim > 33.0f && randomNumberAnim < 66.0f)
            anim.SetTrigger("AnimTwo");
            
    }



}
