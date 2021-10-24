using System.Collections.Generic;
using Photon.Pun;
#if UNITY_EDITOR
using UnityEditor;
#endif
using UnityEngine;

[CreateAssetMenu(menuName = "Singleton/MasterManager")]
public class MasterManager : SingletonScriptableObject<MasterManager>
{
    [SerializeField]
    private GameSettings _gameSettings;
    public static GameSettings GameSettings { get { return Instance._gameSettings; } }

    public List<NetworkedPrefabs> _networkedPrefabs = new List<NetworkedPrefabs>();

    public static GameObject NetworkInstantiate(GameObject obj, Vector3 position, Quaternion rotation)
    {
        Debug.Log("instantiate obj " + obj.name);
        if (Instance == null)
            return null;
        foreach(NetworkedPrefabs networkedPrefab in Instance._networkedPrefabs)
        {
            if (networkedPrefab.Prefab == obj)
            {
                Debug.Log("Found");
                if (networkedPrefab.Path != string.Empty)
                {
                    Debug.Log("Path not empty " + networkedPrefab.Path + "pos : " + position);
                    GameObject res = PhotonNetwork.Instantiate(networkedPrefab.Path, position, rotation);
                    return res;
                }
                else
                {
                    Debug.LogError("Path is empty for the gameObject name : " + networkedPrefab.Prefab);
                }
            }
        }
        return null;
    }

    public static GameObject GetPrefab(string name)
    {
        foreach(NetworkedPrefabs elem in Instance._networkedPrefabs)
        {
            if (elem.Prefab.name == name)
            {
                Debug.Log("Found prefab at path : " + elem.Path);
                return elem.Prefab;
            }
        }
        return null;
    }

    [RuntimeInitializeOnLoadMethod(RuntimeInitializeLoadType.BeforeSceneLoad)]
    public static void PopulateNetworkedPrefabs()
    {
#if UNITY_EDITOR
        if (!Application.isEditor)
            return;
        if (Instance != null && Instance._networkedPrefabs != null)
            Instance._networkedPrefabs.Clear();
        GameObject[] res = Resources.LoadAll<GameObject>("");
        Debug.Log("on load les paths");
        for (int i = 0; i < res.Length; i += 1)
        {
            if (res[i].GetComponent<PhotonView>() != null)
            {
                string path = AssetDatabase.GetAssetPath(res[i]);
                Instance._networkedPrefabs.Add(new NetworkedPrefabs(res[i], path));
            }
        }
#endif
    }

}
