using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TileManager : MonoBehaviour
{
    #region Unity Declarations

        [SerializeField] private GameObject[] tilePrefabs;

        [Header("Spawn Tile Section")]

        [SerializeField] private float spawnPosition;

        [SerializeField] private float tileLength = 30f;

        [SerializeField] private int spawnedTiles = 5;

        [SerializeField] private Transform playerTransform;

        [SerializeField] private float offsetPlayerDestroyTransform = 25f;

        [Header("Spawn Item Section")]

        [SerializeField] private int maxAmountOfGenerateItem = 4;

        [SerializeField] private float spawnedRange = 10f;

        [SerializeField] private float floatItemOffset = 0.75f;

        [SerializeField] private GameObject[] ingrediantPrefabs;

        [Header("Setup Scene Section")]

        [SerializeField] private float  preparingOffset = 10f;

        [SerializeField] private GameObject preparingAsset;

        private float laneDistanceFromPlayerController;

        private List<GameObject> activeTiles = new List<GameObject>();

        private float xAxisValueToSpawn;

        private Coroutine GetLaneDistanceCoroutine;

        private Coroutine DestroyPreparingPhaseObjectCoroutine;

    #endregion

    #region Unity Methods

        private void Start() 
        {
            DestroyPreparingPhaseObjectCoroutine = StartCoroutine(DestroyPreparingPhaseObject());

            GetLaneDistanceCoroutine = StartCoroutine(GetLaneDistance());
            
            SetupTileSpawner();
        }   

        private void Update() 
        {
            if(playerTransform.position.z - (offsetPlayerDestroyTransform + preparingOffset) > spawnPosition - (spawnedTiles * tileLength))
            {
                SpawnTile(Random.Range(0, tilePrefabs.Length));

                DeleteTile();
            }
        }
    
    #endregion
    
    #region Private Methods

        public void SetupTileSpawner()
        {
            spawnPosition = 0;

            for(int i = 0; i < spawnedTiles; i++)
            {
                if(i == 0)
                {
                    SpawnTile(0);
                }
                else
                {
                    SpawnTile(Random.Range(0, tilePrefabs.Length));
                }
            }
        }
    
        public void SpawnTile(int tileIndex)
        {
            GameObject currentTile = Instantiate(tilePrefabs[tileIndex], (transform.forward * spawnPosition) + new Vector3 (0, 0, preparingOffset), transform.rotation);

            activeTiles.Add(currentTile);

            spawnPosition += tileLength;

            GenerateItem(Random.Range(0, ingrediantPrefabs.Length), Random.Range(0, 3), currentTile);
        }

        public void DeleteTile()
        {
            Destroy(activeTiles[0]);

            activeTiles.RemoveAt(0);
        }

        public void GenerateItem(int index, int spawnedLane, GameObject spawnParentObj)
        {
            switch(spawnedLane)
            {
                case 0:

                    xAxisValueToSpawn = -laneDistanceFromPlayerController;

                    break;

                case 1:

                    xAxisValueToSpawn = 0;

                    break;
                
                case 2:

                    xAxisValueToSpawn = laneDistanceFromPlayerController;

                    break;
            }
            
            int randomAmountOfItem = Random.Range(0, maxAmountOfGenerateItem + 1);

            for(int i = 0; i < randomAmountOfItem; i++)
            {
                GameObject itemSpawned = Instantiate(ingrediantPrefabs[index], spawnParentObj.transform.position + new Vector3(xAxisValueToSpawn, 0.5f + floatItemOffset, i * (spawnedRange / randomAmountOfItem)), transform.rotation);
            
                itemSpawned.transform.SetParent(spawnParentObj.transform);
            }
        }


        IEnumerator GetLaneDistance()
        {   
            while(true)
            {
                laneDistanceFromPlayerController = FixedMovementPlayer.laneDistance;

                yield return new WaitForSeconds(0.1f);

                if(laneDistanceFromPlayerController == FixedMovementPlayer.laneDistance)
                {
                    StopCoroutine(GetLaneDistanceCoroutine);
                }
            }
        }

        IEnumerator DestroyPreparingPhaseObject()
        {
            while(true)
            {
                float delayRate = 1.5f;

                yield return new WaitForSeconds(1f);

                if(playerTransform.position.z - FixedMovementPlayer.Instance.spawnPoint.position.z >= preparingOffset * delayRate)
                {
                    Destroy(preparingAsset);

                    StopCoroutine(DestroyPreparingPhaseObjectCoroutine);
                }
            }
        }
        

    #endregion

}
