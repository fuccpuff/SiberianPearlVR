using UnityEngine;

public interface IInputHandler
{
    bool IsPushing();
    Vector3 GetPushDirection();
}