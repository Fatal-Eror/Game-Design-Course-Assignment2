using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Catapult : MonoBehaviour
{
    public float forceOnY = 30;
    public float forceOnZ = 30;
    private void OnCollisionEnter(Collision collision)
    {
        collision.rigidbody.AddForce(new Vector3(0,forceOnY,forceOnZ),ForceMode.Impulse);
    }
}
