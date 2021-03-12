using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Mirror;

public class Mine_AI : NetworkBehaviour
{
    public GameObject radar;
    public float explodeDistance = 4f;
    public float explosionRadius = 6f;
    public float damage = 500f;
    public float speed = 4f;
    public float force = 5f;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    RaycastHit hit;
    GameObject target;

    void Update()
    {
        if (target = radar.GetComponent<Radar>().target)
        {
            Vector3 dir = target.transform.position - transform.position;
            dir = Vector3.Normalize(dir);
            //gameObject.GetComponent<Rigidbody>().AddForce(dir * Time.deltaTime * speed, ForceMode.Impulse);
            gameObject.GetComponent<Rigidbody>().velocity = dir * speed;

            if (Vector3.Distance(transform.position, target.transform.position) < explodeDistance)
            {
                Collider[] hitColliders = Physics.OverlapSphere(transform.position, explosionRadius);
                foreach (Collider c in hitColliders)
                {
                    GameObject parent;
                    if (c.isTrigger || (c.transform.parent && c.transform.parent.parent && c.transform.parent.parent.parent))
                    {

                        parent = c.transform.parent.parent.parent.gameObject;
                        parent.GetComponent<Rigidbody>().AddForce(dir * force, ForceMode.Impulse);

                        if (parent.GetComponent<Entity>())
                        {
                            parent.GetComponent<Entity>().takeDamage(damage);
                        }
                    }
                }
                gameObject.GetComponent<Entity>().takeDamage(50000f);
            }
        }
    }

    [Server]
    void onCollisionEnter(Collider col)
    {
        Collider[] hitColliders = Physics.OverlapSphere(transform.position, explosionRadius);
        foreach (Collider c in hitColliders)
        {
            GameObject parent;
            if (c.isTrigger || (c.transform.parent && c.transform.parent.parent && c.transform.parent.parent.parent))
            {

                parent = c.transform.parent.parent.parent.gameObject;

                if (parent.GetComponent<Entity>())
                {
                    parent.GetComponent<Entity>().takeDamage(damage);
                }
            }
        }
        gameObject.GetComponent<Entity>().takeDamage(50000f);
    }
}
