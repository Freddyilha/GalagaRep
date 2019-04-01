using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameOverBehaviour : MonoBehaviour {

    Dictionary <string, int> scoreDict;
    public GameObject dynamicScorePreFab;
    public GameObject Panel;
    public int lineCounter;
    Sprite alienResourceSprite;
    public Text highScoreText;
    
    void Start () {
        lineCounter = -2;           /* Valor usado para alinhar os inimigos mortos com o texto GALAGA */
        scoreDict = new Dictionary<string, int>();
        scoreDict.Add("AlienOwl", 3);
        scoreDict.Add("AlienMoth", 6);
        scoreDict.Add("AlienBee", 10);

        GameObject enemiesTable;

        foreach (var alien in GameManangerBehaviour.instance.deathsDict)
        {
            enemiesTable = Instantiate<GameObject>(dynamicScorePreFab);
            enemiesTable.transform.SetParent(Panel.transform);
            enemiesTable.transform.localScale = Vector2.one;        /*Escala usada para ficar no tamanho do painel*/
            enemiesTable.transform.position = Vector2.down * lineCounter++;
            /* Aqui foi necessario fazer um slice na string pois quando puxado do dicionario da outra cena
               junto do nome tem '(Unityengine.Sprite)'*/
            alienResourceSprite = Resources.Load<Sprite>("Sprites/" + alien.Key.Substring(0, alien.Key.Length - 21));
            enemiesTable.GetComponent<PanelHelper>().alienSprite.sprite = alienResourceSprite;
            enemiesTable.GetComponent<PanelHelper>().texto.text = alien.Value.ToString();
        }
    }
	
	void Update () {
        if (Input.anyKey)
        {
            GameManangerBehaviour.instance.setResetGameFlag();
            SceneManager.LoadScene("GameScene");
        }
    }
}