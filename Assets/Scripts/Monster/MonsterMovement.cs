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

    private float timeSinceTargetEscaped;
    public float timeToResetAggro;

    public float defaultSpeed;

    public UnityEvent OnMonsterAttack;

    public ObjectSpawner spawner;


    public int amountOfEscapes;

    private void Start()
    {
        OnMonsterAttack.AddListener(target.OnMonsterAttackHandler);
        amountOfEscapes = 0;
    }

    public void SetMonsterToDefault()
    {
        aggro = false;
    }

    private void Update()
    {
        if(target.transform.position.x > transform.position.x)
        {
            transform.localScale = new Vector3(-2, 2, 1);
        } else
        {
            transform.localScale = new Vector3(2, 2, 1);
        }
        if(Vector3.Distance(target.transform.position, transform.position) < aggroDistance)
        {
            aggro = true;
            if(aggro)
            {
                timeSinceTargetEscaped = 0;
            }
        } else
        {
            if(aggro)
            {
                timeSinceTargetEscaped += Time.deltaTime;
            }
        }

        if(timeSinceTargetEscaped >= timeToResetAggro)
        {
            aggro = false;
            spawner.ChangeObjectPositionToNewSpawnPoint(gameObject);
            timeSinceTargetEscaped = 0;
            amountOfEscapes += 1;
            speed = speed + (amountOfEscapes * 0.1f);
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
        if(other.gameObject.name == target.gameObject.name)
        {
            OnMonsterAttack.Invoke();
            aggro = false;
            timeSinceTargetEscaped = 0;
            spawner.ChangeObjectPositionToNewSpawnPoint(gameObject);
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if(other.gameObject.name == target.name)
        {
            
        }   
    }
}
