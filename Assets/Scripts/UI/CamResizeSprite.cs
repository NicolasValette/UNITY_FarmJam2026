using UnityEngine;

[ExecuteAlways]
[RequireComponent(typeof(SpriteRenderer))]
public class FitSpriteToCamera : MonoBehaviour
{
    [SerializeField] private Camera targetCamera;
    [SerializeField] private bool preserveAspect = false;

    private SpriteRenderer spriteRenderer;

    private void OnEnable()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();

        if (targetCamera == null)
            targetCamera = Camera.main;

        Resize();
    }

    private void LateUpdate()
    {
        Resize();
    }

    private void Resize()
    {
        if (targetCamera == null ||
            !targetCamera.orthographic ||
            spriteRenderer == null ||
            spriteRenderer.sprite == null)
        {
            return;
        }

        float cameraHeight = targetCamera.orthographicSize * 2f;
        float cameraWidth = cameraHeight * targetCamera.aspect;

        Vector2 spriteSize = spriteRenderer.sprite.bounds.size;

        float scaleX = cameraWidth / spriteSize.x;
        float scaleY = cameraHeight / spriteSize.y;

        if (preserveAspect)
        {
            float scale = Mathf.Max(scaleX, scaleY);
            scaleX = scale;
            scaleY = scale;
        }

        transform.localScale = new Vector3(scaleX, scaleY, 1f);

        Vector3 cameraPosition = targetCamera.transform.position;
        transform.position = new Vector3(
            cameraPosition.x,
            cameraPosition.y,
            transform.position.z
        );
    }
}