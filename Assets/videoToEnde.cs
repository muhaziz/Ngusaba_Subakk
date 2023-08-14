using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.Video;

public class videoToEnde : MonoBehaviour
{
     public VideoPlayer videoPlayer;
    public string essentialSceneToLoad;
    public string mainSceneToLoad;
    public Vector3 playerStartPosition;
    public GameObject player;

    void Start()
    {
        videoPlayer.loopPointReached += LoadScene;
    }

    void LoadScene(VideoPlayer vp)
    {
        SceneManager.LoadScene(essentialSceneToLoad);
        SceneManager.LoadScene(mainSceneToLoad, LoadSceneMode.Additive);

        // Pindahkan player ke posisi awal setelah scene di-load
        player.transform.position = playerStartPosition;
    }
}

