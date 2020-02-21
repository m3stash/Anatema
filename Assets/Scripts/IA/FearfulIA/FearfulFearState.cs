using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FearfulFearState : State {
    public State ear() {
        return this;
    }

    public State view(ViewEvent ev) {
       if(ev.ennemyInView == false) {
            return new FearfulCalmState();
        }
        return this;
    }
}
