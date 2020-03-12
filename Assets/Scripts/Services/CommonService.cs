public class CommonService {
    
    public static bool ArrayContains<T>(T[] array, T typeToCompare) {
        for(int i = 0; i < array.Length; i++) {
            if(array[i].Equals(typeToCompare)) {
                return true;
            }
        }

        return false;
    }
    
    public static bool ArrayNotContains<T>(T[] array, T typeToCompare) {
        for(int i = 0; i < array.Length; i++) {
            if(array[i].Equals(typeToCompare)) {
                return false;
            }
        }

        return true;
    }
}
