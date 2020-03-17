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

    public static bool IsOutOfBound(int x, int y, int width, int height) {
        return (x < 0 || x > (width)) || (y < 0 || y > (height));
    }

}
