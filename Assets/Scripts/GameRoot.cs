using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameRoot : MonoBehaviour
{
    public float step_timer = 0.0f; // 경과 시간을 유지

    // 멤버 변수 추가
    private PlayerControl player = null;

    void Start()
    {
        this.player = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerControl>();
    }

    void Update()
    {
        this.step_timer += Time.deltaTime; // 경과 시간을 더함

        if (this.player.isPlayEnd())
        {
            SceneManager.LoadScene("TitleScene");
        }
    }
    public float getPlayTime()
    {
        float time;
        time = this.step_timer;
        return (time); // 호출한 곳에 경과 시간을 알려줌
    }
}
