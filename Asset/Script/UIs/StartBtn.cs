using UnityEngine;
using UnityEngine.SceneManagement;

public class StartBtn : MonoBehaviour
{
    public AudioClip titleBGM;

    private void Start()
    {
        AudioManager.instance.PlayBGM(titleBGM);
    }

    public void RobbyStart()
    {
        AudioManager.instance.PlaySFX("BtnSound");
        SceneManager.LoadScene("RobbyScene");
    }
}
