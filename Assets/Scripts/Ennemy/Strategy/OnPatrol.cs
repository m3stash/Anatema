using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OnPatrol : IEnnemyState {
    private readonly EnnemyBrain brain;

    public OnPatrol(EnnemyBrain brain) {
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
        // Vector2 direction = new Vector2(positionToFollow.x, -this.brain.transform.position.x);
        // this.brain.rb.velocity = direction * 0.5f;
        // this.brain.rb.velocity = new Vector2(positionToFollow.x, positionToFollow.y);
    }

    public void Defend() {

    }
}