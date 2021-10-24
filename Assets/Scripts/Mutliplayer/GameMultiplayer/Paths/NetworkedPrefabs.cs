using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class NetworkedPrefabs
{
    public GameObject Prefab;
    public string Path;

    public NetworkedPrefabs(GameObject obj, string path)
    {
        Prefab = obj;
        Path = PrefabPath(path);
    }

    private string PrefabPath(string path)
    {
        int extLength = System.IO.Path.GetExtension(path).Length;
        int addLength = 10;
        int index = path.ToLower().IndexOf("resources");

        if (index == -1)
            return string.Empty;
        else
            return path.Substring(index + addLength, path.Length - (addLength + index + extLength));
    }
}
