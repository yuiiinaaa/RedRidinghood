using UnityEngine;
using UnityEngine.UI;

public class DisableUIImage : MonoBehaviour
{
    public bool disableImage = false;
    private Image imageComponent;

    private void Start()
    {
        imageComponent = GetComponent<Image>(); // Get the Image component attached to this GameObject
        imageComponent.enabled = false;
    }

    private void Update()
    {
        if(GameManager.Instance.GetToggleInteractor() == true){
            imageComponent.enabled = true;

        }else{
            imageComponent.enabled = false;
        } 
    }
}
