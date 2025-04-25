using System.Collections.Generic;
using UnityEngine;

namespace ScriptableObjects {
	[CreateAssetMenu(menuName = "ScriptableObjects/PipeListSO", fileName = "PipeListSO")]
	public class PipeListSO : ScriptableObject {
		public List<GameObject> pipeObstacleList = new List<GameObject>();
	} 
}
