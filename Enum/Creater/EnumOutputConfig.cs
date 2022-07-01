using System.Collections.Generic;
using UnityEngine;

namespace HS {
	[CreateAssetMenu( fileName = nameof( EnumDefine ), menuName = Define.MENU + "/" + nameof( EnumOutputConfig ) )]
	public class EnumOutputConfig : ScriptableObject {
		[SerializeField]
		public OutputSourceCode output = new OutputSourceCode();

	}
}