using UnityEditor;

namespace RocinanteGames.Editor
{
    public class ExportPackage{
        [MenuItem ("Export/RocinanteStarterPackage")]
        static void Export()
        {
            AssetDatabase.ExportPackage (AssetDatabase.GetAllAssetPaths(),PlayerSettings.productName + ".unitypackage",ExportPackageOptions.Interactive | ExportPackageOptions.Recurse | ExportPackageOptions.IncludeDependencies | ExportPackageOptions.IncludeLibraryAssets);
        }
    }
}