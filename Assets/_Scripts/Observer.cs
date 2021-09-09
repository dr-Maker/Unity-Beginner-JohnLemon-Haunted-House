using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(CapsuleCollider))]
public class Observer : MonoBehaviour
{
    public Transform player;

    private bool isPlayerInRange;

    public GameEnding gameEnding;

    private void OnTriggerEnter(Collider other)
    {
        if (other.transform == player)
        {
            isPlayerInRange = true;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.transform == player)
        {
            isPlayerInRange = false;
        }
    }

    private void Update()
    {
        if (isPlayerInRange)
        {
            Vector3 direction = player.position - this.transform.position + Vector3.up;
            Ray ray = new Ray(transform.position, direction);

            Debug.DrawRay(transform.position, direction, Color.magenta, Time.deltaTime, true);

            RaycastHit raycasthit;
            if (Physics.Raycast(ray, out raycasthit))
            {
                if (raycasthit.collider.transform == player)
                {
                    gameEnding.CatchPlayer();
                }
            }
        }
    }

    //private void OnDrawGizmos()
    //{
    //    Gizmos.color = Color.red;
    //    Gizmos.DrawSphere(this.transform.position, 1f);

    //    Gizmos.color = Color.yellow;
    //    Gizmos.DrawLine(transform.position, player.position);
    //}

}
