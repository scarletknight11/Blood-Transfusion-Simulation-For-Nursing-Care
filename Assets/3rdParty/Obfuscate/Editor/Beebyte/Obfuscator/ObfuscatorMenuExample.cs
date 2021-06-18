/**/
using UnityEngine;
using UnityEditor;

//Options are read in the same way as normal Obfuscation, i.e. from the ObfuscatorOptions.asset
namespace Beebyte.Obfuscator
{
	public class ObfuscatorMenuExample 
	{
		private static Options _options = null;

		[MenuItem("Tools/Obfuscate eglib.dll")]
		private static void ObfuscateEaglibDll()
		{
			//const string dllPath1 = @"C:\Users\Drexel\Desktop\Projects\Git\egdl\Assets\eag\enablegames\Plugins\Release\eaglib.dll";
			string dllPath = Application.dataPath + @"/eag/enablegames/Plugins/Release/eaglib.dll";
			//Debug.Log("Obfuscating:"+ dllPath1);
			Debug.Log("Obfuscating:"+ dllPath);
			//return;

			if (System.IO.File.Exists(dllPath))
			{
				if (_options == null) _options = OptionsManager.LoadOptions();

				bool oldSkipRenameOfAllPublicMonobehaviourFields = _options.skipRenameOfAllPublicMonobehaviourFields;
				try
				{
					 //Preserving monobehaviour public field names is an common step for obfuscating external DLLs that
					 //allow MonoBehaviours to be dragged into the scene's hierarchy.
					_options.skipRenameOfAllPublicMonobehaviourFields = true;

					//Consider setting this hidden value to false to allow classes like EditorWindow to be obfuscated.
					//ScriptableObjects would normally be treated as Serializable to avoid breaking loading/saving,
					//but for Editor windows this might not be necessary.
					//options.treatScriptableObjectsAsSerializable = false;

					Obfuscator.SetExtraAssemblyDirectories(_options.extraAssemblyDirectories);
					Obfuscator.Obfuscate(dllPath, _options, EditorUserBuildSettings.activeBuildTarget);
				}
				finally
				{
					_options.skipRenameOfAllPublicMonobehaviourFields = oldSkipRenameOfAllPublicMonobehaviourFields;
					EditorUtility.ClearProgressBar();
				}
			}
			else Debug.Log("Obfuscating could not find file at " + dllPath);
		}
	}
}
/**/
