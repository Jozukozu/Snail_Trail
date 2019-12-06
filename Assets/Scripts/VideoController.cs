using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.Video;

public class VideoController : MonoBehaviour
{
    public VideoPlayer VideoPlayer; 

    void Start()
    {
        VideoPlayer.loopPointReached += LoadScene;
    }

    void Update()
    {
        if (Input.GetKeyUp(KeyCode.Escape))
        {
            LoadScene(VideoPlayer);
        }
    }
    void LoadScene(VideoPlayer vp)
    {
        SceneManager.LoadScene("StartScene");
    }
}
