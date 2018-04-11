using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Monk : MonoBehaviour
{
    public GameObject monk;
    public GameManager gameManager;

    // Use this for initialization
    void Start()
    {
        gameManager = GameObject.Find("Game Manager").GetComponent<GameManager>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.W))
        {
            SpawnNewMonk();
        }
    }

    public void SpawnNewMonk()
    {
        GameObject spawnedMonk = Instantiate(monk, new Vector3(transform.position.x + 2, transform.position.y + 2, transform.position.z), transform.rotation);
        gameManager.monks.Add(spawnedMonk);

        if (gameManager.monks.Count == gameManager.devotionBuildings.Count)
        {
            gameManager.devotionIncrease = true;
            gameManager.devotionDecrease = false;
            gameManager.devotionDecreaseMp1 = false;
        }

        if (gameManager.monks.Count > gameManager.devotionBuildings.Count)
        {
            gameManager.devotionIncrease = false;
            gameManager.devotionDecrease = true;
            gameManager.devotionDecreaseMp1 = false;
        }

        /*
        if (gameManager.monks.Count * 1.3 > gameManager.devotionBuildings.Count)
        {
            gameManager.devotionIncrease = false;
            gameManager.devotionDecrease = true;
            gameManager.devotionDecreaseMp1 = true;
        }
        */
    }
}
