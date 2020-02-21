using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FearfulCalmState : State {
    public State ear() {
        return new FearfulFearState();
    }

    public State view(ViewEvent ev) {
        return new FearfulFearState();
    }
}
   
