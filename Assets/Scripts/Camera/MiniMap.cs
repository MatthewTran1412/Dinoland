using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MiniMap : MonoBehaviour
{
    public Transform player;
    private void Start() => player=PlayerControl.Instance.m_Player?PlayerControl.Instance.m_Player.transform:null;
    private void LateUpdate()
    {
        if(!player)
            return;
        Vector3 newPosition = player.position;
        newPosition.y = transform.position.y;
        transform.position = newPosition;

        transform.rotation= Quaternion.Euler(90f,0,0f);
    }
}
