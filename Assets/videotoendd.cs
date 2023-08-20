using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.Video;

public class videotoendd : MonoBehaviour
{
    public VideoPlayer videoPlayer;
    public string nextScene;


    void Start()
    {
        videoPlayer.loopPointReached += LoadScene;
    }

    void LoadScene(VideoPlayer vp)
    {
        SceneManager.LoadScene(nextScene);
    }
    public void SkipCutscene()
    {
        videoPlayer.Stop();
        LoadScene(videoPlayer);
    }

}
