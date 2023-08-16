using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RopeAttachment : MonoBehaviour
{
    public GameObject playerCharacter;
    public Transform attachmentPoint;
    public LineRenderer ropeRenderer;

    private bool isAttached = false;
    private SpringJoint ropeJoint;
    private float initialMaxDistance = 10;

    // Start is called before the first frame update
    void Start()
    {
        AttachRope();
    }

    // Update is called once per frame
    void Update()
    {
        if (isAttached)
        {
            // Update the rope's LineRenderer to visualize the connection
            ropeRenderer.SetPosition(0, playerCharacter.transform.position);
            ropeRenderer.SetPosition(1, attachmentPoint.position);

            float distanceToAttachment = Vector3.Distance(playerCharacter.transform.position, attachmentPoint.position);
            if (distanceToAttachment > initialMaxDistance)
            {
                Vector3 directionToAttachment = (attachmentPoint.position - playerCharacter.transform.position).normalized;
                Vector3 newPosition = attachmentPoint.position - directionToAttachment * initialMaxDistance;
                playerCharacter.transform.position = newPosition;
            }
        }
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject == playerCharacter && !isAttached)
        {
            AttachRope();
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject == playerCharacter && isAttached)
        {
            DetachRope();
        }
    }

    private void AttachRope()
    {
        isAttached = true;
        ropeRenderer.enabled = true;

        ropeJoint = playerCharacter.AddComponent<SpringJoint>();
        ropeJoint.autoConfigureConnectedAnchor = false;
        ropeJoint.connectedBody = attachmentPoint.GetComponent<Rigidbody>();
        ropeJoint.anchor = Vector3.zero;
        ropeJoint.connectedAnchor = Vector3.zero;
        ropeJoint.spring = 500f;
        ropeJoint.damper = 5f;
        ropeJoint.maxDistance = Vector3.Distance(playerCharacter.transform.position, attachmentPoint.position) * 1.2f;
    }

    private void DetachRope()
    {
        isAttached = false;
        ropeRenderer.enabled = false;
        Destroy(ropeJoint);
    }
}
