/*
 * 数式を使ったMeshの生成クラス.
 */
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace HS {
	public class Surface : Mono {
		[SerializeField]
		Formula formula = new Formula();
	}
}
