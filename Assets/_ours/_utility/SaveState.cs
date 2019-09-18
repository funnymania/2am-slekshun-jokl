using UnityEngine; // For Debug.Log, etc.
 
using System.Text;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
 
using System;
using System.Runtime.Serialization;
using System.Reflection;

[Serializable ()]
public class SaveState : ISerializable {
	
	#region Data
	public static bool SC1_KeyMouth = false;
	public static bool INT1_KeyMouth = false;
	public static bool eventFlag = false;
	public static bool startScreen = false;
	public int odolsAccum = 3;
	public float score;
	#endregion
	
	// The default constructor. Included for when we call it during Save() and Load()
	public SaveState () {}
	 
	// This constructor is called automatically by the parent class, ISerializable
	// We get to custom-implement the serialization process here
	public SaveState (SerializationInfo info, StreamingContext ctxt)
	{
		// Get the values from info and assign them to the appropriate properties. Make sure to cast each variable.
		// Do this for each var defined in the Values section above
		SC1_KeyMouth=(bool)info.GetValue("SC1_KeyMouth",typeof(bool));
		score=(float)info.GetValue("score",typeof(float));
		odolsAccum=(int)info.GetValue("odolsAccum",typeof(int));
	}
	 
	// Required by the ISerializable class to be properly serialized. This is called automatically
	public void GetObjectData (SerializationInfo info, StreamingContext ctxt)
	{
		// Repeat this for each var defined in the Values section
		info.AddValue("SC1_KeyMouth",(SC1_KeyMouth));
		info.AddValue("score",score);
		info.AddValue("levelReached",odolsAccum);
	}
}
	 
// === This is the class that will be accessed from scripts ===
public class SaveLoad {
	public static string currentFilePath="SaveState.mdm"; // Edit this for different save files
	 
	// Call this to write data
	public static void Save () // Overloaded
	{
		Save(currentFilePath);
	}

	public static void Save(string filePath)
	{
		SaveState data=new SaveState();
		 
		Stream stream=File.Open(filePath,FileMode.Create);
		BinaryFormatter bformatter=new BinaryFormatter();
		bformatter.Binder=new VersionDeserializationBinder();
		bformatter.Serialize(stream,data);
		stream.Close();
	}
		 
	// Call this to load from a file into "data"
	public static void Load() { Load(currentFilePath); } // Overloaded
	
	public static void Load(string filePath)
	{
		SaveState data=new SaveState();
		Stream stream=File.Open(filePath,FileMode.Open);
		BinaryFormatter bformatter=new BinaryFormatter();
		bformatter.Binder=new VersionDeserializationBinder();
		data=(SaveState)bformatter.Deserialize(stream);
		stream.Close();
		 
		// Now use "data" to access your Values
	}}
 
// === This is required to guarantee a fixed serialization assembly name, which Unity likes to randomize on each compile
// Do not change this
public sealed class VersionDeserializationBinder : SerializationBinder
{
	public override Type BindToType(string assemblyName,string typeName)
	{
		if (!string.IsNullOrEmpty(assemblyName) && !string.IsNullOrEmpty(typeName))
		{
			Type typeToDeserialize=null;
			 
			assemblyName=Assembly.GetExecutingAssembly().FullName;
			 
			// The following line of code returns the type.
			typeToDeserialize=Type.GetType(String.Format("{0}, {1}",typeName,assemblyName));
			 
			return typeToDeserialize;
		}
		return null;
	}
}