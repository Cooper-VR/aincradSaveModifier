using Newtonsoft.Json;
using System;
using System.IO;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Forms;

namespace aincradSaveModifier
{
	public partial class MainWindow : Window
	{
		#region script-wide variables
		private string stats;
		private string inventory;
		#endregion
		public MainWindow()
		{
			InitializeComponent();

			/*
			//calls the function to get the path of the folder where the file is in
			string path = this.getPath();
			this.path.Text = path;

			//completes the path for the json files
			this.stats = path + "\\avtr_5def9d3c-c59e-4b77-91fd-c7b23323db58";
			this.inventory = path + "\\avtr_73e1a1b0-d9b9-4dc4-9544-5dae72ea8e64";
		*/
		}

		/*
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
		#endregion

		#region button presses
		/// <summary>
		/// this will apply the setting the user has set to the correct numbers and write it to the files
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void GetData(object sender, RoutedEventArgs e)
		{
			//gets all the data I might need including old data
			double[] statData = new double[16];
			double[] stats = jsonStrip(jsonDecompile(this.stats));

			double[] inventoryData = new double[16];
			double[] inventory = jsonStrip(jsonDecompile(this.stats));

			#region set stats
			statData[0] = stats[0];
			stats[1] = ((double)this.exp.Value) / (double)65536;
			stats[2] = ((double)this.health.Value) / (double)256;
			stats[3] = ((double)this.col.Value) / (double)65536;
			stats[4] = stats[4];
			stats[5] = stats[5];
			stats[6] = stats[6];
			#endregion

			#region set inventory
			
			for (int i = 0; i < 7; i++)
			{
				string elementName = "item" + i.ToString();
				var currentElement = this.FindName(elementName) as System.Windows.Controls.ListBox;
				var selectedElement = currentElement.SelectedItem as ListBoxItem;
				string elementContent = selectedElement.Content.ToString();

				string amountName = "slot" + i.ToString() + "Amount";
				var currentAmountElement = this.FindName(amountName) as System.Windows.Controls.TextBox;
				int currentAmount;
				int.TryParse(currentAmountElement.Text, out currentAmount);

				inventoryData[i] = itemValue(elementContent, currentAmount);
			}

			int currentIndex = 0;
			for (int i = 7; i < 15; i++)
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

			#endregion
		}
		#endregion */
	}
}
