using UnityEngine;

public class BossGenerator : MonoBehaviour
{
    [SerializeField] private GameObject[] bossGameObjects;

    private void Awake()
    {
        if (1 >= GameManager.Instance.STAGE)
        {
            bossGameObjects[0].SetActive(true);
        }
        else if (2 >= GameManager.Instance.STAGE)
        {
            bossGameObjects[1].SetActive(true);
        }
        else
        {
            int index = Random.Range(0, 2);

            bossGameObjects[index].SetActive(true);
        }
    }
}