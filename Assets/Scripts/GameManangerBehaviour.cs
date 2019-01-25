using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManangerBehaviour : MonoBehaviour {

    public enum AlienType { AlienBee, AlienMoth, AlienOwl, AlienDemonOwl };
    Dictionary <string, int> deathsDict;
    public static GameManangerBehaviour instance;

    public GameObject[] alienFormationList;
    public GameObject[] alienList;
    [HideInInspector] public GameObject[] playerShipIconList;
    public Transform enemiesCanvas;
    public GameObject playerShip;
    public GameObject[] livesPanel;
    private int lives = 1;

    protected Transform activeFormation;
    protected int activeFormationIndex;

    void Awake()
    {
        if (instance == null)
            instance = this;
        else if (instance != this)
            Destroy(gameObject);
        DontDestroyOnLoad(gameObject);
    }

    // Use this for initialization
    void Start () {
        deathsDict = new Dictionary<string, int>();
        activeFormationIndex = 0;
        createFormation();
        createEnemies();
        createPlayerShip();

	}
	
	// Update is called once per frame
	void Update () {
		
	}

    protected void createFormation()
    {
        activeFormation = Instantiate<GameObject>(alienFormationList[activeFormationIndex]).transform;
        activeFormation.SetParent(enemiesCanvas.transform);
        activeFormation.localPosition = Vector3.zero;
    }



    protected void createEnemies()
    {

        //Itera por todos os filhos, pois o transform possui nativamente um GetIterator com seus filhos.
        foreach (Transform item in activeFormation)
        {
            //print("item pos antes: " + item.position);
            int alienType = (int)item.gameObject.GetComponent<AlienFormationBehaviour>().alienType;
            GameObject alien = Instantiate(alienList[alienType]);

            /*Aqui também estava funcionando porém quando passando o vetor zero eles estavam indo para a mesma
             posição então eu tive que colocar pra receber a posição do item*/
            alien.transform.localPosition = item.position;
            //print("alien pos: " + alien.transform.position);
            alien.transform.SetParent(item);
            //currentAlienFleet++;
        }
    }

    public void onEnemieDeath(string alienKilled)
    {
        try
        {
            deathsDict[alienKilled]++;
        }
        catch
        {
            deathsDict.Add(alienKilled, 1);
        }
    }

    protected void createPlayerShip()
    {
        GameObject player;
        player = Instantiate<GameObject>(playerShip);
        player.transform.position = new Vector2(-0.1f, -4.75f); 

    }

    public void onPlayerHit()
    {
        if (lives == -1)
        {
            SceneManager.LoadScene("GameOver");
        }
        livesPanel[lives].SetActive(!livesPanel[lives]);
        lives--;
    }
}
