﻿using UnityEngine;


public class ModuleConnector : MonoBehaviour {

    public string[] Tags;
    public bool IsDefault;

    private void Awake()
    {

        // transform.parent is the Module, this ModuleConnector is connected to
        GameObject mGO = transform.parent.gameObject;
        Module m = mGO.GetComponent<Module>();
        if (m != null)
            m.AddConnector(this);
    }


    public bool IsConnected { get; set; }

    private void OnDrawGizmos()
    {
        const float scale = 1.0f;

        Gizmos.color = Color.blue;
        Gizmos.DrawLine(transform.position, transform.position + transform.forward * scale);

        Gizmos.color = Color.red;
        Gizmos.DrawLine(transform.position, transform.position - transform.right * scale);
        Gizmos.DrawLine(transform.position, transform.position + transform.right * scale);

        Gizmos.color = Color.green;
        Gizmos.DrawLine(transform.position, transform.position + Vector3.up * scale);

        Gizmos.color = Color.yellow;
        Gizmos.DrawSphere(transform.position, 0.125f);
    }
}