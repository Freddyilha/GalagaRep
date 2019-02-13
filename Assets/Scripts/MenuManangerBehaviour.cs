using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuManangerBehaviour : MonoBehaviour {

    public GameObject creditsPanel;

	// Use this for initialization
	void Start () {
		
	}
    //public GameObject particle;
    // Update is called once per frame
    void Update () {

    }

    public void onStartButtonPress()
    {
        SceneManager.LoadScene("GameScene");
    }

    public void onCreditsButtonPress()
    {
        creditsPanel.SetActive(!creditsPanel.activeSelf);
    }

    public void onCreditsCloseButtonPress()
    {
        creditsPanel.SetActive(false);
    }
}
