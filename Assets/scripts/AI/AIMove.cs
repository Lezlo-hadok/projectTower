
using UnityEngine;
using UnityEngine.AI;


public class AIMove : MonoBehaviour
{
    public WaypointInfo info;
    public NavMeshAgent agent;
    public int currentPoint; 
    

    private void Awake()
    {
        info = GameObject.FindGameObjectWithTag("WaypointContaioner").GetComponent<WaypointInfo>();

        agent = this.GetComponent<NavMeshAgent>();
        currentPoint = 0;
        agent.enabled = true;
        

        
    }
    private void Update()
    {
        agent.SetDestination(info.waypoints[currentPoint].position);
        if (agent.remainingDistance <= 0.1f)
        {
            if (currentPoint < info.waypoints.Length-1)
            {
                currentPoint++;
            }
            

        }
    }
    public void OnDestory() 
    {
        WaveHandler.aliveEnemies.Remove(this.gameObject);
    }
}
