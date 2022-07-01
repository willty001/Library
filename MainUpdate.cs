using System;
using System.Collections.Generic;
using UnityEngine;

namespace HS {
	public class MainUpdate<T> : MonoSingleton<T> where T : MainUpdate<T> {
		Func<SubUpdate,bool> condition = null;
		bool isCheckActive = true;
		bool isCheckEnable = true;
		int frequency = 0;

		public float TimeScale { get; set; } = 1;
		public int UpdateFrequency { get; set; } = 0;
		public bool IsCheckActive { 
			get { return isCheckActive; }
			set {
				if( isCheckActive != value ) {
					isCheckActive = value;
					SetCondition();
				}
			}
		}
		public bool IsCheckEnable { 
			get { return isCheckEnable; }
			set {
				if( isCheckEnable != value ) {
					isCheckEnable = value;
					SetCondition();
				}
			}
		}

		List<SubUpdate> subUpdates = new List<SubUpdate>();

		class UnregisterHandle : Handle {
			SubUpdate SubUpdate { get; set; } = null;

			public UnregisterHandle( SubUpdate subUpdate ) : base() {
				SubUpdate = subUpdate;
				Action = Unregister;
			}

			void Unregister() {
				Delete( SubUpdate );
			}
		}
		protected void Awake() {
			SetCondition();
		}

		// Update is called once per frame
		protected void Update() {
			if( UpdateFrequency != 0 && frequency < UpdateFrequency ) {
				++frequency;
				return;
			}

			float elapsedTime = Time.deltaTime * TimeScale;
			foreach( var subUpdate in subUpdates ) {
				subUpdate.Sub_Update( elapsedTime );
			}

			frequency = 0;
		}
		protected void SetCondition() {
			if( IsCheckActive && isCheckEnable ) {
				condition = ConditionActiveAndEnable;
			} else if( IsCheckActive ) {
				condition = ConditionActive;
			} else if( IsCheckEnable ) {
				condition = ConditionEnable;
			} else {
				condition = ConditionNon;
			}
		}

		bool ConditionActiveAndEnable( SubUpdate subUpdate ) {
			return ConditionActive( subUpdate ) && ConditionEnable( subUpdate );
		}
		bool ConditionActive( SubUpdate subUpdate ) {
			return subUpdate.MyGameObject.activeSelf;
		}
		bool ConditionEnable( SubUpdate subUpdate ) {
			return subUpdate.enabled;
		}
		bool ConditionNon( SubUpdate subUpdate ) {
			return true;
		}

		public static Handle Add( SubUpdate subUpdate ) {
			return Instance.AddInternal( subUpdate );
		}
		public static void Delete( SubUpdate subUpdate ) {
			if( !Instanted ) return;
			Instance.DeleteInternal( subUpdate );
		}

		Handle AddInternal( SubUpdate subUpdate ) {
			if( !subUpdates.Contains( subUpdate ) ) subUpdates.Add( subUpdate );
			return new UnregisterHandle( subUpdate );
		} 
		bool DeleteInternal( SubUpdate subUpdate ) {
			return subUpdates.Remove( subUpdate );
		} 
	}
}