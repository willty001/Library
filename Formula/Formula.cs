using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace HS {
	[Serializable]
	public class Formula {
		[SerializeField]
		List<FormulaPart> parts = new List<FormulaPart>();
	}
}