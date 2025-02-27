using UnityEngine;

public class ChangementScene : MonoBehaviour
{
    [SerializeField] private GameObject objectToBeSpawned; // The cube prefab
    [SerializeField] private int numberOfItems = 20; // Number of cubes
    [SerializeField] protected Transform parent;

    [SerializeField] private Vector3 spawnCenter = Vector3.zero; // Center of the cone base
    [SerializeField] private float baseRadius = 15f; // Radius at the base
    [SerializeField] private float coneHeight = 25f; // Height of the cone
    [SerializeField] private float globalRotationX = 90f; // Rotate the entire cone
    [SerializeField] private float spinSpeed = 1000f; // Rotation speed (increase for faster spinning)

    private Transform coneParent; // Holds all cubes for rotation

    void Start()
    {
        // Create a new empty GameObject to rotate all spawned objects
        coneParent = new GameObject("ConeParent").transform;
        coneParent.position = spawnCenter;
    }

    void Update()
    {
        // Rotate the entire cone fast around the Y-axis
        coneParent.Rotate(Vector3.forward * spinSpeed * Time.deltaTime);
    }

    public void SpawnObject()
    {

        Quaternion globalRotation = Quaternion.Euler(globalRotationX, 0, 0); // Apply global X-axis rotation

        for (int i = 0; i < numberOfItems; i++)
        {
            float angle = Random.Range(0f, 360f) * Mathf.Deg2Rad; // Random angle around the cone
            float heightFactor = Random.Range(0.2f, 0.6f); // Avoid placing cubes too close to the tip

            // Compute spawn position in the cone
            float radiusAtHeight = baseRadius * (1f - heightFactor); // Shrinks as height increases
            float x = spawnCenter.x + radiusAtHeight * Mathf.Cos(angle);
            float z = spawnCenter.z + radiusAtHeight * Mathf.Sin(angle);
            float y = spawnCenter.y + (heightFactor * coneHeight); // Height of the cone

            Vector3 spawnPosition = new Vector3(x, y, z);

            // Compute the cone's outward normal
            Vector3 coneNormal = (spawnPosition - spawnCenter).normalized;
            coneNormal.y = coneHeight / baseRadius; // Adjust y for a more natural cone shape
            coneNormal.Normalize(); // Normalize the vector again

            // **Rotate cube outward at a 45° angle**
            Quaternion cubeRotation = Quaternion.LookRotation(coneNormal) * Quaternion.Euler(45, 0, 0);

            // **Apply global rotation to the entire formation**  
            spawnPosition = globalRotation * (spawnPosition - spawnCenter) + spawnCenter;
            cubeRotation = globalRotation * cubeRotation;

            // Instantiate the object and parent it under `coneParent` so it rotates together
            GameObject cube = Instantiate(objectToBeSpawned, spawnPosition, cubeRotation, coneParent);
        }
    }
}