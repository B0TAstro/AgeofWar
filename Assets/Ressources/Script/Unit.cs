using UnityEngine;

public class Unit : MonoBehaviour
{
    public UnitData unitData;
    public GameObject projectilePrefab;
    public Transform projectileSpawnPoint;

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
            if (currentTarget.GetComponent<Base>() != null)
            {
                AttackBase(currentTarget.GetComponent<Base>());
                attackTimer = unitData.attackInterval;
            }
            else if (unitData.unitName == "RangedUnit" && distanceToTarget <= unitData.attackRange)
            {
                ShootProjectile();
                attackTimer = unitData.attackInterval;
            }
            else if (unitData.unitName != "RangedUnit" && distanceToTarget <= 1.0f)
            {
                MeleeAttack(currentTarget.GetComponent<Unit>());
                attackTimer = unitData.attackInterval;
            }
        }
    }

    private void InitializeUnit()
    {
        currentHealth = unitData.health;
        gameManager = FindObjectOfType<GameManager>();

        BoxCollider2D queueCollider = gameObject.AddComponent<BoxCollider2D>();
        queueCollider.isTrigger = true;
        queueCollider.size = new Vector2(1.0f, 1.0f);

        BoxCollider2D damageCollider = gameObject.AddComponent<BoxCollider2D>();
        damageCollider.isTrigger = false;
        damageCollider.offset = new Vector2(0, -1.0f);

        if (unitData.unitName == "RangedUnit")
        {
            GameObject range = new GameObject("AttackRange");
            range.transform.parent = transform;
            range.transform.localPosition = Vector2.zero;
            CircleCollider2D attackRangeCollider = range.AddComponent<CircleCollider2D>();
            attackRangeCollider.isTrigger = true;
            attackRangeCollider.radius = unitData.attackRange;
            AttackRange attackRange = range.AddComponent<AttackRange>();
            attackRange.unit = this;

            GameObject spawnPoint = new GameObject("ProjectileSpawnPoint");
            spawnPoint.transform.parent = transform;
            spawnPoint.transform.localPosition = new Vector2(1.0f, 0);
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
        if (ally.isStopped)
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

            if (enemy.GetHealth() <= 0)
            {
                gameManager.AddGold(unitData.goldReward);
                gameManager.AddXP(unitData.xpReward);
                Debug.Log($"{unitData.unitName} killed {enemy.unitData.unitName} and gained {unitData.goldReward} gold and {unitData.xpReward} XP.");
            }
        }
    }

    private void ShootProjectile()
    {
        if (projectilePrefab != null && projectileSpawnPoint != null)
        {
            GameObject projectileObject = Instantiate(projectilePrefab, projectileSpawnPoint.position, Quaternion.identity);
            Projectile projectile = projectileObject.GetComponent<Projectile>();
            if (projectile != null)
            {
                projectile.damage = unitData.attackPower;
                projectile.speed = 10f;
                projectile.teamId = unitData.teamId;
                projectile.target = currentTarget;
            }
        }
    }

    private void AttackBase(Base enemyBase)
    {
        Debug.Log($"{unitData.unitName} attacks {enemyBase.name} for {unitData.attackPower} damage.");
        enemyBase.TakeDamage(unitData.attackPower);
    }

    public void TakeDamage(int damage)
    {
        currentHealth -= damage;
        Debug.Log($"{unitData.unitName} takes {damage} damage. Remaining health: {currentHealth}.");
        if (currentHealth <= 0)
        {
            Debug.Log($"{unitData.unitName} has been destroyed.");
            Destroy(gameObject);
        }
    }

    public int GetHealth()
    {
        return currentHealth;
    }
}