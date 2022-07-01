using System;
using UnityEngine;

namespace HS {
	public abstract class SubUpdate : Mono, IDisposable {
		public GameObject MyGameObject { get; set; } = null;
		Handle handle = null;

		protected void Awake() {
			MyGameObject = gameObject;
			AddOnDestroy += Dispose;
			AddThis();
		}

		public abstract void Sub_Update( float deltaTime );
		public abstract Handle AddThis();

		public void Dispose() {
			handle?.Dispose();
			handle = null;
			enabled = false;
		}
	}
}