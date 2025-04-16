using UnityEngine;
using UnityEngine.AI;

public class Unit : MonoBehaviour
{
    private NavMeshAgent agent;

    private void Awake()
    {
        agent = GetComponent<NavMeshAgent>();
    }

    public void MoveTo(Vector3 destination)
    {
        agent.SetDestination(destination);
    }

    [SerializeField] private GameObject SelectionCircle;

    public void Select()
    {
        SelectionCircle.SetActive(true);
    }

    private void Start()
    {
        Deselect();
    }

    public void Deselect()
    {
        SelectionCircle.SetActive(false);
    }

    // Attack

    public float attackRange = 10f;
    public float attackCooldown = 1.0f;
    public int minDamage = 5;
    public int maxDamage = 15;
    public float hitChance = 0.7f;

    public float lastAttackTime;

    private void Update()
    {
        AutoAttack();
    }

    void AutoAttack()
    {
        //Check Enemy in Range
        Collider[] hits = Physics.OverlapSphere(transform.position, attackRange, LayerMask.GetMask("Enemy"));

        foreach (var hit in hits)
        {
            if (Time.time - lastAttackTime >= attackCooldown)
            {
                lastAttackTime = Time.time;

                //RNG
                if (Random.value <= hitChance)
                {
                    DummyEnemy dummy = hit.GetComponent<DummyEnemy>();
                    if (dummy != null)
                    {
                        int damage = Random.Range(minDamage, maxDamage + 1);
                        dummy.TakeDamage(damage);
                        Debug.Log($"{gameObject.name} hit {dummy.name} for {damage} damage!");
                    }
                }
                else
                {
                    Debug.Log($"{gameObject.name} missed the shot!");
                }

                break;
            }
        }
    }

}
