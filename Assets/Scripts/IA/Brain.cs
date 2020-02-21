using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Brain : MonoBehaviour
{

    private State state;

    public Brain(State initialState) {
        this.state = initialState;
    }

    public void receiveViewEvent(ViewEvent ev) {
        state = state.view(ev);
    }

    public void receiveEarEvent() {
        state = state.ear();
    }




    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
