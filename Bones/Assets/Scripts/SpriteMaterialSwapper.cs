using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpriteMaterialSwapper : MonoBehaviour
{

    public SpriteRenderer sr;

    [System.Serializable]
    private struct MaterialData {
        public string key;
        public Material material;
    }

    [SerializeField] 
    private MaterialData[] materialData;

    private Dictionary<string, Material> materials = new Dictionary<string, Material>();
    void Awake()
    {
        foreach (MaterialData i in materialData)
            materials.Add(i.key, i.material);
    }

    public void Swap(string key) {
        if (key.Length == 0)
            Swap("Default");

        if (!materials.ContainsKey(key))
            return;

        sr.material = materials[key];
    }

    public void Swap() {
        Swap("");
    }
}
