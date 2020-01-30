using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OnMove : IEnnemyState {

    private readonly EnnemyBrain brain;

    public OnMove(EnnemyBrain brain) {
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
        // this.brain.rb.velocity = new Vector2(positionToFollow.x, positionToFollow.y);
    }

    public void Defend() {

    }
}
