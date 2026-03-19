using JetBrains.Annotations;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using UnityEngine;

public class LinePath : MonoBehaviour
{
    [Header("Main variable")]
    public LineRenderer line; // To draw line. Use to draw arrow line
    public Transform[] pionts; // pionts where to draw arrow line
    public Transform[] followPath; // path for arrrow to follow
    public Sprite arrowSprite;       // Head of the arrow 

    [Header("Attribute of Arrow")]
    public float lineWidth = 0.35f;
    public float arrowSize = 0.4f;
    public float speed = 3f;
    public float segmentSpacing = 0.5f;

    [Header("Arrow")]
    GameObject arrow;
    EdgeCollider2D edge;

    [Header("Arrow colours")]
    public Color normalColor = Color.black;
    public Color activeColor = new Color(0f, 0.5f, 0f);
    public Color failColor = Color.red;

    Vector3 startPosition;
    public SpriteRenderer arrowRenderer;
    //BoxCollider2D clickCollider;


    bool move = false;
    int index = 1;
    int maxTrailLength;

    public enum ArrowDirection
    {
        Top,
        Bottom,
        Left,
        Right
    }

    public ArrowDirection arrowDirection;

    List<Vector3> trail = new List<Vector3>();
    Vector3 lastArrowPos;

    // Update is called once per frame

    
    void Start()
    {
        lineWidth = 0.20f;

        line.startWidth = lineWidth;
        line.endWidth = lineWidth;
        line.material = new Material(Shader.Find("Sprites/Default"));

        // YAHAN ADD KARNA HAI
        line.numCornerVertices = 8;
        line.numCapVertices = 8;


        speed = 15;
        //clickCollider = gameObject.AddComponent<BoxCollider2D>();

        // initial line
        foreach (Transform p in points)
            trail.Add(p.position);

        UpdateLine();

        arrow = new GameObject("Arrow");
        arrow.transform.SetParent(transform);   // IMPORTANT

        SpriteRenderer sr = arrow.AddComponent<SpriteRenderer>();

        sr.sprite = arrowSprite;
        sr.sortingOrder = 10;

        arrow.transform.localScale = Vector3.one * arrowSize;
        arrow.transform.position = points[0].transform.position;
        arrow.AddComponent<BoxCollider2D>();
        arrowRenderer = arrow.GetComponent<SpriteRenderer>();
        arrowRenderer.color = normalColor;
        startPosition = arrow.transform.position;
        lastArrowPos = arrow.transform.position;

        edge = gameObject.AddComponent<EdgeCollider2D>();
        UpdateCollider();
        maxTrailLength = trail.Count;


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


        // YAHAN PASTE KARNA HAI (Start() ke end par)

        int theme = PlayerPrefs.GetInt("Theme", 0);

        if (theme == 1) // dark mode
        {
            ThemeManager tm = FindObjectOfType<ThemeManager>();

            if (tm != null)
            {
                tm.ApplyTheme();
            }
        }

        // YAHAN PASTE KARNA HAI (Start() function ke andar)

        foreach (Transform p in points)
        {
            SpriteRenderer sri = p.GetComponent<SpriteRenderer>();

            if (sri != null)
            {
                Color c = sri.color;
                c.a = 0f;   // alpha kam (0 = invisible, 1 = full)
                sri.color = c;
            }
        }




    }

    
    void Update()
    {
     
    }
}
