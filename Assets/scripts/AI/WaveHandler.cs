using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaveHandler : MonoBehaviour
{
    [System.Serializable]
    public struct Wave
    {
        public GameObject enemyPrefab;
        public int count;
        public float rate;
    }

    public Wave[] wave;
    public Transform spawnPoint;
    public int currentWaveIndex;

    public static List<GameObject> aliveEnemies = new List<GameObject>();

    private void Awake()
    {
        spawnPoint = GameObject.FindGameObjectWithTag("Spawn Point").GetComponent<Transform>();
    }
    IEnumerator SpawnWave(Wave wave)
    {
        for (int i = 0; i < wave.count; i++)
        {
            GameObject enemy = Instantiate(wave.enemyPrefab, spawnPoint.position,spawnPoint.rotation);

            aliveEnemies.Add(enemy);

            yield return new WaitForSeconds(1f/wave.rate);
        }
    }
    private void OnTriggerEnter(Collider other)
    {
         if (other.gameObject.tag == "Goal")
        {
            Debug.Log("Hit goal");
            Destroy(this.gameObject);
        }
    }
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.S))
        {
            if (!(aliveEnemies.Count > 0))
            {
                StartCoroutine(SpawnWave(wave[currentWaveIndex]));
                currentWaveIndex++;
            }
        }
    }

}
