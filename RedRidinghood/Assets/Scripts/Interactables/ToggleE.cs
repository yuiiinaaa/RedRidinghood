using UnityEngine;

public class EnableDisableSpriteRenderer : MonoBehaviour
{
    public bool enableSpriteRenderer = false;
    private SpriteRenderer spriteRenderer;

    private void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>(); // Get the SpriteRenderer component attached to this GameObject
        spriteRenderer.enabled = false;
    }

    private void Update()
    {
        if (GameManager.Instance.GetToggleInteractor() == true && !GameManager.Instance.GetCutsceneTrigger())
        {
            spriteRenderer.enabled = true;
        }
        else
        {
            spriteRenderer.enabled = false;
        }
    }
}
