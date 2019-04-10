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
    private int lives = 0;
    private int activeFormationEnemiesCount;
    protected Transform activeFormation;
    protected int activeFormationIndex;
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

    void Awake()
    {
        if (instance == null)
            instance = this;
        else if (instance != this)
            Destroy(gameObject);
        DontDestroyOnLoad(gameObject);
        //DontDestroyOnLoad(enemiesFormationTransform);
    }

    // Use this for initialization
    void Start () {
        onNewGame();
        //TestLive = Instantiate<GameObject>(
        livesPanel[2].gameObject.SetActive(true);
        livesPanel[3].gameObject.SetActive(true);
        
    }
	
	// Update is called once per frame
	void Update () {
        if (activeFormationEnemiesCount == 0)
        {
            activeFormationIndex++;
            if (activeFormationIndex >= alienFormationList.Length)
            {
                //Debug.Log("Troquei!!!");
                //gameReseted = true;
                SceneManager.LoadScene("GameOver");                    
                //SceneManager.UnloadScene("GameScene");
            }
            else
            {
                createFormation();
                createEnemies();
            }
        }
        if (gameReseted == true)
        {
            onNewGame();
            Debug.Log("Cheguei aqui!");
        }
        //Debug.Log("Flag: " + gameReseted)
    }

    protected void createFormation()
    {
        //enemiesFormationTransform = Instantiate<GameObject>(EnemiesFormationPrefab).transform; //EnemiesFormationRef.GetComponent<Transform>();
        activeFormation = Instantiate<GameObject>(alienFormationList[activeFormationIndex]).transform;
        //activeFormation.SetParent(enemiesFormationTransform.transform);
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
        
    }

    public void onPlayerHit()
    {
        if (lives == -1)
        {
            //enemiesFormationTransform.
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

    public int getFinalScore()
    {
        return scoreTextToInt;
    }

    public void setResetGameFlag()
    {
        gameReseted = true;
    }
}
