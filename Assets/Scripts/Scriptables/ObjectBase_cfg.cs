using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

[CreateAssetMenu(fileName = "ObjectBase configuration", menuName = "ObjectBase/config")]
public class ObjectBase_cfg : ScriptableObject {
    [System.Serializable]
    public class config {
        public int id;
        public Item_cfg objectBase;
    }
    public config[] objectBase_cfg;

    public Dictionary<int, Item_cfg> GetDico() {
        var dico = new Dictionary<int, Item_cfg>();
        for (var i = 0; i < objectBase_cfg.Length; i++) {
            dico.Add(objectBase_cfg[i].id, objectBase_cfg[i].objectBase);
        }
        return dico;
    }
}


