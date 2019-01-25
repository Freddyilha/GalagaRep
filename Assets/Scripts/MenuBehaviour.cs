using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuBehaviour : MonoBehaviour {

	// Use this for initialization
	void Start () {
		
	}
    public GameObject particle;
    // Update is called once per frame
    void Update () {
        if (Input.anyKey)
        {
            SceneManager.LoadScene("GameScene");
        }




    }


}
