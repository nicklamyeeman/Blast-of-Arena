﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpellMeshPreview : MonoBehaviour
{
    private Mesh mesh;
    private Vector3 origin;

    private readonly float fov = 360;
    private readonly float viewDistance = 40f;

    void Start()
    {
        origin = Vector3.zero;
        mesh = new Mesh();
    }

    public void CreatePreviewMesh(Vector3 origin, float distance)
    {
        Vector3[] vertices = new Vector3[4];
        Vector2[] uv = new Vector2[vertices.Length];
        int[] triangles = new int[6];

        vertices[0] = new Vector3(origin.x - 1, origin.y);
        vertices[1] = new Vector3(origin.x - 1, origin.y + distance);
        vertices[2] = new Vector3(origin.x + 1, origin.y + distance);
        vertices[3] = new Vector3(origin.x + 1, origin.y);

        uv[0] = new Vector2(0, 0);
        uv[1] = new Vector2(0, 1);
        uv[2] = new Vector2(1, 1);
        uv[3] = new Vector2(1, 0);

        triangles[0] = 0;
        triangles[1] = 1;
        triangles[2] = 2;
        triangles[3] = 0;
        triangles[4] = 2;
        triangles[5] = 3;

        mesh.vertices = vertices;
        mesh.uv = uv;
        mesh.triangles = triangles;
        GetComponent<MeshFilter>().mesh = mesh;
    }

    public void DestroyMesh()
    {
        GetComponent<MeshFilter>().mesh = null;
    }
}
