using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuUI : MonoBehaviour {
    
    public void NewGame() {
        SceneManager.LoadScene("SampleScene");
    }

    public void Tutorial(GameObject window) {
        window.SetActive(true);
    }

    public void Quit() {
        Application.Quit();
    }

}
