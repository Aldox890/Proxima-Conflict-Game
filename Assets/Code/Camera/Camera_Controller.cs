using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Camera_Controller : MonoBehaviour
{
    public GameObject target;
    public float followSpeed = 10f;
    private Vector3 position;

    public float offsetStarth = 1f;
    public float offseth = 9f;
    public float offsetb = -3f;

    // Start is called before the first frame update
    void Start()
    {
        position = transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        handleRaycast();
        handleCamera();
    }

    private void handleRaycast()
    {

        Vector3 endPos = target.transform.position + (Vector3.up * offseth) + (Vector3.forward * offsetb);
        
        RaycastHit hit;
        Vector3 start = target.transform.position + target.transform.up * offsetStarth;
        if (Physics.Raycast(start, endPos - start, out hit, 20f, LayerMask.GetMask("Default")))
        {
            position = hit.point;
        }
        else
        {
            position = endPos;
        }
    }

    private void handleCamera()
    {
        if (target)
        {
            //transform.LookAt(target.transform);
            transform.position = Vector3.Slerp(transform.position, position, followSpeed * Time.deltaTime);
            Quaternion lookOnLook = Quaternion.LookRotation((target.transform.position + target.transform.up * 1f) - transform.position);
            transform.rotation = Quaternion.Slerp(transform.rotation, lookOnLook, Time.deltaTime);
        }
    }
}
