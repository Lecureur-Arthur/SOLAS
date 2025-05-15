using UnityEngine;
using TMPro;  // Nécessaire si tu utilises TextMeshPro

[RequireComponent(typeof(Rigidbody))]
public class RobotAspirateur : MonoBehaviour
{
    public float speed = 1.0f;
    public float moveDuration = 3f;
    private float moveTimer = 0f;
    private Rigidbody rb;
    private Vector3 moveDirection;

    // Référence au Canvas et au Text
    public GameObject textCanvas;        // Le Canvas contenant le texte
    public TextMeshProUGUI messageText;  // Le texte qui apparaîtra au-dessus du robot

    void Start()
    {
        rb = GetComponent<Rigidbody>();
        rb.freezeRotation = true;
        moveDirection = transform.forward.normalized;
        textCanvas.SetActive(false);  // Assurez-vous que le texte est initialement caché
    }

    void FixedUpdate()
    {
        // Avancer tout droit
        rb.MovePosition(rb.position - moveDirection * speed * Time.fixedDeltaTime);

        // Vérifier si le temps pour avancer tout droit est écoulé
        moveTimer += Time.fixedDeltaTime;
        if (moveTimer >= moveDuration)
        {
            Debug.Log("Temps écoulé, rotation de 180°");
            TurnAround();  // Faire une rotation de 180°
            moveTimer = 0f;  // Réinitialiser le timer pour la prochaine période de mouvement
        }
    }

    void TurnAround()
    {
        moveDirection = -moveDirection;  // Inverser la direction
        transform.rotation = Quaternion.LookRotation(moveDirection);  // Appliquer la nouvelle rotation
    }

    // Détecter la collision avec le joueur
    void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Player"))  // Si la collision se fait avec l'objet "Player"
        {
            ShowMessage("Vous avez rencontré le robot !");
        }
    }

    // Afficher le texte au-dessus du robot
    void ShowMessage(string message)
    {
        messageText.text = message;           // Définir le texte
        textCanvas.SetActive(true);           // Afficher le Canvas
        StartCoroutine(HideMessage());       // Masquer le texte après un délai
    }

    // Masquer le texte après un certain temps
    System.Collections.IEnumerator HideMessage()
    {
        yield return new WaitForSeconds(3f);  // Attendre 3 secondes avant de cacher le texte
        textCanvas.SetActive(false);          // Cacher le Canvas
    }
}
