using System.Collections.Generic;
using UnityEngine;

public class TileManager : MonoBehaviour
{
    private List<GameObject> activeTiles;
    public GameObject[] tilePrefabs;
    public float tileLength = 60;
    public int numberOfTiles = 5;
    public int totalNumOfTiles = 6;
    public float zSpawn = 0;
    public Transform playerTransform;
    //private int previousIndex;

    void Start()
    {

        activeTiles = new List<GameObject>();
        for (int i = 0; i < numberOfTiles; i++)
        {
            if(i==0)
                SpawnTile(0);
            else
                SpawnTile(Random.Range(0, totalNumOfTiles));
        }

        // playerTransform = GameObject.FindGameObjectWithTag("Player").transform;

    }
    void Update()
    {
        if(playerTransform.position.z - 65 > zSpawn - (numberOfTiles * tileLength))
         {
             SpawnTile(Random.Range(0, totalNumOfTiles));
        //     int index = Random.Range(0, totalNumOfTiles);
        //     while(index == previousIndex)
        //         index = Random.Range(0, totalNumOfTiles);

             DeleteTile();
        //     SpawnTile(index);
         }
            
    }

    public void SpawnTile(int titleIndex)
    {
        GameObject go = Instantiate(tilePrefabs[titleIndex], transform.forward * zSpawn, transform.rotation);
        //Instantiate(tilePrefabs[titleIndex], transform.forward * zSpawn, transform.rotation);
        activeTiles.Add(go);
        zSpawn += tileLength;
        // GameObject tile = tilePrefabs[index];
        // if (tile.activeInHierarchy)
        //     tile = tilePrefabs[index + 8];

        // if(tile.activeInHierarchy)
        //     tile = tilePrefabs[index + 16];

        // tile.transform.position = Vector3.forward * zSpawn;
        // tile.transform.rotation = Quaternion.identity;
        // tile.SetActive(true);

        // activeTiles.Add(tile);
        // zSpawn += tileLength;
        // previousIndex = index;
    }

    private void DeleteTile()
    {
         //activeTiles[0].SetActive(false);
         Destroy(activeTiles[0]);
         activeTiles.RemoveAt(0);
        //PlayerManager.score += 3;
    }
}
