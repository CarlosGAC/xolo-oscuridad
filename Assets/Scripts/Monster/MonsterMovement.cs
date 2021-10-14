using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class MonsterMovement : MonoBehaviour
{
    public Stats target;
    public float speed;

    public float aggroDistance;
    public bool aggro;

    private bool targetInsideAggroRange;

    private float timeSinceTargetEscaped;
    public float timeToResetAggro;

    public float defaultSpeed;

    public UnityEvent OnMonsterAttack;

    public ObjectSpawner spawner;

    private void Start()
    {
        OnMonsterAttack.AddListener(target.OnMonsterAttackHandler);
    }

    public void SetMonsterToDefault()
    {
        aggro = false;
    }

    private void Update()
    {
        if(Vector3.Distance(target.transform.position, transform.position) < aggroDistance)
        {
            aggro = true;
            targetInsideAggroRange = true;
            if(aggro)
            {
                timeSinceTargetEscaped = 0;
            }
        } else
        {
            if(aggro)
            {
                targetInsideAggroRange = false;
                timeSinceTargetEscaped += Time.deltaTime;
            }
        }

        if(timeSinceTargetEscaped >= timeToResetAggro)
        {
            aggro = false;
            spawner.ChangeObjectPositionToNewSpawnPoint(gameObject);
            timeSinceTargetEscaped = 0;
        }

        if (aggro && Vector3.Distance(target.transform.position, transform.position) > 0.2f)
        {
            Vector3 newPos = Vector3.MoveTowards(transform.position, target.transform.position, speed * Time.deltaTime);
            newPos = new Vector3(newPos.x, newPos.y, -0.5f);
            transform.position = newPos;
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        Debug.Log(other.gameObject.name);
        if(other.gameObject.name == target.gameObject.name)
        {
            Debug.Log("I'm inside the target's collider");
            OnMonsterAttack.Invoke();
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if(other.gameObject.name == target.name)
        {
            Debug.Log("I'm leaving the target's collider");
            
        }   
    }
}
