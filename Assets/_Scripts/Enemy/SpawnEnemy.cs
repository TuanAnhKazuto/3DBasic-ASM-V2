using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class SpawnEnemy : MonoBehaviour
{
    public GameObject enemyPrefab;
    [HideInInspector] public int xPos;
    public int yPos;
    [HideInInspector] public int zPos;
    public int xPosStart;
    public int xPosEnd;
    public int zPosStart;
    public int zPosEnd;
    [HideInInspector] public int _enemyCount;
    public int enemyCount;

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            StartCoroutine(EnemySpawn());
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
    }
}
