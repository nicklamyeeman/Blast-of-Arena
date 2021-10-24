using UnityEngine;

public static class Transforms
{
    public static void DestroyChildren(this Transform t, bool destroyImmediatly = false)
    {
        foreach(Transform child in t)
        {
            if (destroyImmediatly)
                Object.DestroyImmediate(child.gameObject);
            else
                Object.Destroy(child.gameObject);
        }
    }
}