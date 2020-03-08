using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CommonService {
    
    public static bool ArrayContains<T>(T[] unallowedBlockTypes, T typeToCompare) {
        for(int i = 0; i < unallowedBlockTypes.Length; i++) {
            if(unallowedBlockTypes[i].Equals(typeToCompare)) {
                return false;
            }
        }

        return true;
    }
}
