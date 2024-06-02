using UnityEngine;

public class Unit : MonoBehaviour
{
    public UnitData unitData;
    public GameObject projectilePrefab;
    public Transform projectileSpawnPoint; // Point de lancement des projectiles

    private int currentHealth;
    private bool isStopped = false;
    private float attackTimer = 0f;
    private Transform currentTarget;
    private GameManager gameManager;

    void Start()
    {
        InitializeUnit();
    }

    void Update()
    {
        if (!isStopped)
        {
            float direction = unitData.teamId == 2 ? -1 : 1;
            transform.Translate(Vector2.right * direction * unitData.speed * Time.deltaTime);
        }

        attackTimer -= Time.deltaTime;

        if (currentTarget != null && attackTimer <= 0f)
        {
            float distanceToTarget = Vector2.Distance(transform.position, currentTarget.position);
            if (currentTarget.GetComponent<Base>() != null) // Attaquer la base
            {
                AttackBase(currentTarget.GetComponent<Base>());
                attackTimer = unitData.attackInterval;
            }
            else if (unitData.unitName == "RangedUnit" && distanceToTarget <= unitData.attackRange)
            {
                ShootProjectile();
                attackTimer = unitData.attackInterval;
            }
            else if (unitData.unitName != "RangedUnit" && distanceToTarget <= 1.0f) // Ajuster la portée de mêlée si nécessaire
            {
                MeleeAttack(currentTarget.GetComponent<Unit>());
                attackTimer = unitData.attackInterval;
            }
        }
    }

    private void InitializeUnit()
    {
        currentHealth = unitData.health;
        gameManager = FindObjectOfType<GameManager>(); // Trouver le GameManager

        // Ajout du BoxCollider2D pour la gestion de la file d'attente
        BoxCollider2D queueCollider = gameObject.AddComponent<BoxCollider2D>();
        queueCollider.isTrigger = true;
        queueCollider.size = new Vector2(1.0f, 1.0f); // Ajustez la taille selon vos besoins

        // Ajout du BoxCollider2D pour détecter les collisions pour prendre des dégâts
        BoxCollider2D damageCollider = gameObject.AddComponent<BoxCollider2D>();
        damageCollider.isTrigger = false; // Ce collider ne doit pas être un trigger pour les collisions physiques
        damageCollider.offset = new Vector2(0, -1.0f); // Ajustez la position selon vos besoins

        if (unitData.unitName == "RangedUnit")
        {
            // Ajout du CircleCollider2D pour gérer la portée d'attaque
            GameObject range = new GameObject("AttackRange");
            range.transform.parent = transform;
            range.transform.localPosition = Vector2.zero;
            CircleCollider2D attackRangeCollider = range.AddComponent<CircleCollider2D>();
            attackRangeCollider.isTrigger = true;
            attackRangeCollider.radius = unitData.attackRange;
            AttackRange attackRange = range.AddComponent<AttackRange>();
            attackRange.unit = this;

            // Ajout du GameObject pour le point de lancement des projectiles
            GameObject spawnPoint = new GameObject("ProjectileSpawnPoint");
            spawnPoint.transform.parent = transform;
            spawnPoint.transform.localPosition = new Vector2(1.0f, 0); // Ajustez la position selon vos besoins
            projectileSpawnPoint = spawnPoint.transform;
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        Unit otherUnit = collision.gameObject.GetComponent<Unit>();
        Base otherBase = collision.gameObject.GetComponent<Base>();
        if (otherUnit != null)
        {
            if (otherUnit.unitData.teamId != unitData.teamId)
            {
                HandleEnemyCollision(otherUnit);
            }
            else if (otherUnit.unitData.teamId == unitData.teamId)
            {
                HandleAllyCollision(otherUnit);
            }
        }
        else if (otherBase != null)
        {
            if (otherBase.teamId != unitData.teamId)
            {
                HandleEnemyBaseCollision(otherBase);
            }
        }
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        Unit otherUnit = collision.gameObject.GetComponent<Unit>();
        Base otherBase = collision.gameObject.GetComponent<Base>();
        if (otherUnit != null)
        {
            if (otherUnit.unitData.teamId != unitData.teamId)
            {
                HandleEnemyCollision(otherUnit);
            }
            else if (otherUnit.unitData.teamId == unitData.teamId)
            {
                HandleAllyCollision(otherUnit);
            }
        }
        else if (otherBase != null)
        {
            if (otherBase.teamId != unitData.teamId)
            {
                HandleEnemyBaseCollision(otherBase);
            }
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        Unit otherUnit = collision.gameObject.GetComponent<Unit>();
        Base otherBase = collision.gameObject.GetComponent<Base>();
        if (otherUnit != null)
        {
            if (otherUnit.unitData.teamId != unitData.teamId)
            {
                if (unitData.unitName == "RangedUnit")
                {
                    float distanceToEnemy = Vector2.Distance(transform.position, otherUnit.transform.position);
                    if (distanceToEnemy > unitData.attackRange)
                    {
                        currentTarget = null;
                        isStopped = false;
                    }
                }
                else
                {
                    currentTarget = null;
                    isStopped = false;
                }
            }
            else if (otherUnit.unitData.teamId == unitData.teamId)
            {
                // Gestion de la file d'attente des unités alliées
                isStopped = false;
            }
        }
        else if (otherBase != null)
        {
            if (otherBase.teamId != unitData.teamId)
            {
                currentTarget = null;
                isStopped = false;
            }
        }
    }

    private void HandleEnemyCollision(Unit enemy)
    {
        if (unitData.unitName == "RangedUnit")
        {
            float distanceToEnemy = Vector2.Distance(transform.position, enemy.transform.position);
            if (distanceToEnemy <= unitData.attackRange)
            {
                isStopped = true;
                currentTarget = enemy.transform;
            }
        }
        else
        {
            isStopped = true;
            currentTarget = enemy.transform;
        }
    }

    private void HandleAllyCollision(Unit ally)
    {
        // Gestion de la file d'attente des unités alliées
        if (ally.isStopped) // Arrêtez cette unité si l'unité alliée devant est arrêtée
        {
            isStopped = true;
        }
    }

    private void HandleEnemyBaseCollision(Base enemyBase)
    {
        isStopped = true;
        currentTarget = enemyBase.transform;
    }

    public void EnemyInRange(Unit enemy)
    {
        if (enemy.unitData.teamId != unitData.teamId)
        {
            isStopped = true;
            currentTarget = enemy.transform;
        }
    }

    public void EnemyOutOfRange(Unit enemy)
    {
        if (enemy.unitData.teamId != unitData.teamId)
        {
            isStopped = false;
            currentTarget = null;
        }
    }

    private void Attack(Unit enemy)
    {
        if (unitData.unitName == "RangedUnit")
        {
            ShootProjectile();
        }
        else
        {
            MeleeAttack(enemy);
        }
    }

    private void MeleeAttack(Unit enemy)
    {
        if (enemy != null)
        {
            Debug.Log($"{unitData.unitName} attacks {enemy.unitData.unitName} for {unitData.attackPower} damage.");
            enemy.TakeDamage(unitData.attackPower);
        }
    }

    private void AttackBase(Base enemyBase)
    {
        if (enemyBase != null)
        {
            Debug.Log($"{unitData.unitName} attacks base {enemyBase.teamId} for {unitData.attackPower} damage.");
            enemyBase.TakeDamage(unitData.attackPower);
        }
    }

    private void ShootProjectile()
    {
        if (projectilePrefab != null && currentTarget != null && projectileSpawnPoint != null)
        {
            GameObject projectile = Instantiate(projectilePrefab, projectileSpawnPoint.position, Quaternion.identity);
            Projectile projScript = projectile.GetComponent<Projectile>();
            projScript.damage = unitData.attackPower;
            projScript.speed = 5.0f; // Vitesse du projectile
            projScript.teamId = unitData.teamId;
            projScript.target = currentTarget; // Assigner la cible pour le projectile
            Debug.Log($"{unitData.unitName} shoots a projectile.");
        }
    }

    public void TakeDamage(int damage)
    {
        currentHealth -= damage;
        Debug.Log($"{unitData.unitName} takes {damage} damage. Remaining health: {currentHealth}.");
        if (currentHealth <= 0)
        {
            if (gameManager != null && unitData.teamId != 1) // Vérifiez que l'unité n'appartient pas à l'équipe du joueur (équipe 1)
            {
                gameManager.AddGold(unitData.goldReward); // Ajout de la récompense d'or
                gameManager.AddXP(unitData.xpReward); // Ajout de l'XP
            }
            Debug.Log($"{unitData.unitName} has been destroyed.");
            Destroy(gameObject);
        }
    }
}
