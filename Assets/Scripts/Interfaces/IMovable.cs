using UnityEngine;

public interface IMovable {
    float Speed { get; }
    void Move(Vector2 direction);
}
