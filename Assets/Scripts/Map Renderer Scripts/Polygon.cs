using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[RequireComponent(typeof(EdgeCollider2D))]
public class Polygon : MonoBehaviour
{
    [SerializeField] private int sides = 5;
    [SerializeField] private float radius = 3;
    public LineRenderer polygonRenderer;
    [SerializeField] private int extraSteps = 2;
    [SerializeField] private bool isLooped;
    [SerializeField] private float width;

    [SerializeField]EdgeCollider2D edgeCollider;
    private List<Vector2> points;
    private void Start()
    {
        edgeCollider = this.GetComponent<EdgeCollider2D>();
        edgeCollider.transform.position -= transform.position;
    }
    void Update()
    {
        if (isLooped)
        {
            DrawLooped();
        }
        else
        {
            DrawOverlapped();
        }
    }
    void DrawLooped()
    {
        List<Vector2> edges = new List<Vector2>();
        polygonRenderer.positionCount = sides;
        float TAU = 2 * Mathf.PI;

        for(int currentPoint = 0; currentPoint < sides; currentPoint++)
        {
            float currentRadian = ((float)currentPoint / sides) * TAU;
            float x = Mathf.Cos(currentRadian) * radius;
            float y = Mathf.Sin(currentRadian) * radius;
            polygonRenderer.SetPosition(currentPoint, new Vector3(x, y, 0));
            edges.Add(new Vector3(x, y, 0));
        }
        edgeCollider.SetPoints(edges);
        edgeCollider.points = edges.ToArray();
        polygonRenderer.loop = true;
    }
    void DrawOverlapped()
    {         
        DrawLooped();
        polygonRenderer.loop = false;
        polygonRenderer.positionCount += extraSteps;

        int positionCount = polygonRenderer.positionCount;
        for(int i = 0; i < extraSteps; i++)
        {
            polygonRenderer.SetPosition((positionCount - extraSteps + i), polygonRenderer.GetPosition(i));
        }
    }
}
