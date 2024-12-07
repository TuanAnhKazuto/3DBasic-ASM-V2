using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class SpawnEnemy : MonoBehaviour
{
    public GameObject enemyPrefab;
    public GameObject waveAreaInMinimap;
    public GameObject waveAreaInFullmap;
    [HideInInspector] public int xPos;
    public float yPos;
    [HideInInspector] public int zPos;
    public int xPosStart;
    public int xPosEnd;
    public int zPosStart;
    public int zPosEnd;
    [HideInInspector] public int _enemyCount;
    public int enemyCount;

    private void Start()
    {
        waveAreaInMinimap.SetActive(true);
        waveAreaInFullmap.SetActive(true);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            StartCoroutine(EnemySpawn());
            waveAreaInMinimap.SetActive(false);
            waveAreaInFullmap.SetActive(false);
        }
    }

    IEnumerator EnemySpawn()
    {
        while(_enemyCount < enemyCount)
        {
            xPos = Random.Range(xPosStart, xPosEnd);
            zPos = Random.Range(zPosStart, zPosEnd);
            Instantiate(enemyPrefab, new Vector3(xPos, yPos, zPos), Quaternion.identity);
            yield return new WaitForSeconds(0.01f);
            _enemyCount++;
        }
        yield return new WaitForSeconds(0.1f);
        Destroy(gameObject);
    }
}
