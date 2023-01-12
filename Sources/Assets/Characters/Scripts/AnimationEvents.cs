using UnityEngine;

public class AnimationEvents : MonoBehaviour
{
    CharacterRenderer controller;
    Entity entity;
    private void Awake()
    {
        controller = GetComponentInParent<CharacterRenderer>();
        entity = GetComponentInParent<Entity>();
    }

    public void AnimationEnd()
    {
        controller.AttackEnd();
    }

    public void AnimationStart()
    {
        controller.AttackStart();
    }

    public void DestroyObject()
    {
        entity.Destroy();
    }
}
