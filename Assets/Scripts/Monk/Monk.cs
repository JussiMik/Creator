using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Monk : MonoBehaviour
{
    public GameObject monk;
    public GameManager gameManager;

    void Start()
    {
        gameManager = GameObject.Find("Game Manager").GetComponent<GameManager>();
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.W))
        {
            SpawnNewMonk();
        }
        
    }
    void CheckFarmCount()
    {
            if (gameManager.farms.Count == 0)
            {
                gameManager.devotionDecrease = true;
                //gameManager.devotionDecreaseMp1 = true;

                if (gameManager.monks.Count == 0)
                {
                    gameManager.devotionDecrease = false;
                }
            }

            if (gameManager.monks.Count > 0 && gameManager.farms.Count > 0)
            {
                if (gameManager.monks.Count / gameManager.farms.Count <= 4)
                {
                    gameManager.devotionDecrease = false;
                    gameManager.devotionIncrease = true;

                    if (gameManager.gardens.Count > 0 || gameManager.meditationRooms.Count > 0)
                    {
                        gameManager.devotionIncreaseMp1 = true;
                    }
                }

                if (gameManager.monks.Count / gameManager.farms.Count > 4)
                {
                    gameManager.devotionIncrease = false;
                    gameManager.devotionDecrease = true;
                }

                if (gameManager.monks.Count / gameManager.farms.Count >= 5.7)
                {
                    gameManager.devotionDecreaseMp1 = true;
                }
            }
        
    }
    public void SpawnNewMonk()
    {
        GameObject spawnedMonk = Instantiate(monk, new Vector3(transform.position.x + 2, transform.position.y + 2, transform.position.z), transform.rotation);
        gameManager.monks.Add(spawnedMonk);
    }
}
