using System;
using BepInEx;
using UnityEngine;
using Utilla;
using System.IO;
using System.Reflection;
using System.Collections;
using UnityEngine.InputSystem;



namespace StevesModdingTools
{
	// Modded stuff (You cannot ban me now!)
	[ModdedGamemode]
	[BepInDependency("org.legoandmars.gorillatag.utilla", "1.5.0")]
	[BepInPlugin(PluginInfo.GUID, PluginInfo.Name, PluginInfo.Version)]
	public class Plugin : BaseUnityPlugin
	{
		/// <summary>
		/// Refrences
		/// </summary>
		bool inRoom;
		bool isEnabled;
		bool isLoaded;
		bool isFakeLoaded; // :trol:
		// bool isExtreme = false; //Crashes the game when you try to load the menu in a public (godzilla had a stroke and died from reading this)

		//bool MenuSpawn;
		//public static GameObject MenuBack;
		//internal Material MenuMaterial;

		/// <summary>
		/// Mods
		/// </summary>

		// Platforms
		bool PlatEnable = false;
		bool PlatFormToggle = false;
		bool platSetR;
		bool platSetL;
		internal GameObject CustomPlatR;
		internal Material CubeMaterialR;
		internal GameObject CustomPlatL;
		internal Material CubeMaterialL;

		// Long Arms
		bool LarmsEnable = false;
		bool LarmToggle = false;
		float armval = 2.3f; // The float value to be changed by the slider

		// Fly
		bool FlyEnable = false;
		bool FlyToggle = false;
		float soarval = 1400f; // The float value to be changed by the slider

							 // Explode
		bool ExplodeEnable = false;
		bool ExplodeToggle = false;
		bool ExplodePause = false;
		float boomval = 1400f; // The float value to be changed by the slider

		/* Nuke
		bool NukeEnable = false;
		bool NukeToggle = false;
		bool NukePause = false;*/

		// Speedboost
		bool SBEnable = false;
		bool SBToggle = false;
		float speedval = 2.3f; // The float value to be changed by the slider

		// Client sided sound spammer
		bool FakeSpammerEnable = false;
		//bool FakeSpammerToggle = false;
		// CS Sound Spammer Sounds
		bool FakeSpammerMetalEnable = false;
		bool FakeSpammerMetalToggle = false;
		bool FakeSpammerDefaultEnable = false;
		bool FakeSpammerDefaultToggle = false;
		bool FakeSpammerFrogEnable = false;
		bool FakeSpammerFrogToggle = false;

		/// <summary>
		/// Owner Only Mods
		/// </summary>

		/* proof of da steiv
		bool SteveProofEnabled = false;
		bool SteveProofToggle = false;
		bool IsSteve = false;*/

		/// <summary>
		/// onGUI stuff
		/// </summary>
		bool isDebug = false; // Testing only, still only works in moddeds.
		bool GUIThingy = false;
		bool ClientSidedSection = false;
		bool MovementSection = false;
		bool CharacterSelection = false;

		void Start()
		{
			Utilla.Events.GameInitialized += OnGameInitialized;
		}

		void OnEnable()
		{
			isEnabled = true;
			HarmonyPatches.ApplyHarmonyPatches();
		}

		void OnDisable()
		{
			GorillaLocomotion.Player.Instance.transform.localScale = new Vector3(1f, 1f, 1f);
			isEnabled = false;
			HarmonyPatches.RemoveHarmonyPatches();
		}

		void OnGameInitialized(object sender, EventArgs e)
		{
			/*
			MenuBack = GameObject.CreatePrimitive(PrimitiveType.Cube);
			MenuBack.name = "MenuBack";
			MenuBack.transform.localScale = new Vector3(0.02853902f, 0.1977991f, 0.2621724f);
			MenuMaterial = new Material(Shader.Find("GorillaTag/UberShader"));
			MenuBack.GetComponent<Renderer>().material = MenuMaterial;
			BoxCollider uselessboxCollider = MenuBack.GetComponent<BoxCollider>();
			Destroy(uselessboxCollider);*/

			// Platform loading
			CustomPlatL = GameObject.CreatePrimitive(PrimitiveType.Cube);
			CustomPlatR = GameObject.CreatePrimitive(PrimitiveType.Cube);
			CustomPlatL.name = "CustomPlatL";
			CustomPlatR.name = "CustomPlatR";
			CustomPlatR.transform.localScale = new Vector3(0.25f, 0.04f, 0.25f);
			CustomPlatL.transform.localScale = new Vector3(0.25f, 0.04f, 0.25f);
			CubeMaterialR = new Material(Shader.Find("GorillaTag/UberShader"));
			CubeMaterialL = new Material(Shader.Find("GorillaTag/UberShader"));
			CustomPlatR.GetComponent<Renderer>().material = CubeMaterialR;
			CustomPlatL.GetComponent<Renderer>().material = CubeMaterialL;
			CubeMaterialR.SetColor("_Color", Color.black);
			CubeMaterialL.SetColor("_Color", Color.black);
			Instantiate(CustomPlatL);
			Instantiate(CustomPlatR);
			CustomPlatL.AddComponent<GorillaSurfaceOverride>();
			CustomPlatR.AddComponent<GorillaSurfaceOverride>();
			CustomPlatL.GetComponent<GorillaSurfaceOverride>().overrideIndex = (103);
			CustomPlatR.GetComponent<GorillaSurfaceOverride>().overrideIndex = (103);

			// Is loaded make it trueie woiueie
			isLoaded = true;
		}

		// Thank you Zayne!
		void Update()
		{
			// Unused
			//bool extrememodeconfig = Config.Bind("Extreme Mode", "False", false, "Crashes your game when you try and use a mod.").Value;
			//float longarmlength = Config.Bind("Long Arm Length", "1.6f", 1.6f, "Changes the length of the long arm mod.").Value;
			//float speedboost = Config.Bind("SpeedBoost", "1.3f", 1.3f, "what should I write here").Value;
			if (isFakeLoaded)
			{
				if (isLoaded)
				{
					if (isEnabled)
					{
						if (Keyboard.current.spaceKey.wasPressedThisFrame)
						{
							if (inRoom)
							{
								GUIThingy = !GUIThingy;
							}
						}
						if (Keyboard.current.ctrlKey.wasPressedThisFrame)
						{
							if (inRoom)
							{
								isDebug = !isDebug;
								GUIThingy = !GUIThingy;
							}
						}
						if (!inRoom)
						{
							GUIThingy = false;
						}
						if (isDebug)
						{
							//SBValue = GorillaLocomotion.Player.Instance.jumpMultiplier;
						}
						if (inRoom)
						{
							//if (!isDebug)
							{
								/*
								if (ControllerInputPoller.instance.rightControllerSecondaryButton)
								{
									if (MenuSpawn == false)
									{
										MenuBack.transform.eulerAngles = new Vector3(GorillaLocomotion.Player.Instance.rightControllerTransform.eulerAngles.x, GorillaLocomotion.Player.Instance.rightControllerTransform.eulerAngles.y, GorillaLocomotion.Player.Instance.rightControllerTransform.eulerAngles.z);
										MenuBack.transform.position = new Vector3(GorillaLocomotion.Player.Instance.rightControllerTransform.position.x, GorillaLocomotion.Player.Instance.rightControllerTransform.position.y, GorillaLocomotion.Player.Instance.rightControllerTransform.position.z);
										MenuSpawn = true;
									}
								}
								else
								{ 
									MenuSpawn = false;
									MenuBack.transform.position = new Vector3(0, 0, 0);
									MenuBack.transform.eulerAngles = new Vector3(0, 0, 0);
								}*/
								if (PlatEnable)
								{
									//right controller
									if (ControllerInputPoller.instance.rightControllerGripFloat >= 0.5f)
									{
										if (platSetR == false)
										{
											CustomPlatR.transform.eulerAngles = new Vector3(GorillaLocomotion.Player.Instance.rightControllerTransform.eulerAngles.x, GorillaLocomotion.Player.Instance.rightControllerTransform.eulerAngles.y, GorillaLocomotion.Player.Instance.rightControllerTransform.eulerAngles.z);
											CustomPlatR.transform.position = new Vector3(GorillaLocomotion.Player.Instance.rightControllerTransform.position.x, GorillaLocomotion.Player.Instance.rightControllerTransform.position.y, GorillaLocomotion.Player.Instance.rightControllerTransform.position.z);
											platSetR = true;
										}
									}
									else
									{
										platSetR = false;
										CustomPlatR.transform.position = new Vector3(0, 0, 0);
										CustomPlatR.transform.eulerAngles = new Vector3(0, 0, 0);
									}
									//left controller
									if (ControllerInputPoller.instance.leftControllerGripFloat >= 0.5f)
									{
										if (platSetL == false)
										{
											CustomPlatL.transform.eulerAngles = new Vector3(GorillaLocomotion.Player.Instance.leftControllerTransform.eulerAngles.x, GorillaLocomotion.Player.Instance.leftControllerTransform.eulerAngles.y, GorillaLocomotion.Player.Instance.leftControllerTransform.eulerAngles.z);
											CustomPlatL.transform.position = new Vector3(GorillaLocomotion.Player.Instance.leftControllerTransform.position.x, GorillaLocomotion.Player.Instance.leftControllerTransform.position.y, GorillaLocomotion.Player.Instance.leftControllerTransform.position.z);
											platSetL = true;
										}
									}
									else
									{
										platSetL = false;
										CustomPlatL.transform.position = new Vector3(0, 0, 0);
										CustomPlatL.transform.eulerAngles = new Vector3(0, 0, 0);
									}

								}
								if (LarmsEnable)
								{
									GorillaLocomotion.Player.Instance.transform.localScale = new Vector3(armval, armval, armval);
								}
								else
								{
									GorillaLocomotion.Player.Instance.transform.localScale = new Vector3(1f, 1f, 1f);
								}
								if (FlyEnable)
								{
									if (ControllerInputPoller.instance.rightControllerSecondaryButton)
									{
										GorillaLocomotion.Player.Instance.GetComponent<Rigidbody>().velocity = Camera.main.transform.forward * soarval * Time.deltaTime;
									}
								}
								if (ExplodeEnable)
								{
									if (ControllerInputPoller.instance.leftControllerSecondaryButton)
									{
										if (!ExplodePause)
										{
											Vector3 randomDirection = UnityEngine.Random.onUnitSphere;
											randomDirection.y = Mathf.Abs(randomDirection.y);
											GorillaLocomotion.Player.Instance.GetComponent<Rigidbody>().velocity = randomDirection * boomval * Time.deltaTime;
											Vibration(2);
											ExplodePause = true;
										}
									}
									else
									{
										ExplodePause = false;
									}
								}

								/*if (NukeEnable)
								{
									if (!ExplodeEnable)
									{
										if (ControllerInputPoller.instance.leftControllerSecondaryButton)
										{
											if (!NukePause)
											{
												Vector3 randomDirection = UnityEngine.Random.onUnitSphere;
												randomDirection.y = Mathf.Abs(randomDirection.y);
												GorillaLocomotion.Player.Instance.GetComponent<Rigidbody>().velocity = randomDirection * 9456f * Time.deltaTime;
												NukePause = true;
											}
										}
										else
										{
											NukePause = false;
										}
									}
								}*/
								if (SBEnable)
								{
									GorillaLocomotion.Player.Instance.jumpMultiplier = speedval;
									GorillaLocomotion.Player.Instance.maxJumpSpeed = 30.0f;
								}
								if (FakeSpammerEnable)
								{
									/* Example
									if(FakeSpammerSFXEnable)
									{

									}
									*/
									if (FakeSpammerMetalEnable)
									{
										if (ControllerInputPoller.instance.rightControllerGripFloat >= 0.5f)
										{
											Hitsound(18);
										}
									}
									if (FakeSpammerDefaultEnable)
									{
										if (ControllerInputPoller.instance.rightControllerGripFloat >= 0.5f)
										{
											Hitsound(0);
										}
									}
									if (FakeSpammerFrogEnable)
									{
										if (ControllerInputPoller.instance.rightControllerGripFloat >= 0.5f)
										{
											Hitsound(91);
										}
									}
								}
							}
							/*else
							{

							}*/
						}
						else
						{
							/*if(extrememodeconfig)
							{
								Application.Quit();
							}*/
						}
					}
				}
			}
			else
            {
				StartCoroutine(Fakeingaloding());
			}
		}

		IEnumerator Fakeingaloding()
		{
			// Print a message before waiting
			Debug.Log("Loading Modules");

			// Wait for 3 seconds
			yield return new WaitForSeconds(4.0f);

			// Print a message after waiting
			Debug.Log("Loaded");

			isFakeLoaded = true;
		}

		[ModdedGamemodeJoin]
		public void OnJoin(string gamemode)
		{
			inRoom = true;
		}

		[ModdedGamemodeLeave]
		public void OnLeave(string gamemode)
		{
			GorillaLocomotion.Player.Instance.transform.localScale = new Vector3(1f, 1f, 1f);
			inRoom = false;
		}
		public AssetBundle LoadAssetBundle(string path)
		{
			Stream stream = Assembly.GetExecutingAssembly().GetManifestResourceStream(path);
			AssetBundle bundle = AssetBundle.LoadFromStream(stream);
			stream.Close();
			return bundle;
		}

		// all 3 of these are really lazy so chin don't scream at me
		public void ButtonSFX(bool on)
        {
			if (on)
			{
				Hitsound(66);
				Vibration(2);
			}
			else
			{
				Hitsound(212);
				Vibration(2.2f);
			}

        }
		public void Hitsound(int hitsoundnumber)
        {
			GorillaTagger.Instance.offlineVRRig.PlayHandTapLocal(hitsoundnumber, true, 0.12f);
			GorillaTagger.Instance.offlineVRRig.PlayHandTapLocal(hitsoundnumber, false, 0.12f);
		}
		public void Vibration(float power)
        {
			GorillaTagger.Instance.StartVibration(true, GorillaTagger.Instance.tapHapticStrength / power, GorillaTagger.Instance.tapHapticDuration);
			GorillaTagger.Instance.StartVibration(false, GorillaTagger.Instance.tapHapticStrength / power, GorillaTagger.Instance.tapHapticDuration);
		}
		// Lazy ass gui but who gives a fuck
		void OnGUI()
		{
			// fake loading :trol:
			if (!isFakeLoaded)
			{
				GUI.Label(new Rect(0, 0, 200, 200), "Loading Steve's Modding Tools Mod, Please wait!");
			}

			if (GUIThingy)
			{
				// Branding
				GUI.color = Color.blue;

				//GUI.Label(new Rect(672, 580, 120, 90), "Credits: Steve, Zayne.");
				//GUI.Label(new Rect(672, 620, 200, 120), "Steve's Modding Tools");

				// Mod UI / Configuration
				/*if(!FakeSpammerEnable)
                {
					GUI.Label(new Rect(561, 620, 200, 120), "Client Sided");
                }*/	
				if (SBEnable)
                {
					GUI.color = Color.yellow;
					speedval = GUI.HorizontalSlider(new Rect(450, 820, 200, 20), speedval, 0.0f, 30.0f);
					GUI.Label(new Rect(450, 720, 200, 30), "Speed Configuration");
					GUI.color = Color.blue;
				}
				if (LarmsEnable)
				{
					GUI.color = Color.yellow;
					armval = GUI.HorizontalSlider(new Rect(120, 820, 200, 20), armval, 0.0f, 10.0f);
					GUI.Label(new Rect(120, 720, 200, 30), "Arm Configuration");
					GUI.color = Color.blue;
				}
				if (ExplodeEnable)
				{
					GUI.color = Color.yellow;
					boomval = GUI.HorizontalSlider(new Rect(340, 820, 200, 20), boomval, 1400f, 9456f);
					GUI.Label(new Rect(340, 720, 200, 30), "Blast Configuration");
					GUI.color = Color.blue;
				}
				if (FlyEnable)
				{
					GUI.color = Color.yellow;
					soarval = GUI.HorizontalSlider(new Rect(230, 820, 200, 20), soarval, 1400f, 9456f);
					GUI.Label(new Rect(230, 720, 200, 30), "Soar Configuration");
					GUI.color = Color.blue;
				}
				if(FakeSpammerEnable)
                {
					/* Example
					if (GUI.Button(new Rect(The last one + 111, 720, 120, 60), "Example Spammer"))
					{
						if (!FakeSpammerExampleToggle)
						{
							FakeSpammerExampleEnable = true;
							FakeSpammerExampleToggle = true;
							ButtonSFX(true);
						}
						else
						{
							FakeSpammerExampleEnable = false;
							FakeSpammerExampleToggle = false;
							ButtonSFX(false);
						}
					}*/
					GUI.color = Color.red;

					GUI.Label(new Rect(561, 420, 200, 120), "Disclamer: All of these only work on your side of the game!");

					GUI.color = Color.yellow;
					if (GUI.Button(new Rect(561, 720, 120, 60), "Default Spammer"))
					{
						if (!FakeSpammerDefaultToggle)
						{
							FakeSpammerDefaultEnable = true;
							FakeSpammerDefaultToggle = true;
							ButtonSFX(true);
						}
						else
						{
							FakeSpammerDefaultEnable = false;
							FakeSpammerDefaultToggle = false;
							ButtonSFX(false);
						}
					}
					if (GUI.Button(new Rect(561, 620, 120, 60), "Metal Spammer"))
					{
						if (!FakeSpammerMetalToggle)
						{
							FakeSpammerMetalEnable = true;
							FakeSpammerMetalToggle = true;
							ButtonSFX(true);
						}
						else
						{
							FakeSpammerMetalEnable = false;
							FakeSpammerMetalToggle = false;
							ButtonSFX(false);
						}
					}
					if (GUI.Button(new Rect(561, 520, 120, 60), "Frog Spammer"))
					{
						if (!FakeSpammerFrogToggle)
						{
							FakeSpammerFrogEnable = true;
							FakeSpammerFrogToggle = true;
							ButtonSFX(true);
						}
						else
						{
							FakeSpammerFrogEnable = false;
							FakeSpammerFrogToggle = false;
							ButtonSFX(false);
						}
					}
					GUI.color = Color.blue;
				}

				// Section Buttons
				if (GUI.Button(new Rect(920, 10, 120, 60), "Movement"))
				{
					if (!MovementSection)
					{
						MovementSection = true;
						ButtonSFX(true);
					}
					else
					{
						MovementSection = false;
						ButtonSFX(false);
					}
				}
				if (GUI.Button(new Rect(1031, 10, 120, 60), "Character"))
				{
					if (!CharacterSelection)
					{
						CharacterSelection = true;
						ButtonSFX(true);
					}
					else
					{
						CharacterSelection = false;
						ButtonSFX(false);
					}
				}
				if (GUI.Button(new Rect(1142, 10, 120, 60), "Client Sided"))
				{
					if (!ClientSidedSection)
					{
						ClientSidedSection = true;
						ButtonSFX(true);
					}
					else
					{
						ClientSidedSection = false;
						ButtonSFX(false);
					}
				}

				// The mod stuffs
				if (MovementSection)
				{
					if (GUI.Button(new Rect(10, 920, 120, 60), "Air Jump"))
					{
						if (!PlatFormToggle)
						{
							PlatEnable = true;
							PlatFormToggle = true;
							ButtonSFX(true);
						}
						else
						{
							PlatEnable = false;
							PlatFormToggle = false;
							ButtonSFX(false);
						}
					}
					if (GUI.Button(new Rect(230, 920, 120, 60), "Soar"))
					{
						if (!FlyToggle)
						{
							FlyEnable = true;
							FlyToggle = true;
							ButtonSFX(true);
						}
						else
						{
							FlyEnable = false;
							FlyToggle = false;
							ButtonSFX(false);
						}
					}
					if (GUI.Button(new Rect(340, 920, 120, 60), "Explode"))
					{
						if (!ExplodeToggle)
						{
							ExplodeEnable = true;
							ExplodeToggle = true;
							ButtonSFX(true);
						}
						else
						{
							ExplodeEnable = false;
							ExplodeToggle = false;
							ButtonSFX(false);
						}
					}
					if (GUI.Button(new Rect(450, 920, 120, 60), "Speed Configure"))
					{
						if (!SBToggle)
						{
							SBEnable = true;
							SBToggle = true;
							ButtonSFX(true);
						}
						else
						{
							SBEnable = false;
							SBToggle = false;
							ButtonSFX(false);
						}
					}
				}
				if(CharacterSelection)
                {
					if (GUI.Button(new Rect(120, 920, 120, 60), "Arm Configure"))
					{
						if (!LarmToggle)
						{
							LarmsEnable = true;
							LarmToggle = true;
							ButtonSFX(true);
						}
						else
						{
							LarmsEnable = false;
							LarmToggle = false;
							ButtonSFX(false);
						}
					}
				}
				if(ClientSidedSection)
                {
					GUI.color = Color.yellow;
					GUI.Label(new Rect(561, 820, 920, 120), "Uh oh, there is nothing here yet!");
					GUI.color = Color.blue;
					/*if (GUI.Button(new Rect(561, 920, 120, 60), "SoundSpammers"))
					{
						if (!FakeSpammerToggle)
						{
							FakeSpammerEnable = true;
							FakeSpammerToggle = true;
							ButtonSFX(true);
						}
						else
						{
							FakeSpammerEnable = false;
							FakeSpammerToggle = false;
							ButtonSFX(false);
						}
					}*/
				}
			}
		}
	}
}
