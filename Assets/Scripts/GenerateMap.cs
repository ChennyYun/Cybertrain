using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;
using UnityEngine.SceneManagement;

public class GenerateMap : MonoBehaviour
{

    public Tilemap map;

    public Tilemap Ground;
    public TileBase tile;
    private int difficulty;

    private string trainType;

    public GameObject[] powerUps;
    public GameObject[] enemies;
    public Vector2 a;
    public Vector2 b;
    public Vector2 c;
    public Vector2 d;


    void Start()
    {

        difficulty = GameManager.Instance.difficulty;
        trainType = SceneManager.GetActiveScene().name;
        List<int[,]> domains = new List<int[,]>();

        int startX = 1, endX = 10;
        int startY = 1, endY = 6;

        domains.Add(addToDomain(startX, startY, endX, endY, true));

        if (trainType == "SmallTrain")
        {
            for (int i = 0; i < difficulty; i++)
            {
                domains.Add(addToDomain(1, 1, 9, 5, false));
            }
        }
        else if (trainType == "NormalTrain")
        {
            for (int i = 0; i < difficulty * 2; i++)
            {
                domains.Add(addToDomain(-1, -1, 20, 15, false));
            }
        }
        else if (trainType == "LongTrain")
        {
            for (int i = 0; i < difficulty * 3; i++)
            {
                domains.Add(addToDomain(-15, -10, 24, 5, false));
            }
        }
        Debug.Log(domains.Count);
        int n = domains.Count;
        int[,] assignment = new int[n, 2];
        Solver solver = new Solver(domains, n, map,a,b,c,d);
        int[,] solution = solver.Solve(0, assignment);

        int randomIndex = Random.Range(0, powerUps.Length);
        Debug.Log(solution);
        GameObject PowerUp = Instantiate(powerUps[randomIndex],new Vector3(solution[0,0], solution[0,1],0), Quaternion.identity);
        
        for (int i = 1; i < solution.GetLength(0); i++)
        {
            randomIndex = Random.Range(0, enemies.Length);
            GameObject Enemy = Instantiate(enemies[randomIndex],new Vector3(solution[i,0], solution[i,1],0), Quaternion.identity);
        }
    }

    private int[,] addToDomain(int startX, int startY, int endX, int endY, bool isPowerUp)
    {
        if (isPowerUp)
        {
            int[,] powerUpDomain = new int[(endX - startX + 1) * ( endY - startY + 1) + 1, 2];
            int index = 0;
            for (int x = startX; x <= endX; x++)
            {
                for (int y = startY; y <= endY; y++)
                {
                    powerUpDomain[index, 0] = x;
                    powerUpDomain[index, 1] = y;
                    index++;
                }
            }
            powerUpDomain[index, 0] = -1;
            powerUpDomain[index, 1] = -1;

            // Adding the generated domain for an object
            return powerUpDomain;
        }
        else
        {
            int[,] enemyDomain = new int[(endX - startX + 1) * (endY - startY + 1) + 1, 2];
            int index = 0;
            for (int x = startX; x <= endX; x++)
            {
                for (int y = startY; y <= endY; y++)
                {
                    enemyDomain[index, 0] = x;
                    enemyDomain[index, 1] = y;
                    index++;
                }
            }
            enemyDomain[index, 0] = -1;
            enemyDomain[index, 1] = -1;
            return enemyDomain;
        }
    }

}

