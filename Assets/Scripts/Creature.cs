using UnityEngine;

public interface Creature
{
    public void Hit(float damage, Vector2 knockback, float backTime);
    public void Die();
}