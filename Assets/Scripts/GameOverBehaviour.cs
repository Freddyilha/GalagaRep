﻿    using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameOverBehaviour : MonoBehaviour {

    Dictionary <string, int> scoreDict;
    public GameObject dynamicScorePreFab;
    public GameObject Panel;
    [HideInInspector] public int lineCounter;
    Sprite alienResourceSprite;
    public Text highScoreText;
    //public GameObject enemiePicture;

    // Use this for initialization
    void Start () {
        //foreach (var alien in GameManangerBehaviour.instance.deathsDict)
        //{
        //    Debug.Log("Key: " + alien.Key + " -- Value: " + alien.Value);
        //}

        //Debug.Log("iuwiwqu: " + GameManangerBehaviour.instance.deathsDict.Values);

        //scoreDict = new Dictionary<string, int>();
        //scoreDict.Add("AlienOwl", 3);
        //scoreDict.Add("AlienMoth", 6);
        //scoreDict.Add("AlienBee", 10);

        //Debug.Log("jjjjjjjjjjjjjj: " + scoreDict.Keys);

        //highScoreText.text = GameManangerBehaviour.instance.getFinalScore().ToString();
        GameObject enemiesTable;

        highScoreText.text = GameManangerBehaviour.instance.getFinalScore().ToString();

        //Debug.Log("asdasd: " + scoreDict.Keys);
        lineCounter = -2;       /*Valor usado para a alinhar os inimigos derrotados com texto Galaga*/
        foreach (var alien in GameManangerBehaviour.instance.deathsDict)
        {
            //Debug.Log("asdasd: " + alien.Value.ToString());

            enemiesTable = Instantiate<GameObject>(dynamicScorePreFab);
            
            // enemiesTable.transform.position = panel.transform.position;
            

            enemiesTable.transform.SetParent(Panel.transform);

            /*Eu tinha esquecido de trocar a escala estava funcionando desdo inicio
            porém quando instanciados eles vão para outra posição ignorando o painel*/
            enemiesTable.transform.localScale = Vector2.one;
            //enemiesTable.transform.position = Panel.transform.position; 
            //enemiesTable.transform.position = Vector2.up * 40;
            //enemiesTable.transform = Panel.transform;
            enemiesTable.transform.position = Vector2.down * lineCounter++;
            //Debug.Log("dict: " + alien);
            //Debug.Log("dict: " + alien.GetType());
            //Debug.Log("Key: " + alien.Key);
            //Debug.Log(alien.Key);
            //Debug.Log("Key: " + alien.Key.GetType());
            //Debug.Log("Key string: " + alien.Key.Substring(0, alien.Key.Length - 21));
            //Debug.Log("Sprites/" + alien.Key);

            /* Aqui foi necessario fazer um slice na string pois quando puxado do dicionario da outra cena
               junto do nome tem '(Unityengine.Sprite)'*/
            alienResourceSprite = Resources.Load<Sprite>("Sprites/" + alien.Key.Substring(0, alien.Key.Length - 21));
            //Debug.Log(">>> " + bla);
            enemiesTable.GetComponent<PanelHelper>().alienSprite.sprite = alienResourceSprite;

            enemiesTable.GetComponent<PanelHelper>().texto.text = alien.Value.ToString();
            

            //GetComponent<DerpHelper>().alienSprite.sprite = Resources.Load<Sprite>("Sprites/" + alien.Key);
            //GetComponent<DerpHelper>().texto.text = alien.Value.ToString();


        }
    }
	
	// Update is called once per frame
	void Update () {
        if (Input.anyKey)
        {
            SceneManager.LoadScene("GameScene");
        }
    }
}
