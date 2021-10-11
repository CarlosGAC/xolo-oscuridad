using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MonsterMovement : MonoBehaviour
{
    public GameObject target;
    public float speed;

    public float aggroDistance;
    public bool aggro;
    private void Update()
    {
        if(Vector3.Distance(target.transform.position, transform.position) < aggroDistance)
        {
            aggro = true;
        }

        if (aggro && Vector3.Distance(target.transform.position, transform.position) > 0.5f)
        {
            Vector3 newPos = Vector3.MoveTowards(transform.position, target.transform.position, speed * Time.deltaTime);
            newPos = new Vector3(newPos.x, newPos.y, -0.5f);
            transform.position = newPos;
        }
    }
}
