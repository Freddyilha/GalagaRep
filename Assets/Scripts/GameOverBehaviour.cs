using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameOverBehaviour : MonoBehaviour {

    Dictionary <string, int> scoreDict;
    public GameObject dynamicScorePreFab;
    public GameObject Panel;
    public int lineCounter;
    Sprite bla;
    //public GameObject enemiePicture;

    // Use this for initialization
    void Start () {
        scoreDict = new Dictionary<string, int>();
        scoreDict.Add("AlienOwl", 3);
        scoreDict.Add("AlienMoth", 6);
        scoreDict.Add("AlienBee", 10);

        GameObject enemiesTable;

        //Debug.Log("asdasd: " + scoreDict.Keys);

        foreach (var alien in scoreDict)
        {
            //Debug.Log("asdasd: " + alien.Value.ToString());

            enemiesTable = Instantiate<GameObject>(dynamicScorePreFab);
            enemiesTable.transform.position = Vector2.down * lineCounter++;
            // enemiesTable.transform.position = panel.transform.position;
            

            enemiesTable.transform.SetParent(Panel.transform);

            /*Eu tinha esquecido de trocar a escala estava funcionando desdo inicio
            porém quando instanciados eles vão para outra posição ignorando o painel*/
            enemiesTable.transform.localScale = Vector3.one;

            bla = Resources.Load<Sprite>("Sprites/" + alien.Key);
            //print(">>> " + bla);
            enemiesTable.GetComponent<PanelHelper>().alienSprite.sprite = bla;

            enemiesTable.GetComponent<PanelHelper>().texto.text = alien.Value.ToString();

            //GetComponent<DerpHelper>().alienSprite.sprite = Resources.Load<Sprite>("Sprites/" + alien.Key);
            //GetComponent<DerpHelper>().texto.text = alien.Value.ToString();


        }
    }
	
	// Update is called once per frame
	void Update () {
		
	}
}
