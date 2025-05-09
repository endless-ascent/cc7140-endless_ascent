using UnityEngine;

public class CameraBob : MonoBehaviour
{
    public float bobFrequency = 10f;
    public float bobAmplitude = 0.05f;
    public float moveThreshold = 0.1f;

    private Vector3 initialLocalPosition;
    private Transform playerTransform;
    private Rigidbody2D playerRb;

    void Start()
    {
        playerTransform = transform.parent; // pega o Player (pai da câmera)
        playerRb = playerTransform.GetComponent<Rigidbody2D>();
        initialLocalPosition = transform.localPosition;
    }

    void Update()
    {
        if (playerRb == null) return;

        // Usa velocidade para determinar se o player está andando
        float horizontalSpeed = Mathf.Abs(playerRb.linearVelocity.x);


        if (horizontalSpeed != moveThreshold)
        {
            float bobOffset = Mathf.Sin(Time.time * bobFrequency) * bobAmplitude;
            transform.localPosition = new Vector3(
                initialLocalPosition.x,
                initialLocalPosition.y + bobOffset,
                initialLocalPosition.z
            );
        }
        else
        {
            // Volta suavemente à posição original
            transform.localPosition = Vector3.Lerp(transform.localPosition, initialLocalPosition, Time.deltaTime * 5f);
        }
    }
}