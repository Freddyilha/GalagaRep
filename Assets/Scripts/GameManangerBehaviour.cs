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
    public Transform enemiesCanvas;
    public GameObject playerShip;
    public GameObject[] livesPanel;
    private int lives = 0;
    private int activeFormationEnemiesCount;
    protected Transform activeFormation;
    protected int activeFormationIndex;
<<<<<<< HEAD
    private bool gameReseted;
    [SerializeField] private AudioSource[] audios = new AudioSource[2];
    [SerializeField] private GameObject liveTest;
    [SerializeField] private GameObject LiveCellPrefab;
    [SerializeField] private GameObject gameCanvas;
    [HideInInspector] public List<GameObject> livesList;
    //private GameObject Live;
    //private GameObject TestLive;
    //[SerializeField] private GameObject EnemiesFormationPrefab; // A REFERENCIA PARA UM OBJETO NÃO É PERDIDA MAS A REFERENCIA PRO TRANSFORM DELE SIM
    //private Transform enemiesFormationTransform;
=======
    private bool gameOverFlag;
>>>>>>> parent of cb14716... Otimizations

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
<<<<<<< HEAD
        onNewGame();
        //TestLive = Instantiate<GameObject>(
        livesPanel[2].gameObject.SetActive(true);
        livesPanel[3].gameObject.SetActive(true);
        
    }
=======
        deathsDict = new Dictionary<string, int>();
        activeFormationIndex = 0;
        createFormation();
        createEnemies();
        createPlayerShip();
        gameOverFlag = false;
	}
>>>>>>> parent of cb14716... Otimizations
	
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
                    SceneManager.LoadScene("GameOver");
                    gameOverFlag = true;
                    //SceneManager.UnloadScene("GameScene");
                }
                else
                {
                    createFormation();
                    createEnemies();
                }
            
            }
        }
<<<<<<< HEAD
        //Debug.Log("Flag: " + gameReseted)
=======
>>>>>>> parent of cb14716... Otimizations
    }

    protected void createFormation()
    {
        activeFormation = Instantiate<GameObject>(alienFormationList[activeFormationIndex]).transform;
        activeFormation.SetParent(enemiesCanvas.transform);
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
        Debug.Log("quem morreu: " + alienKilled);
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
<<<<<<< HEAD
        player.transform.position = new Vector2(-0.1f, -4.75f);
        lives = 1;
    }

    protected void createHUD()
    {
        GameObject liveCell;
        liveCell = Instantiate<GameObject>(LiveCellPrefab);
        liveCell.transform.SetParent(gameCanvas.transform);
        liveCell.transform.localScale = Vector2.one;
        liveCell.transform.localPosition = new Vector2(-327.15f, -214.75f);         /*Canto esquerdo inferior*/
        GameObject Live;
        for (int i = -4; i < -2; i++)   /*Usado para mover do limite esquerdo para a direita*/
        {
            Live = Instantiate<GameObject>(liveTest);
            Live.GetComponent<Image>().sprite = Resources.Load<Sprite>("Sprites/ship");
            Live.transform.SetParent(liveCell.transform);
            Live.transform.localScale = new Vector2(0.2f, 0.2f);
            Live.transform.localPosition = Vector2.right * i * 18;//new Vector2(-70.5f, 0f);
            livesList.Add(Live);
        }
        
=======
        player.transform.position = new Vector2(-0.1f, -4.75f); 
>>>>>>> parent of cb14716... Otimizations
    }

    public void onPlayerHit()
    {
        if (lives == -1)
        {
            SceneManager.LoadScene("GameOver");
        }
        else
        {
            //livesPanel[lives].SetActive(!livesPanel[lives]);
            Destroy(livesList[livesList.Count - 1]);
            livesList.RemoveAt(livesList.Count - 1);
            lives--;
        }
    }

<<<<<<< HEAD
    public void onNewGame()
    {
        deathsDict = new Dictionary<string, int>();
        activeFormationIndex = 0;
        createFormation();
        createEnemies();
        createPlayerShip();
        createHUD();
        gameReseted = false;
    }

=======
>>>>>>> parent of cb14716... Otimizations
    public int getFinalScore()
    {
        return scoreTextToInt;
    }
}
