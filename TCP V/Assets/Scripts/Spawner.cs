using UnityEngine;
using System.Collections;

public class Spawner : MonoBehaviour
{
    public GameObject[] prefabs; // Array com os 3 prefabs
    public float spawnCooldown = 20f; // Tempo de espera entre os spawns
    public Vector2 spawnAreaMin; // Limite inferior esquerdo da �rea de spawn
    public Vector2 spawnAreaMax; // Limite superior direito da �rea de spawn

    private void Start()
    {
        StartCoroutine(SpawnRoutine());
    }

    IEnumerator SpawnRoutine()
    {
        while (true)
        {
            yield return new WaitForSeconds(spawnCooldown);
            SpawnRandomObject();
        }
    }

    void SpawnRandomObject()
    {
        if (prefabs.Length == 0) return;

        // Escolhe um prefab aleat�rio
        GameObject prefabToSpawn = prefabs[Random.Range(0, prefabs.Length)];

        // Define uma posi��o aleat�ria dentro da �rea de spawn
        float randomX = Random.Range(spawnAreaMin.x, spawnAreaMax.x);
        float randomY = Random.Range(spawnAreaMin.y, spawnAreaMax.y);
        Vector2 spawnPosition = new Vector2(randomX, randomY);

        // Instancia o prefab na posi��o calculada
        Instantiate(prefabToSpawn, spawnPosition, Quaternion.identity);
    }
}
