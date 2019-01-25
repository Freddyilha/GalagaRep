using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AlienFormationBehaviour : MonoBehaviour {

    public GameManangerBehaviour.AlienType alienType;
    public Texture textureGuizmo;

    private void OnDrawGizmos()
    {
        Gizmos.DrawWireSphere(transform.position, 0.3f);
    }
}
