# Pulse Unity Asset

This repository is a collection of assets designed to integrate the [Pulse Physiology Engine](https://pulse.kitware.com) within [Unity3D](https://unity3d.com/).

## Platforms Supported
- Unity minimum version 2019.4.X LTS
- Windows 10 x64
- Windows ARM64
- ManyLinux 2010 x64 
- macOS 10.11+
- Lumin OS
- Android ARMv7a
- Android ARMv8a

## Development

**Any contributions to this repository need to be done using Unity 2019.4.X LTS**

This unity version was chosen as it is Long Term Supported and provides compatibility with all targeted platfroms.

There is no reason why this asset could not be used by earlier versions of Unity, but those don't easily support all of our targeted platforms.

## Installation

### From the Asset Store

The Pulse Unity Asset is available for download on the [Unity Asset Store](http://u3d.as/1sp1).

### From a packaged release

Each release is associated with a [Unity Asset Package](https://docs.unity3d.com/Manual/AssetPackages.html) which can be downloaded from our [release page](https://gitlab.kitware.com/physiology/unity/releases).

### From Git

#### Install Git LFS
This project uses Git LFS for data (.csv, .json state files) and binaries (libraries in `Plugins`), so ensure that [Git LFS is installed](https://git-lfs.github.com/) and that you've run `git lfs install`.

### Setup the repository inside your Unity project
To be able to access the Pulse Unity Asset, this whole repository needs to live somewhere under the `Assets` folder of your Unity project. For example:
```
YourProject/
  Assembly-CSharp-Editor.csproj
  Assembly-CSharp.csproj
  Assets/
    PulsePhysiologyEngine/ <-- make this folder here
    ...
  Library/
  YourProject.sln
  obj/
  ProjectSettings/
  Temp/
  ...
```

Therefore:
```
mkdir YourProject/Assets/PulsePhysiologyEngine
cd YourProject/Assets/PulsePhysiologyEngine
git clone https://gitlab.kitware.com/physiology/unity .
```

If `YourProject` is version-controlled with Git, you can add our asset as a submodule instead:
```bash
mkdir YourProject/Assets/PulsePhysiologyEngine
cd YourProject/Assets/PulsePhysiologyEngine
git submodule add https://gitlab.kitware.com/physiology/unity
```

## How to use

The user manual needs to be embedded in the Unity asset package according to the Unity Asset Store submission guidelines ([section 3.2](https://unity3d.com/asset-store/sell-assets/submission-guidelines#content-organization)) and can therefore be found at
[`Documentation/PulseUnityAssetUserManual.pdf`](/Documentation/PulseUnityAssetUserManual.pdf)

To learn more about the Pulse api used by scripts in this repository, check out our [C# API Examples](https://gitlab.kitware.com/physiology/engine/-/tree/3.x/src/csharp/howto)

### Pulse Data

One instance of a Pulse Engine simulates 1, and only 1, patient.
You can initialize a Pulse patient in 2 ways:

#### Load a Pulse Engine state from a previous instance of a Pulse patient.

This is the easiest and quickest way to start Pulse.
This asset provides several [pregenerated patient states for you to use](/Data/states)
You can use these states to create your own new states as well.

You only need to provide Provide the Pulse API the state file, or its contents.
(StreamingAssets are not used when loading from a state file/string!)

#### Creating a new Patient in Pulse

If you wish to define your own patient, you may do so with the Pulse API.
When doing so, Pulse will need to read files from disk.
If you are doing this within Unity, you will need to copy the [Data](/Data) directory into your applications StreamingAssets folder.
You can rename it to anything you wish.

You will need to provide the Pulse [InitializeEngine](https://gitlab.kitware.com/physiology/engine/-/blob/3.x/src/csharp/pulse/engine/PulseEngine.cs#L84) method the location of this directory in your code, for example:

```C#
// Assuming you Copied the Data folder into your StreamingAssets folder and renamed it to 'PulseData'
string pulseDataPath = Application.streamingAssetsPath + "/PulseData/";
DirectoryInfo directoryInfo = new DirectoryInfo(pulseDataPath);
if (!directoryInfo.Exists)
{
    string error = "Data files for Pulse not found. Expected at " + pulseDataPath + ".\n" +
    "Make sure you have copied them from the Pulse Data folder to your projects 'StreamingAssets' folder.";
    throw new Exception(error);
}
if (!engine.InitializeEngine(cfg, data_mgr, pulseDataPath))
      Debug.unityLogger.LogError("PulsePhysiologyEngine", "Unable to initialize Pulse ");
```

## Pulse engine backend

Current version of Pulse engine: [REL_3_2_0](https://gitlab.kitware.com/physiology/engine/-/releases/REL_3_2_0)

### Plugins
Functionality from the C++ API of the Pulse engine are encapsulated in  the following [plugins](https://docs.unity3d.com/Manual/Plugins.html):
- **PulseC <sup>(native)</sup>:** a [C wrapper](https://gitlab.kitware.com/physiology/engine/-/tree/3.x/src/c) for the Pulse engine
  - `.dll` for Windows
  - `.so` for Linux, Lumin and Android
  - `.dylib` for MacOS
  - `.a` for Web Assembly
- **PulseEngine.dll <sup>(managed)</sup>:** a [C# interface](https://gitlab.kitware.com/physiology/engine/-/tree/3.x/src/csharp/pulse/engine) to the Pulse engine implemented by wrapping PulseC
- **CommonDataModel.dll <sup>(managed)</sup>:** a [C# implementation](https://gitlab.kitware.com/physiology/engine/-/tree/3.x/src/csharp/pulse/cdm) of the [common data model](http://pulse.kitware.com/_c_d_m.html) 
- **DataModelBindings.dll <sup>(managed)</sup>:** C# bindings autogenerated by protobuf of the common data model [schema](https://gitlab.kitware.com/physiology/engine/-/tree/3.x/src/schema)
- **Google.Protobuf.dll <sup>(managed)</sup>:** C# library of [protocol-buffers](https://github.com/protocolbuffers/protobuf/tree/master/csharp/src/Google.Protobuf)

### Building the Pulse Asset plugins

The binaries/plugins above will be built along with the Pulse engine (steps [here](https://gitlab.kitware.com/physiology/engine#building)), noting that:
- The native plugins (`PulseC`) need to be built on **each of your target platforms**
- The managed plugins can **only be built on Windows** but will then support every target platform

## Limitations

### IL2CPP Build Support

The Pulse Unity Asset will not work out of the box in Unity projects built with the [IL2CPP compiler](https://docs.unity3d.com/Manual/IL2CPP.html) due to [bytecode stripping](https://docs.unity3d.com/Manual/IL2CPP-BytecodeStripping.html).
The provided Pulse plugins are impacted by this (Google Protocol Buffers used in Pulse for serialization), which results in stripping of code that is actually necessary for Pulse.
A link.xml file (contents below) should be placed in your assets folder root to effectively disable bytecode stripping by preserving both types and full assemblies.
```xml
<linker>
  <assembly fullname="System" preserve="all"/>
  <assembly fullname="Google.Protobuf" preserve="all"/>
  <assembly fullname="Pulse" preserve="all"/>
</linker>
```

If you need to use the IL2CPP compiler and it is preventing you from integrating Pulse in your Unity application, let us know on our [Discourse support forum](https://discourse.kitware.com/c/pulse-physiology-engine).

### Missing C++ functionality from the Pulse engine in the C# assets

The Pulse Unity Asset only exposes a portion of the functionality available in the C++ interface of the Pulse engine. If you need to expose more functionality from the C++ library in Unity, you need to:
1. Update the C and C# implementations [of the plugins listed above](#plugins). You can contribute those changes to the [Pulse engine repository](https://gitlab.kitware.com/physiology/engine/blob/master/CONTRIBUTING.md).
2. Build those plugins as described [above](#building-the-pulse-asset-plugins)
3. Update those files in the [Plugins](/Plugins) directory. See the contribution guidelines [below](#contributing) to receive reviews and continuous support for your implementation.

If you are interested in collaborating with us to bring more of the Pulse engine functionality to Unity, you can contact us at kitware@kitware.com.

### Using a custom version/fork of PulseEngine?

Before we can bring any capabilities to the Pulse Unity Asset, they will need to be part of the Pulse Engine repository. You can contribute your changes [there](https://gitlab.kitware.com/physiology/engine/blob/master/CONTRIBUTING.md) to facilitate their integration to the Unity Asset in the future.
If you need help designing a custom solution based on Pulse, you can contact us at kitware@kitware.com.

## Contributing

### Found a bug

Ask your questions and share your remarks in our issue tracker on GitLab: 
https://gitlab.kitware.com/physiology/unity/issues

### Can't quite make it work?

You can find support on our [Discourse support forum](https://discourse.kitware.com/c/pulse-physiology-engine).

### Have a fix you'd like to contribute back?

You can follow this simple [forking workflow](https://docs.gitlab.com/ee/workflow/forking_workflow.html):
1. [Fork](https://docs.gitlab.com/ee/gitlab-basics/fork-project.html) this project in your own GitLab user or group
2. Commit the fix to a branch on top of `master`
3. Push the branch on your forked repository
4. Create a [merge request](https://docs.gitlab.com/ee/gitlab-basics/add-merge-request.html) in this repository
