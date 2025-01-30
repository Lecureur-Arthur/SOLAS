using UnityEngine;

public class ChangementScene : MonoBehaviour
{
    [SerializeField] private GameObject objectToBeSpawned;
    [SerializeField] private int numberOfItems;
    [SerializeField] private Transform parent;

    [SerializeField] private Vector3 spawnCenter = Vector3.zero;
    [SerializeField] private float innerRadius = 1f; // The inner sphere (no-spawn zone)
    [SerializeField] private float outerRadius = 3f; // The outer sphere (spawn zone)

    void Start()
    {
        for (int i = 0; i < numberOfItems; i++)
        {
            Vector3 spawnPosition;

            do
            {
                // Generate a random position inside the outer sphere
                spawnPosition = spawnCenter + Random.insideUnitSphere * innerRadius;
            }
            // Ensure spawn position is outside the inner sphere (no spawn within innerRadius)
            while (Vector3.Distance(spawnCenter, spawnPosition) > outerRadius);

            // Spawn object with fixed rotation
            Instantiate(objectToBeSpawned, spawnPosition, Quaternion.Euler(85, 0, 0), parent);

  
        }
    }
}
