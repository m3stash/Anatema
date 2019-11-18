using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

[CreateAssetMenu(fileName = "TileBase configuration", menuName = "Tilebase/config")]
public class TileBase_cfg : ScriptableObject {
    [System.Serializable]
    public class config {
        public int id;
        public TileBase tilebase;
    }
    public config[] tileBase_cfgs;

    public Dictionary<int, TileBase> GetDico() {
        var dico = new Dictionary<int, TileBase>();
        for(var i = 0; i < tileBase_cfgs.Length; i++) {
            dico.Add(tileBase_cfgs[i].id, tileBase_cfgs[i].tilebase);
        }
        return dico;
    }
}



