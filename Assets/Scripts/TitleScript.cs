using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class TitleScript : MonoBehaviour
{
    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            SceneManager.LoadScene("GameScene");
        }
    }

    private GUIStyle guiStyle = new GUIStyle();

    void OnGUI()
    {
        guiStyle.fontSize = 80;
        guiStyle.normal.textColor = Color.white;
        GUI.Label(new Rect(Screen.width / 2 - 240, Screen.height / 2 - 40, 128, 32), "Plastic Runner", guiStyle);
    }
}
