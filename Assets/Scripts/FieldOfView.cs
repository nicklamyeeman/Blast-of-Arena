using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FieldOfView : MonoBehaviour
{
    [SerializeField] private LayerMask layerMask;
    [SerializeField] private LayerMask layerMask2;
    private Mesh mesh;
    private Vector3 origin;
    private Vector3 originInCollider;

    private readonly float fov = 360;
    private readonly float viewDistance = 40f;

    void Start()
    {
        origin = Vector3.zero;
        mesh = new Mesh();
        GetComponent<MeshFilter>().mesh = mesh;
    }

    private void LateUpdate()
    {
        int rayCount = 100;
        float angle = 0f;
        float angleIncrease = fov / rayCount;

        Vector3[] vertices = new Vector3[rayCount + 1 + 1];
        Vector2[] uv = new Vector2[vertices.Length];
        int[] triangles = new int[rayCount * 3];

        vertices[0] = origin;

        int vertexIndex = 1;
        int triangleIndex = 0;
        for (int i = 0; i <= rayCount; i += 1)
        {
            Vector3 vertex;
            RaycastHit2D rayCast;
            RaycastHit2D rayInCollider;
            rayCast = Physics2D.Raycast(origin, GetVectorFromAngle(angle), viewDistance, layerMask);
            if (rayCast.collider == null)
            {
                vertex = origin + GetVectorFromAngle(angle) * viewDistance;
            }
            else
            {
                float magnitude = 0;
                originInCollider = rayCast.point;
                rayInCollider = Physics2D.Raycast(originInCollider, GetVectorFromAngle(angle), viewDistance, layerMask);
                while (rayInCollider.collider != null && rayCast.collider.transform.GetInstanceID() == rayInCollider.collider.transform.GetInstanceID()) {
                    originInCollider.x += GetVectorFromAngle(angle).x / 12;
                    originInCollider.y += GetVectorFromAngle(angle).y / 12;
                    magnitude += GetVectorFromAngle(angle).magnitude / 12f;
                    rayInCollider = Physics2D.Raycast(originInCollider, GetVectorFromAngle(angle), viewDistance, layerMask);
                }
                vertex = origin + GetVectorFromAngle(angle) * (rayCast.distance + magnitude);
                //Debug.Log("Vector from angle: " + GetVectorFromAngle(angle) + " Hitted Object: " + vertex + " Object's collider origin: " + originInCollider + " Name of hitted object: " + rayCast.collider.transform.name);
            }
            vertices[vertexIndex] = vertex;

            if (i > 0)
            {
                triangles[triangleIndex + 0] = 0;
                triangles[triangleIndex + 1] = vertexIndex - 1;
                triangles[triangleIndex + 2] = vertexIndex;
                triangleIndex += 3;
            }
            vertexIndex += 1;
            angle -= angleIncrease;
        }
        mesh.vertices = vertices;
        mesh.uv = uv;
        mesh.triangles = triangles;
    }

    public void SetOrigin(Vector3 origin)
    {
        origin.y -= 1;
        this.origin = origin;
    }

    public void SetAimDirection(Vector3 aimDirection)
    {

    }

    private Vector3 GetVectorFromAngle(float angle)
    {
        float angleRad = angle * (Mathf.PI / 180f);
        return new Vector3(Mathf.Cos(angleRad), Mathf.Sin(angleRad));
    }
}
