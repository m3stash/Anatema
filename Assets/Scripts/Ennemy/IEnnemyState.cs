using UnityEngine;

public interface IEnnemyState {

    //actions
    void Ground();
    void Idle();
    void Attack();
    void Patrol();
    void Move(Vector2 positionToFollow);
    void Defend();
}
