using UnityEngine;

namespace GameSystem {
	public class Phase : ScriptableObject {
		public string PhaseName = "";
		public ActionList player = new ActionList();
		public ActionList other_player = new ActionList();
	}
}
