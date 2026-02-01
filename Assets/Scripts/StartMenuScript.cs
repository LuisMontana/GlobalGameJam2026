using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using UnityEngine.Video;

public class StartMenuScript : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created

    [SerializeField] private GameObject MainMenu;
    [SerializeField] private GameObject MainPanel;
    [SerializeField] private int sceneId;
    [SerializeField] private string videoFileName;

    public AudioClip[] SFX;
    public AudioSource sourceSFX;
    void Start()
    {
      
    }

    private void Awake()
    {
        PlayVideo();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void StartGame()
    {
        SceneManager.LoadScene(sceneId); //Cargas la escena del juego
    }

    public void quitGame()
    {
        Application.Quit();
    }

    public void PlayVideo()
    {
        VideoPlayer videoPlayer = GetComponent<VideoPlayer>();

        if (videoPlayer != null )
        {
            string videoPath = System.IO.Path.Combine(Application.streamingAssetsPath, videoFileName);
            Debug.Log(videoPath);
            videoPlayer.url = videoPath;
            videoPlayer.Play();

        }
    }

    public void PlaySFX(int soundIndex)
    {
        sourceSFX.PlayOneShot(SFX[soundIndex]);
        Debug.Log("You're hearing: ",SFX[soundIndex]);
    }
}
