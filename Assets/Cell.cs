using UnityEngine;

public class Cell : MonoBehaviour
{
    public bool isAlive;
    private SpriteRenderer spriteRenderer;
    private Color wallColor;
    private Color floorColor;

    void Awake()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        if (spriteRenderer == null)
        {
            Debug.LogError("SpriteRenderer is missing from Cell prefab");
        }
    }

    public void Initialize(bool alive, Color wallColor, Color floorColor)
    {
        this.wallColor = wallColor;
        this.floorColor = floorColor;
        SetState(alive);
    }

    public void SetState(bool alive)
    {
        isAlive = alive;
        UpdateColor();
    }

    void UpdateColor()
    {
        if (spriteRenderer != null)
        {
            spriteRenderer.color = isAlive ? wallColor : floorColor;
        }
    }
}
