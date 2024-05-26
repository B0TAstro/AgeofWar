using UnityEngine;

public class Projectile : MonoBehaviour
{
    public int damage;
    public float speed;
    public int teamId;
    public Transform target; // Cible du projectile

    private void Update()
    {
        if (target != null)
        {
            Vector2 direction = (target.position - transform.position).normalized;
            transform.Translate(direction * speed * Time.deltaTime);

            float distanceToTarget = Vector2.Distance(transform.position, target.position);
            if (distanceToTarget < 0.1f) // Ajustez cette valeur selon vos besoins
            {
                HitTarget();
            }
        }
        else
        {
            Destroy(gameObject); // Détruire le projectile si la cible n'existe plus
        }
    }

    private void HitTarget()
    {
        Unit enemy = target.GetComponent<Unit>();
        if (enemy != null && enemy.unitData.teamId != teamId)
        {
            enemy.TakeDamage(damage);
        }
        Destroy(gameObject); // Détruire le projectile après avoir touché
    }
}
