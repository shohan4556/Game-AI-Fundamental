using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public struct Link
{
	public enum direction {UNI, BI}; // uni or bi directional 
	public GameObject node1;
	public GameObject node2;
	public direction dir;
}

public class WaypointManager : MonoBehaviour {

	public GameObject[] Waypoints;
	public Link[] Links;
	public Graph graph = new Graph();
	// Use this for initialization
	void Start () {
		if(Waypoints.Length > 0){

			foreach(GameObject _node in Waypoints){
				graph.AddNode(_node);
			}

			foreach(Link _link in Links){
				graph.AddEdge(_link.node1, _link.node2);
				if(_link.dir == Link.direction.BI)
					graph.AddEdge(_link.node2, _link.node1);
			}
		}
	} // end 
	
	// Update is called once per frame
	void Update () {
		graph.debugDraw();
	}
}
