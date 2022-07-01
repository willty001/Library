/*
 * メッセージの管理クラス.
 */
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace HS {
	public enum MessageType {
		/// <summary> key name:{0} </summary>
		KeyNotExist,
		/// <summary> key name:{0} </summary>
		KeyAlreadyExist,
	}

	[CreateAssetMenu( fileName = "SystemMessage", menuName = "ScriptableObjects/SystemMessage", order = 1)]
	public class SystemMessage : ScriptableObject {
		[Serializable]
		protected class Messsage {
			[SerializeField]
			public MessageType MessageType = (MessageType)0;
			[SerializeField]
			public string Message = null;
		}

		[SerializeField]
		List<Messsage> messsages = new List<Messsage>();

		public static string GetMessage( MessageType type ) {
			switch( type ) {
				case MessageType.KeyNotExist:  return "キー[{0}]は、存在しません。";//"Key[{0}] is not exist.";
				case MessageType.KeyAlreadyExist:  return "キー[{0}]は、既に存在します。";//"Key[{0}] is Already Exist.";
				default: return string.Empty;
			}
		}
	}
}
