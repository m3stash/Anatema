using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface State {     // Start is called before the first frame update

    public State view(ViewEvent ev);

    public State ear();

}