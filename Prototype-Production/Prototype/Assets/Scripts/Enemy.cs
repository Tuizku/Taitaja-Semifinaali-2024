using UnityEngine;

public class Enemy : Entity
{
    private new void OnEnable()
    {
        GameManager.onTurn += Run; // Act on turn
        base.OnEnable();
    }
    private new void OnDisable()
    {
        GameManager.onTurn -= Run;
        base.OnDisable();
    }
    public virtual void Run()
    {
        Debug.Log("Enemy run!");
    }
}
