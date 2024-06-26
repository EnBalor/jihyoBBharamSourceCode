using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class WarpZone : MonoBehaviour
{
    [SerializeField] string sceneName;

    Vector2 playerPos = new Vector2(-2.4f, -3.3f);

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            AudioManager.instance.PlaySFX("Warp");

            if ("MainScene" == sceneName)
                ++GameManager.Instance.STAGE;

            if (null != GameManager.Instance.Player)
                GameManager.Instance.Player.transform.position = playerPos;

            SceneManager.LoadScene(sceneName);
        }
    }
}
