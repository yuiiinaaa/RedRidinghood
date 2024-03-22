using UnityEngine;
using UnityEngine.UI;

public class UIRotation : MonoBehaviour
{
    public float rotationSpeed = 20f; // Adjust the speed of rotation as needed
    private RectTransform rectTransform;

    void Start()
    {
        rectTransform = GetComponent<RectTransform>();
    }

    void Update()
    {
        // Rotate the RectTransform continuously
        rectTransform.Rotate(0f, 0f, rotationSpeed * Time.deltaTime);
    }
}
