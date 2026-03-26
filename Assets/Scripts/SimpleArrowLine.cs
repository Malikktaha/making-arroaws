using System.Collections.Generic;
using UnityEngine;

public class SimpleArrowLine : MonoBehaviour
{
    public LineRenderer line;           // LineRenderer component
    public Transform[] points;          // Points for the line
    public Sprite arrowSprite;          // Arrow sprite
    public float lineWidth = 0.2f;      // Line thickness
    public float arrowSize = 0.4f;      // Arrow size
    public enum ArrowDirection { Top, Bottom, Left, Right }
    public ArrowDirection arrowDirection = ArrowDirection.Top;

    private GameObject arrow;           // Arrow instance

    void Start()
    {
        DrawLine();
        PlaceArrow();
    }

    void DrawLine()
    {
        if (points.Length == 0 || line == null) return;

        line.positionCount = points.Length;
        line.startWidth = lineWidth;
        line.endWidth = lineWidth;
        line.material = new Material(Shader.Find("Sprites/Default"));

        for (int i = 0; i < points.Length; i++)
        {
            line.SetPosition(i, points[i].position);
        }

        // Optional: smooth corners
        line.numCornerVertices = 8;
        line.numCapVertices = 8;
    }

    void PlaceArrow()
    {
        if (points.Length == 0 || arrowSprite == null) return;

        // Create arrow object
        arrow = new GameObject("Arrow");
        arrow.transform.SetParent(transform);

        SpriteRenderer sr = arrow.AddComponent<SpriteRenderer>();
        sr.sprite = arrowSprite;
        sr.sortingOrder = 10;

        // Scale arrow properly
        arrow.transform.localScale = Vector3.one * arrowSize;

        // Place on the top (last) point
        Transform topPoint = points[points.Length - 1];
        arrow.transform.position = topPoint.position;

        // Set arrow rotation
        switch (arrowDirection)
        {
            case ArrowDirection.Top:
                arrow.transform.rotation = Quaternion.Euler(0, 0, 0);
                break;
            case ArrowDirection.Bottom:
                arrow.transform.rotation = Quaternion.Euler(0, 0, 180);
                break;
            case ArrowDirection.Left:
                arrow.transform.rotation = Quaternion.Euler(0, 0, 90);
                break;
            case ArrowDirection.Right:
                arrow.transform.rotation = Quaternion.Euler(0, 0, -90);
                break;
        }
    }
}