using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class EndScene : MonoBehaviour {

    public static string lastScene = "";
    public static bool won = false;


    public Text endText;

    void Start() {
        if (won) {
            endText.text = "You Win!\n<size=48>Press space to restart!</size>";
        }
        else {
            endText.text = "You Lose!\n<size=48>Press space to restart!</size>";
        }
    }

    void Update() {
        if (Input.GetKeyDown(KeyCode.Space)) {
            SceneManager.LoadScene(lastScene);
        }
    }

    public static void loadWinScene() {
        lastScene = SceneManager.GetActiveScene().name;
        won = true;
        SceneManager.LoadScene("EndScene");
    }

    public static void loadLoseScene() {
        lastScene = SceneManager.GetActiveScene().name;
        won = false;
        SceneManager.LoadScene("EndScene");
    }


}
