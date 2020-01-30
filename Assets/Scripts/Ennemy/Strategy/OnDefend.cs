using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OnDefend : IEnnemyState {

    private readonly EnnemyBrain brain;

    public OnDefend(EnnemyBrain brain) {
        this.brain = brain;
    }

    public void Ground() {

    }

    public void Idle() {

    }

    public void Attack() {
        // brain.currentState = new Ennemy_OnAttack(brain);
    }

    public void Patrol() {

    }

    public void Move(Vector2 positionToFollow) {

    }

    public void Defend() {

    }
}
