using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Numerics;
using System.Text.RegularExpressions;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Forms;
using System.Windows.Media;

namespace aincradSaveModifier
{
	public partial class MainWindow : Window
	{
		#region script-wide variables
		private string stats;
		private string inventory;
		private string path;
		#endregion
		
		/// <summary>
		/// this method is called when the applications is started
		/// </summary>
		public MainWindow()
		{
			InitializeComponent();

			//this sets the listBoxItems with the saved paths
			string[] sortedPaths = checkPathFile();
			File.WriteAllText("BaseData/savedPaths.txt", string.Empty);

			this.userIDS.Items.Clear();
			for (int i = 0; i < sortedPaths.Length; i++)
			{
				using (StreamWriter writer = new StreamWriter("BaseData/savedPaths.txt", true))
				{
					if (sortedPaths[i] != "")
					{
						writer.Write(sortedPaths[i] + "$");

						pathItemBox(sortedPaths[i].Split("\\")[sortedPaths[i].Split("\\").Length - 1]);
					}

				}
			}
		}

		#region private helpers
		/// <summary>
		/// turns json data into a dictionary, works with any file type as long as its in json format, returns the json data in a dictionary format
		/// </summary>
		/// <param name="path">the path to the file to read</param>
		/// <returns>returns the json data in a dictionary format</returns>
		private dynamic jsonDecompile(string path)
		{
			while (true)
			{
				try
				{
					string file = File.ReadAllText(path);
					dynamic jsonData = JsonConvert.DeserializeObject(file);
					for (int i = 0; i < 16; i++)
					{
						string parameterName = "newparameter_" + i.ToString();
						jsonData.animationParameters[i].name = parameterName;
					}

					return jsonData;
				}
				catch (Exception ex)
				{
					System.Windows.MessageBox.Show(ex.Message);
					path = getPath();
				}
			}
		}

		/// <summary>
		/// strips downs the json data dictionary into a list, this is meant only form vrc paremeters
		/// </summary>
		/// <param name="jsonData">a dictionary that was gotten from a json file</param>
		/// <returns>returns the json data striped down into a list</returns>
		private double[] jsonStrip(dynamic jsonData)
		{
			double[] json = new double[16];
			for (int i = 0; i<json.Length; i++)
			{
				json[i] = jsonData["animationParameters"][i]["value"];
			}
			return json;
		}

		/// <summary>
		/// opens a diolage box and returns a string of the path
		/// </summary>
		/// <returns>string of file path</returns>
		private string getPath()
		{
			var dialog = new FolderBrowserDialog();
			dialog.ShowDialog();
			string selectedPath = dialog.SelectedPath;

			return selectedPath;
		}

		/// <summary>
		/// will take in the value of the item name and find the decimal value it should be 
		/// </summary>
		/// <returns>returns a value 0-1 of the item it is</returns>
		private double itemValue(string itemName, int amount)
		{
			#region item ids
			int teleport = 769;
			int mirror = 1025;
			int healingCrystal = 2049;
			int curingCrystal = 2305;
			int potion = 5377;
			int fireWork = 5633;
			int punpkin = 5889;

			int blackGreatSword = 257;
			int darkRepulser = 513;
			int woodenSword = 1281;
			int excalibur = 1537;
			int excaliburPF = 1793;
			int blueLongSword = 2561;
			int groundGorge = 2817;
			int vespidRapiar = 3073;
			int bloodThorn = 3329;
			int divineBlade = 3841;
			int dragonSlayer = 4097;
			int ironKatana = 4353;
			int ironSword = 4609;
			int ironSpear = 4865;
			int steelSword = 5121;
			int AbyssalCleaver = 5889;
			int carrotClaymore = 6145;
			int carrotClunker = 6401;
			int carrotImpaler = 6657;
			int cocoaClash = 6913;
			int cocoaStream = 7169;
			int cocoaThunder = 7425;
			int rageoutRage = 7681;
			int rageoutRipper = 7937;
			int witchBroom = 8193;
			int wereTears = 8449;
			int soulChiller = 8705;
			int graveDagger = 8961;
			int monsterMasher = 9217;
			int elucidator = 9473;
			int lampentLight = 9729;
			int bronzeDagger = 9985;
			int bronzeGreataxe = 10241;
			int bronzeGreatSword = 10497;
			int bronzeMace = 10753;
			int bronzeRapiar = 11009;
			int bronzeSpear = 11265;
			int bronzeSword = 11521;
			int quartanier = 11777;
			int ironAxe = 12033;
			int Axemas = 12289;
			int Blittzard = 12545;
			#endregion

			#region switchStament
			switch (itemName)
			{
				case ("teleport"):
					return ((double)(teleport + amount - 1)) / (double)65536;
				case ("mirror"):
					return ((double)(mirror + amount - 1)) / (double)65536;
				case ("healing crystal"):
					return ((double)(healingCrystal + amount - 1)) / (double)65536;
				case ("curing crystal"):
					return ((double)(curingCrystal + amount - 1)) / (double)65536;
				case ("potion"):
					return ((double)(potion + amount - 1)) / (double)65536;
				case ("firework"):
					return ((double)(fireWork + amount - 1)) / (double)65536;
				case ("punpkin"):
					return ((double)(punpkin + amount - 1)) / (double)65536;

				case ("black great sword"):
					return (double)blackGreatSword / (double)65536;
				case ("dark repulser"):
					return (double)darkRepulser / (double)65536;
				case ("wooden sword"):
					return (double)woodenSword / (double)65536;
				case ("excalibur"):
					return (double)excalibur / (double)65536;
				case ("excalibur pf"):
					return (double)excaliburPF / (double)65536;
				case ("blue long sword"):
					return (double)blueLongSword / (double)65536;
				case ("ground gorge"):
					return (double)groundGorge / (double)65536;
				case ("prometheus wrath"):
					return (double)3073 / (double)65536;
				case ("vespid rapier"):
					return (double)vespidRapiar / (double)65536;
				case ("blood thorn"):
					return (double)bloodThorn / (double)65536;
				case ("divine blade"):
					return (double)divineBlade / (double)65536;
				case ("dragon slayer"):
					return (double)dragonSlayer / (double)65536;
				case ("iron katana"):
					return (double)ironKatana / (double)65536;
				case ("iron sword"):
					return (double)ironSword / (double)65536;
				case ("iron spear"):
					return (double)ironSpear / (double)65536;
				case ("steel sword"):
					return (double)steelSword / (double)65536;
				case ("abyssal cleaver"):
					return (double)AbyssalCleaver / (double)65536;
				case ("carrot claymore"):
					return (double)carrotClaymore / (double)65536;
				case ("carrot clunker"):
					return (double)carrotClunker / (double)65536;
				case ("carrot impaler"):
					return (double)carrotImpaler / (double)65536;
				case ("cocoa clash"):
					return (double)cocoaClash / (double)65536;
				case ("cocoa Stream"):
					return (double)cocoaStream / (double)65536;
				case ("cocoa Thunder"):
					return (double)cocoaThunder / (double)65536;
				case ("rageout Rage"):
					return (double)rageoutRage / (double)65536;
				case ("rageout Ripper"):
					return (double)rageoutRipper / (double)65536;
				case ("witch Broom"):
					return (double)witchBroom / (double)65536;
				case ("were Tears"):
					return (double)wereTears / (double)65536;
				case ("soul Chiller"):
					return (double)soulChiller / (double)65536;
				case ("grave Dagger"):
					return (double)graveDagger / (double)65536;
				case ("monster Masher"):
					return (double)monsterMasher / (double)65536;
				case ("elucidator"):
					return (double)elucidator / (double)65536;
				case ("lampent Light"):
					return (double)lampentLight / (double)65536;
				case ("bronze Dagger"):
					return (double)bronzeDagger / (double)65536;
				case ("bronze Greataxe"):
					return (double)bronzeGreataxe / (double)65536;
				case ("bronze GreatSword"):
					return (double)bronzeGreatSword / (double)65536;
				case ("bronze Mace"):
					return (double)bronzeMace / (double)65536;
				case ("bronze Rapiar"):
					return (double)bronzeRapiar / (double)65536;
				case ("bronze Spear"):
					return (double)bronzeSpear / (double)65536;
				case ("bronze Sword"):
					return (double)bronzeSword / (double)65536;
				case ("quartanier"):
					return (double)quartanier / (double)65536;
				case ("iron Axe"):
					return (double)ironAxe / (double)65536;
				case ("Axemas"):
					return (double)Axemas / (double)65536;
				case ("Blittzard"):
					return (double)Blittzard / (double)65536;
				default:
					return 0;
			}
			#endregion
		}

		/// <summary>
		/// this simply creates a listBoxItem with some styling
		/// </summary>
		/// <param name="content"></param>
		public void pathItemBox(string content)
		{
			//create a new listboxitem
			var listBox = new System.Windows.Controls.ListBoxItem();

			//set the properties
			listBox.Background = (Brush)new BrushConverter().ConvertFrom("#001D2A");
			listBox.BorderThickness = new Thickness(2);
			listBox.Foreground = new SolidColorBrush(Colors.White);
			listBox.Content = content;
			listBox.FontFamily = new FontFamily("/Fonts/#SAO UI TT");
			listBox.FontSize = 18;

			//add the listbox to the xaml
			this.userIDS.Items.Add(listBox);

		}

		/// <summary>
		/// this will open the savedPaths file and turn the text into an array and remove any repeating paths
		/// </summary>
		/// <returns>returns a new array with the with the unique paths</returns>
		public string[] checkPathFile()
		{
			//create a new string array with paths
			string[] NewPathArray = File.ReadAllText("BaseData/savedPaths.txt").Split("$");

			List<string> uniqueItems = new List<string>();

			//use this to make a list of only the unique items
			foreach (string item in NewPathArray)
			{
				if (!uniqueItems.Contains(item))
				{
					uniqueItems.Add(item);
				}
			}

			//convert it to an array
			string[] newArray = uniqueItems.ToArray();

			return newArray;
		}

		/// <summary>
		/// this simply gets the slider values and sets then as the paremeters
		/// </summary>
		/// <returns>returns a double array with the location parameters</returns>
		private double[] SetLoation()
		{
			//this method is not set, need more data to see the cords better, will spawn user at TOB
			double[] stats = new double[3];
			stats[0] = this.xLocation.Value;
			stats[1] = this.yLocation.Value;
			stats[2] = stats[2];

			return stats;
		}

		/// <summary>
		/// this method sets playtime parameters
		/// </summary>
		/// <returns>returns a double array with the paremeters for playtime</returns>
		private double[] SetPlayTime()
		{
			//this is all the known values
			double[] playTime = new double[7];
		
			playTime[0] = -1;
			playTime[1] = -1;

			int hours;
			int minutes;

			//will parse the hours and min
			if (int.TryParse(this.Hours.Text, out hours) != true)
			{
				System.Windows.MessageBox.Show("invalid input for hours");
				hours = 100;
			}

			if (int.TryParse(this.Hours.Text, out minutes) != true)
			{
				System.Windows.MessageBox.Show("invalid input for minutes");
				minutes = 0;
			}

			//convert to seconds to compare to time mulipliers
			int totalSeconds = (hours * 3600) + (minutes * 60);

			double bigMuliplier = 18 * 3600 + 12 * 60;
			double smallMuliplier = 4 * 60 + 20;

			int bigIterations;
			int smallIterations;

			//check to see if it can make param 11 bigger than 0
			if (totalSeconds > bigMuliplier)
			{
				bigIterations = (int)Math.Floor((totalSeconds / bigMuliplier));
				playTime[2] = (double)bigIterations / (double)256;
			}

			double remainingSeconds = totalSeconds % bigMuliplier;

			//this will make param 12 the propert value
			if (remainingSeconds > smallMuliplier)
			{
				smallIterations = (int)Math.Floor((remainingSeconds / smallMuliplier));
				playTime[3] = (double)smallIterations / (double)65536;
			}

			//set these to placeholder numbers to check and maybe change later
			playTime[4] = -1;
			playTime[5] = -1;
			playTime[6] = -1;

			return playTime;
		}
		#endregion

		#region button presses
		/// <summary>
		/// this will apply the setting the user has set to the correct numbers and write it to the files
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void GetData(object sender, RoutedEventArgs e)
		 {
			
			//check if the path saved is valid, return if null because of denied access error
			if (this.path == null)
			{
				System.Windows.MessageBox.Show("file location not specified");
				return;
			}

			//will append avtr names to base path to read and write
			if (path != string.Empty)
			{
				this.stats = path + "\\avtr_5def9d3c-c59e-4b77-91fd-c7b23323db58";
				this.inventory = path + "\\avtr_73e1a1b0-d9b9-4dc4-9544-5dae72ea8e64";
			}

			//create two arrays for each one is new data, other is old data
			double[] statData = new double[16];
			double[] stats = jsonStrip(jsonDecompile(this.stats));

			double[] inventoryData = new double[16];
			double[] inventory = jsonStrip(jsonDecompile(this.inventory));

			#region set stats
			statData[0] = stats[0];
			stats[1] = ((double)this.exp.Value) / (double)65536;
			//health is spotty with the muliplier, its 250 * (amount/256), not value * 256
			stats[2] = ((double)this.health.Value) / (double)256;
			stats[3] = ((double)this.col.Value) / (double)65536;

			double[] LocationData = SetLoation();

			//set the locaton data, don't worry about z aincrad chek if its valid
			stats[4] = LocationData[0];
			stats[5] = LocationData[1];
			stats[6] = LocationData[2];

			double[] playTime = SetPlayTime();
			
			//just set individually and dont check for "-1" value
			stats[7] = stats[7];
			stats[8] = stats[8];
			stats[9] = stats[9];
			stats[10] = stats[10];
			stats[11] = playTime[2];
			stats[12] = playTime[3];
			stats[13] = stats[13];
			stats[14] = stats[14];
			stats[15] = stats[15];

			//base string repusenting the save data unwritten
			string inputString = "{\"animationParameters\":[{\"name\":\"newparameter_0\", \"value\":placeHolder}, {\"name\":\"newparameter_1\", \"value\":placeHolder}, {\"name\":\"newparameter_2\", \"value\":placeHolder}, {\"name\":\"newparameter_3\", \"value\":placeHolder}, {\"name\":\"newparameter_4\", \"value\":placeHolder}, {\"name\":\"newparameter_5\", \"value\":placeHolder}, {\"name\":\"newparameter_6\", \"value\":placeHolder}, {\"name\":\"newparameter_7\", \"value\":placeHolder}, {\"name\":\"newparameter_8\", \"value\":placeHolder}, {\"name\":\"newparameter_9\", \"value\":placeHolder}, {\"name\":\"newparameter_10\", \"value\":placeHolder}, {\"name\":\"newparameter_11\", \"value\":placeHolder}, {\"name\":\"newparameter_12\", \"value\":placeHolder}, {\"name\":\"newparameter_13\", \"value\":placeHolder}, {\"name\":\"newparameter_14\", \"value\":placeHolder}, {\"name\":\"newparameter_15\", \"value\":placeHolder}]}";
			string statsString = inputString;
			//this will replace "placeholder" withcorrosplonding values
			for (int i = 0; i < 16; i++)
			{
				var regex = new Regex(Regex.Escape("placeHolder"));
				statsString = regex.Replace(statsString, stats[i].ToString(), 1);
			}

			//clear the file
			File.WriteAllText(this.stats, string.Empty);
			//write the string to the file
			using (StreamWriter writer = new StreamWriter(this.stats, true))
			{
				writer.Write(statsString);
				writer.Close();
			}
			#endregion
			
			#region set inventory
			for (int i = 0; i < 7; i++)
			{
				string elementName = "item" + i.ToString();

				try
				{
					var listBox = this.FindName(elementName) as System.Windows.Controls.ListBox;
					var selectedElement = listBox.SelectedItem as System.Windows.Controls.ListBoxItem;
					string elementContent = selectedElement.Content.ToString();

					if (elementContent != "mirror")
					{
						string amountName = "slot" + i.ToString() + "Amount";
						var currentAmountElement = this.FindName(amountName) as System.Windows.Controls.TextBox;
						string amount = currentAmountElement.Text.ToString();
						int currentAmount;

						if (int.TryParse(amount, out currentAmount) == false)
						{
							currentAmount = 1;
						}


						if (currentAmount < 0)
						{
							currentAmount = Math.Abs(currentAmount);
						}
						if (currentAmount > 64)
						{
							currentAmount = 64;
						}


						inventoryData[i] = itemValue(elementContent, currentAmount);
					}
					else
					{
						string amountName = "slot" + i.ToString() + "Amount";
						var currentAmountElement = this.FindName(amountName) as System.Windows.Controls.TextBox;
						string amount = currentAmountElement.Text.ToString();
						int currentAmount;

						currentAmount = 1;


						inventoryData[i] = itemValue(elementContent, currentAmount);
					}

					
				} catch (System.NullReferenceException ex)
				{
					Debug.WriteLine(ex.Message);
					inventory[i] = 0;
				}
			}

			int currentIndex = 0;
			for (int i = 7; i < 16; i++)
			{
				string elementName = "weapons" + currentIndex.ToString();
				var currentElement = this.FindName(elementName) as System.Windows.Controls.ListBox;

				try
				{
					var selectedElement = currentElement.SelectedItem as ListBoxItem;
					string elementContent = selectedElement.Content.ToString();

					inventoryData[i] = itemValue(elementContent, 0);

					currentIndex++;
				}
				catch
				{
					inventoryData[i] = 0;

					currentIndex++;
				}
			}

			string inventoryString = inputString;

			//this will replace "placeholder" i, keep the Regex class in mind
			for (int i = 0; i < 16; i++)
			{
				var regex = new Regex(Regex.Escape("placeHolder"));
				inventoryString = regex.Replace(inputString, inventoryData[i].ToString(), 1);
			}

			File.WriteAllText(this.inventory, string.Empty);

			using (StreamWriter writer = new StreamWriter(this.inventory, true))
			{
				writer.Write(inventoryString);
				writer.Close();
			}
			#endregion
		}

		#region view switches;
		/// <summary>
		/// this switches the view by changing visibility
		/// </summary>
		public void ViewSwitch()
		{
			if (this.HomeButton.IsChecked== true)
			{
				this.InventoryWindow.Visibility  = Visibility.Hidden;
				this.StatsWindow.Visibility = Visibility.Hidden;
				this.LoginWindow.Visibility = Visibility.Visible;
			} else if (this.StatsButton.IsChecked == true) 
			{
				this.LoginWindow.Visibility = Visibility.Hidden;
				this.InventoryWindow.Visibility = Visibility.Hidden;
				this.StatsWindow.Visibility = Visibility.Visible;
			} else if (InventoryButton.IsChecked == true)
			{
				this.LoginWindow.Visibility = Visibility.Hidden;
				this.StatsWindow.Visibility = Visibility.Hidden;
				this.InventoryWindow.Visibility = Visibility.Visible;
			} else
			{
				this.HomeButton.IsChecked = true;
			}
		} 

		/// <summary>
		/// for when view is to be switched
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void InventoryPressed(object sender, RoutedEventArgs e)
		{
			ViewSwitch();
		}

		/// <summary>
		/// for when view is to be switched
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void HomePressed(object sender, RoutedEventArgs e)
		{
			ViewSwitch();
		}

		/// <summary>
		/// for when view is to be switched
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void StatsPressed(object sender, RoutedEventArgs e)
		{
			ViewSwitch();
		}
		#endregion
		
		/// <summary>
		/// this will tell the user to find their data. it then will check if the data is there and add it to the savedPaths file
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void FindFiles(object sender, RoutedEventArgs e)
		{
			string folder = getPath();
			string statsFile;
			string inventoryFile;
			bool statsFound;
			bool inventoryFound;

			statsFile = folder + "\\avtr_5def9d3c-c59e-4b77-91fd-c7b23323db58";
			inventoryFile = folder + "\\avtr_73e1a1b0-d9b9-4dc4-9544-5dae72ea8e64";

			

			statsFound = File.Exists(statsFile);
			inventoryFound = File.Exists(inventoryFile);

			while (statsFound != true || inventoryFound != true)
			{
				folder = getPath();

				statsFile = folder + "\\avtr_5def9d3c-c59e-4b77-91fd-c7b23323db58";
				inventoryFile = folder + "\\avtr_73e1a1b0-d9b9-4dc4-9544-5dae72ea8e64";

				statsFound = File.Exists(statsFile);
				inventoryFound = File.Exists(inventoryFile);

				if (statsFound != true || inventoryFound != false)
				{
					this.StatsStatus.Text = "not found";
					this.InventoryStatus.Text = "not found";

					System.Windows.MessageBox.Show("data not found, maybe try creating new data or finding the correct folder");
					break;
				}
			}
			this.StatsStatus.Text = "found";
			this.InventoryStatus.Text = "found";

			this.path = folder;

			string[] sortedPaths;

			using (StreamWriter writer = new StreamWriter("BaseData/savedPaths.txt", true))
			{
				writer.Write(folder + "$");
				writer.Close();
			}
			sortedPaths = checkPathFile();
			File.WriteAllText("BaseData/savedPaths.txt", string.Empty);

			this.userIDS.Items.Clear();
			for (int i = 0; i < sortedPaths.Length; i++)
			{
				using (StreamWriter writer = new StreamWriter("BaseData/savedPaths.txt", true))
				{
					if (sortedPaths[i] != "")
					{
						writer.Write(sortedPaths[i] + "$");

						pathItemBox(sortedPaths[i].Split("\\")[sortedPaths[i].Split("\\").Length - 1]);
					}
					
				}
				
			}
		}

		/// <summary>
		/// this will have the user open the folder to create the files in, and create them. it will then add the path to the savedPaths file
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void CreateFiles(object sender, RoutedEventArgs e)
		{
			string folderPath;
			System.Windows.MessageBox.Show("go to vrchat local avatar data folder");
			folderPath = getPath();

			while (folderPath == string.Empty) 
			{
				System.Windows.MessageBox.Show("go to vrchat local avatar data folder");
				folderPath = getPath();
			}

			string UserID = folderPath + "$";

			using (StreamWriter writer = new StreamWriter("BaseData/savedPaths.txt", true))
			{
				writer.WriteLine(folderPath + "$");
				writer.Close();

				this.userIDS.Items.Clear();

				for (int i = 0; i < File.ReadAllText("BaseData/savedPaths.txt").Split("$").Length; i++)
				{
					string wholeFile = File.ReadAllText("BaseData/savedPaths.txt");
					string currentFile = wholeFile.Split("$")[i];
					pathItemBox(currentFile.Split("\\")[currentFile.Split("\\").Length - 1]);
				}
			}

			try
			{
				File.Copy("BaseData\\avtr_5def9d3c-c59e-4b77-91fd-c7b23323db58", folderPath + "\\avtr_5def9d3c-c59e-4b77-91fd-c7b23323db58");
				System.Windows.MessageBox.Show("Save File Created");
			} catch (Exception ex)
			{
				System.Windows.MessageBox.Show("Save File NOT Created");
			}

			try
			{
				File.Copy("BaseData\\avtr_73e1a1b0-d9b9-4dc4-9544-5dae72ea8e64", folderPath + "\\avtr_73e1a1b0-d9b9-4dc4-9544-5dae72ea8e64");
				System.Windows.MessageBox.Show("Save File Created");
			}
			catch (Exception ex)
			{
				System.Windows.MessageBox.Show("Save File NOT Created");
			}
		}

		/// <summary>
		/// this will get the selected item from a list box and get the path from the savedPaths file and load the save files
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void UseSelected(object sender, RoutedEventArgs e)
		{
			int index = this.userIDS.SelectedIndex;

			string wholeFile = File.ReadAllText("BaseData/savedPaths.txt");
			string selectedPath = wholeFile.Split("$")[index];

			string statsFile;
			string inventoryFile;
			bool statsFound;
			bool inventoryFound;

			statsFile = selectedPath + "\\avtr_5def9d3c-c59e-4b77-91fd-c7b23323db58";
			inventoryFile = selectedPath + "\\avtr_73e1a1b0-d9b9-4dc4-9544-5dae72ea8e64";


			statsFound = File.Exists(statsFile);
			inventoryFound = File.Exists(inventoryFile);

			while (statsFound != true || inventoryFound != true)
			{
				selectedPath = getPath();

				statsFile = selectedPath + "\\avtr_5def9d3c-c59e-4b77-91fd-c7b23323db58";
				inventoryFile = selectedPath + "\\avtr_73e1a1b0-d9b9-4dc4-9544-5dae72ea8e64";

				statsFound = File.Exists(statsFile);
				inventoryFound = File.Exists(inventoryFile);

				if (statsFound != true || inventoryFound != false)
				{
					this.InventoryStatus.Text = "not found";
					this.StatsStatus.Text = "not found";
					System.Windows.MessageBox.Show("data not found");
				}
			}
			this.path = selectedPath;

			this.StatsStatus.Text = "found";
			this.InventoryStatus.Text = "found";
		}
		#endregion
	}
}
