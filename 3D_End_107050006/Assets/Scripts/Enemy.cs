using UnityEngine;
using UnityEngine.AI;

public class Enemy : MonoBehaviour
{
    [Header("移動速度"), Range(0, 50)]
    public float speed = 3;

    private Transform player;
    private NavMeshAgent nav;

    private void Awake()
    {
        nav = GetComponent<NavMeshAgent>();

        player = GameObject.Find("艾爾").transform;
    }
}
