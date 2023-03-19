using Newtonsoft.Json;
using System;
using System.IO;
using System.Windows;
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

			//calls the function to get the path of the folder where the file is in
			string path = this.getPath();
			this.path.Text = path;

			//completes the path for the json files
			this.stats = path + "\\avtr_5def9d3c-c59e-4b77-91fd-c7b23323db58";
			this.inventory = path + "\\avtr_73e1a1b0-d9b9-4dc4-9544-5dae72ea8e64";
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
		/// <returns>returns a  alue 0-1 of the item it is</returns>
		private double itemValue()
		{
			int teleport = 769;
			int mirror = 1025;
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
			stats[1] = ((short)this.exp.Value) / (double)65536;
			stats[2] = ((short)this.health.Value) / (double)256;
			stats[3] = ((short)this.col.Value) / (double)65536;
			stats[4] = stats[4];
			stats[5] = stats[5];
			stats[6] = stats[6];
			#endregion
			#region set inventory

			#endregion
		}
		#endregion
	}
}
