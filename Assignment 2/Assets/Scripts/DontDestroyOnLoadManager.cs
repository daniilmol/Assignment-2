using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class DontDestroyOnLoadManager
{
    static List<GameObject> _ddolObjects = new List<GameObject>();

    public static void DontDestroyOnLoad(this GameObject go) {
       UnityEngine.Object.DontDestroyOnLoad(go);
       _ddolObjects.Add(go);
    }

    public static void DestroyAll() {
        foreach(var go in _ddolObjects)
            if(go != null)
                UnityEngine.Object.Destroy(go);

        _ddolObjects.Clear();
    }

    public static void DisableAll() {
        foreach(var go in _ddolObjects)
            if (go != null)
                go.SetActive(false);
    }

    public static void EnableAll() {
        foreach(var go in _ddolObjects)
            if (go != null)
                go.SetActive(true);
    }

    public static bool CheckDontDestoryExist() {
        if (_ddolObjects.Count == 0)
            return false;
        return true;
    }
}
