﻿using UnityEditor;
using System.IO;

namespace Fisiomed.Bundles
{
    public class BundleBuilder : Editor
	{
		[MenuItem("Assets/Build Asset Bundles for Android")]
		static void BuildAllAssetBundleAndroid()
		{
			string path = EditorUtility.OpenFolderPanel("Please, choose a saving directory...", "", "");
			try
			{
				BuildPipeline.BuildAssetBundles(path + "/AndroidPack", BuildAssetBundleOptions.None, BuildTarget.Android);
			}
			catch
			{
				CreateDirectory(path + "/AndroidPack");
				BuildPipeline.BuildAssetBundles(path + "/AndroidPack", BuildAssetBundleOptions.None, BuildTarget.Android);
			}
		}

		[MenuItem("Assets/Build Asset Bundles for iOS")]
		static void BuildAllAssetBundleiOS()
		{
			string path = EditorUtility.OpenFolderPanel("Please, choose a saving directory...", "", "");
			try
			{
				BuildPipeline.BuildAssetBundles(path + "/iOSPack", BuildAssetBundleOptions.None, BuildTarget.iOS);
			}
			catch
			{
				CreateDirectory(path + "/iOSPack");
				BuildPipeline.BuildAssetBundles(path + "/iOSPack", BuildAssetBundleOptions.None, BuildTarget.iOS);
			}
		}

		[MenuItem("Assets/Build Asset Bundles for Android and iOS")]
		static void BuildAllAssetBundleAndroidAndIos()
		{
			BuildAllAssetBundleAndroid();
			BuildAllAssetBundleiOS();
		}

		static void CreateDirectory(string path)
		{
			var folder = Directory.CreateDirectory(path);
		}
	}
}