using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class MapEffect : MonoBehaviour
{
    private Tilemap tilemap;
    private Dictionary<Vector3Int, Color> originalColors = new Dictionary<Vector3Int, Color>();

    // Gi� tr? alpha t??ng ?ng (100/255 = ~0.39)
    public float transparentAlpha = 100f / 255f;  // ?? trong su?t (t? 0 ??n 1)

    void Start()
    {
        tilemap = GetComponent<Tilemap>();
        if (tilemap != null)
        {
            // L?u l?i m�u s?c ban ??u c?a t?t c? c�c � trong Tilemap
            foreach (var pos in tilemap.cellBounds.allPositionsWithin)
            {
                if (tilemap.HasTile(pos))
                {
                    originalColors[pos] = tilemap.GetColor(pos);
                }
            }
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            SetTransparency(transparentAlpha);  
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            SetTransparency(1f);  
        }
    }

    void SetTransparency(float alpha)
    {
        if (tilemap != null)
        {
            foreach (var pos in tilemap.cellBounds.allPositionsWithin)
            {
                if (tilemap.HasTile(pos))
                {
                    Color originalColor = originalColors[pos];
                    Color newColor = new Color(originalColor.r, originalColor.g, originalColor.b, alpha);
                    tilemap.SetTileFlags(pos, TileFlags.None);  // Cho ph�p ch?nh s?a m�u s?c �
                    tilemap.SetColor(pos, newColor);
                }
            }
        }
    }
}
