using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ResourceSpawner : MonoBehaviour
{
    public Transform spawnStart;
    public Transform spawnEnd;
    public Transform spawnStartEnemy;
    public Transform spawnEndEnemy;
    public Vector2 spawnRange;
    private float randomPositionX;
    public List<GameObject> stones;
    public List<GameObject> trees;
    public List<GameObject> golds;
    public List<GameObject> diamonds;
    public List<GameObject> enemies;
    public int resourceQuantity = 100;
    public int enemyQuantity = 10;
    public int resourceCounter;
    public int enemyCounter;
    public LayerMask terrainAndPlayerLayer;


    // Start is called before the first frame update
    void Start()
    {
        spawnRange = new Vector2(spawnStart.position.x, spawnEnd.position.x);
        randomPositionX = Random.Range(spawnRange.x, spawnRange.y);
        SpawnResources();
        SpawnEnemies();
    }
    public void SpawnEnemies()
    {
        if (enemyCounter >= enemyQuantity)
        {
            return;
        }
        int randomNum = Random.Range(1, 3);
        if(randomNum == 1)
        {
            randomPositionX = Random.Range(spawnRange.x, spawnEndEnemy.position.x);
        }
        else if(randomNum == 2)
        {
             randomPositionX = Random.Range(spawnStartEnemy.position.x, spawnRange.y);
        }
        RaycastHit2D hit = Physics2D.Raycast(new Vector2(randomPositionX, 100), Vector2.down, 200f, terrainAndPlayerLayer);
        if (hit.collider.gameObject.layer == 6)
        {
            int randomNumber = Random.Range(1, 100);

            if (randomNumber <= 50)
            {
                Instantiate(enemies[0], hit.point, Quaternion.identity);
                enemyCounter++;
                SpawnEnemies();
            }
            else if (randomNumber > 50 && randomNumber <= 95)
            {
                Instantiate(enemies[1], hit.point, Quaternion.identity);
                enemyCounter++;
                SpawnEnemies();
            }
            else
            {
                Instantiate(enemies[2], hit.point, Quaternion.identity);
                enemyCounter++;
                SpawnEnemies();
            }
        }
        else
        {
            SpawnEnemies();
        }
    }
    public void EnemyRespawn()
    {
        int randomNum = Random.Range(1, 3);
        if (randomNum == 1)
        {
            randomPositionX = Random.Range(spawnRange.x, spawnEndEnemy.position.x);
        }
        else if (randomNum == 2)
        {
            randomPositionX = Random.Range(spawnStartEnemy.position.x, spawnRange.y);
        }
        RaycastHit2D hit = Physics2D.Raycast(new Vector2(randomPositionX, 100), Vector2.down, 200f, terrainAndPlayerLayer);
        if (hit.collider.gameObject.layer == 6)
        {
            int randomNumber = Random.Range(1, 100);

            if (randomNumber <= 50)
            {
                Instantiate(enemies[0], hit.point, Quaternion.identity);

            }
            else if (randomNumber > 50 && randomNumber <= 95)
            {
                Instantiate(enemies[1], hit.point, Quaternion.identity);
            }
            else
            {
                Instantiate(enemies[2], hit.point, Quaternion.identity);
            }
        }
        else
        {
            SpawnEnemies();
        }
    }
    public IEnumerator EnemyRespawnDelay()
    {
        yield return new WaitForSeconds(120f);
        EnemyRespawn();
    }
    public void SpawnResources()
    {
        if(resourceCounter >= resourceQuantity)
        {
            return;
        }
        randomPositionX = Random.Range(spawnRange.x, spawnRange.y);
        RaycastHit2D hit = Physics2D.Raycast(new Vector2(randomPositionX, 100), Vector2.down, 200f);
        if (hit.collider.gameObject.layer == 6)
        {
            int randomNumber = Random.Range(1, 100);

            if (randomNumber <= 40)
            {
                int randomTree = Random.Range(0, trees.Count - 1);
                Instantiate(trees[randomTree], hit.point, Quaternion.identity);
                resourceCounter++;
                SpawnResources();

            }
            else if (randomNumber > 40 && randomNumber <= 80)
            {
                int randomStone = Random.Range(0, stones.Count - 1);
                Instantiate(stones[randomStone], hit.point, Quaternion.identity);
                resourceCounter++;
                SpawnResources();
            }
            else if (randomNumber > 80 && randomNumber <= 95)
            {
                int randomGold = Random.Range(0, golds.Count - 1);
                Instantiate(golds[randomGold], hit.point, Quaternion.identity);
                resourceCounter++;
                SpawnResources();
            }
            else
            {
                int randomDiamond = Random.Range(0, diamonds.Count - 1);
                Instantiate(diamonds[randomDiamond], hit.point, Quaternion.identity);
                resourceCounter++;
                SpawnResources();
            }
        }
        else
        {
            SpawnResources();
        }
    }

    public void Respawn()
    {
        randomPositionX = Random.Range(spawnRange.x, spawnRange.y);
        RaycastHit2D hit = Physics2D.Raycast(new Vector2(randomPositionX, 100), Vector2.down, 200f);
        if (hit.collider.gameObject.layer == 6)
        {
            int randomNumber = Random.Range(1, 100);

            if (randomNumber <= 40)
            {
                int randomTree = Random.Range(0, trees.Count - 1);
                Instantiate(trees[randomTree], hit.point, Quaternion.identity);
            }
            else if (randomNumber > 40 && randomNumber <= 80)
            {
                int randomStone = Random.Range(0, stones.Count - 1);
                Instantiate(stones[randomStone], hit.point, Quaternion.identity);
                
            }
            else if (randomNumber > 80 && randomNumber <= 95)
            {
                int randomGold = Random.Range(0, golds.Count - 1);
                Instantiate(golds[randomGold], hit.point, Quaternion.identity);
                
            }
            else
            {
                int randomDiamond = Random.Range(0, diamonds.Count - 1);
                Instantiate(diamonds[randomDiamond], hit.point, Quaternion.identity);
               
            }
        }
        else
        {
            Respawn();
        }
    }

    public IEnumerator RespawnDelay()
    {
        yield return new WaitForSeconds(60);
        Respawn();
    }
}
