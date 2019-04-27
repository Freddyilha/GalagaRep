using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameManangerBehaviour : MonoBehaviour {

    public static GameManangerBehaviour instance;

    public enum AlienType { AlienBee, AlienMoth, AlienOwl, AlienDemonOwl };
    public Dictionary <string, int> deathsDict;
    public GameObject[] alienFormationList;
    public GameObject[] alienList;
    public Text scoreText;
    private int scoreTextToInt;
    [HideInInspector] public GameObject[] playerShipIconList;
    public GameObject playerShip;
    public GameObject[] livesPanel;
    private int lives = 1;
    private int activeFormationEnemiesCount;
    protected Transform activeFormation;
    protected Transform enemieFormationTransform;
    protected int activeFormationIndex;
    private bool gameOverFlag;
    [HideInInspector] public bool restartingGameFlag;
    [SerializeField] private GameObject enemieTransformPreFab; 

    void Awake()
    {
        if (instance == null)
            instance = this;
        else
            Destroy(gameObject);
        DontDestroyOnLoad(gameObject);
    }

    // Use this for initialization
    void Start () {
        StartCoroutine(waitForFanfare());
	}
	
	// Update is called once per frame
	void Update () {
        if (gameOverFlag == false)
        {
            if (activeFormationEnemiesCount == 0)
            {
                activeFormationIndex++;
                if (activeFormationIndex >= alienFormationList.Length)
                {
                    //Debug.Log("Troquei!!!");
                    gameOverFlag = true;
                    SceneManager.LoadScene("GameOver");
                    //SceneManager.UnloadScene("GameScene");
                }
                else
                {
                    createFormation();
                    createEnemies();
                }
            
            }
        }
        if (restartingGameFlag == true)
        {
            restartingGameFlag = false;
            StartCoroutine(waitForFanfare());
        }
    }

    protected void createFormation()
    {

        enemieFormationTransform = Instantiate<GameObject>(enemieTransformPreFab).transform;
        activeFormation = Instantiate<GameObject>(alienFormationList[activeFormationIndex]).transform;
        activeFormation.SetParent(enemieFormationTransform);
        activeFormation.localPosition = Vector3.zero;
        //Debug.Log("child Count: " + activeFormation.childCount);
        activeFormationEnemiesCount = activeFormation.childCount;
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
        int.TryParse(scoreText.text, out scoreTextToInt);
        scoreTextToInt += 50;
        scoreText.text = scoreTextToInt.ToString();
        activeFormationEnemiesCount--;
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
        else
        {
            livesPanel[lives].SetActive(!livesPanel[lives]);
            lives--;
        }
    }

    public int getFinalScore()
    {
        return scoreTextToInt;
    }

    public void onNewGame()
    {
        deathsDict = new Dictionary<string, int>();
        activeFormationIndex = 0;
        createFormation();
        createEnemies();
        createPlayerShip();
        gameOverFlag = false;
        restartingGameFlag = false;
    }
    
    public IEnumerator waitForFanfare()
    {
        AudioManagerBehaviour.instance.Play("Fanfare");
        gameOverFlag = true;
        yield return new WaitForSecondsRealtime(7);
        onNewGame();
    }
}
