using System.Windows;
using System.Windows.Controls;
using System;
using VRChat.API.Api;
using VRChat.API.Client;
using VRChat.API.Model;
using System.Diagnostics;
using System.Net;

namespace aincradSaveModifier.MVVM.View
{
	/// <summary>
	/// Interaction logic for HomeView.xaml
	/// </summary>
	public partial class HomeView : UserControl
	{
		public string userID = string.Empty;
		public HomeView()
		{
			InitializeComponent();
		}

		public async void LoginUser(object sender, RoutedEventArgs e)
		{
			Configuration Config = new Configuration();
			Config.Username = this.userName.Text;
			Config.Password = GetPasswordText(this.Password);

			// Create instances of API's we'll need
			AuthenticationApi AuthApi = new AuthenticationApi(Config);
			UsersApi UserApi = new UsersApi(Config);

			try
			{
				// Calling "GetCurrentUser(Async)" logs you in if you are not already logged in.
				CurrentUser CurrentUser = await AuthApi.GetCurrentUserAsync();
				if (CurrentUser != null)
				{
					Debug.WriteLine(CurrentUser.Id);
					this.Status.Text = "logged in";
					userID = CurrentUser.Id;
				}
				else
				{
					Config = new Configuration();
					
					Config.AddApiKey("auth", this.AuthBox.Text);
					Config.BasePath = "https://api.vrchat.cloud/api/1";

					this.AuthBox.Visibility = Visibility.Visible;
					this.AuthMessage.Visibility= Visibility.Visible;
					Debug.WriteLine("Failed to get current user");
					this.Status.Text = "Key Required";

					var apiInstance = new AuthenticationApi(Config);
					var twoFactorEmailCode = new TwoFactorEmailCode(this.AuthBox.Text); // TwoFactorEmailCode |  (optional) 

					try
					{
						// Verify 2FA code
						var cookie = new Cookie();
						
						Verify2FAEmailCodeResult result = apiInstance.Verify2FAEmailCode(twoFactorEmailCode);
						Debug.WriteLine(result);
					}
					catch (ApiException er)
					{
						Debug.WriteLine("Exception when calling AuthenticationApi.Verify2FA: " + er.Message);
						
						Debug.WriteLine(er.StackTrace);
					}
				} 
			}
			catch (ApiException er)
			{
				this.Status.Text = "failed to login";
				Debug.WriteLine("Exception when calling API: {0}", er.Message);
				Debug.WriteLine("Status Code: {0}", er.ErrorCode);
				Debug.WriteLine(er.ToString());
				userID = string.Empty;
			}
		}

		#region private helpers
		private string GetPasswordText(PasswordBox passwordBox)
		{
			IntPtr passwordPtr = System.Runtime.InteropServices.Marshal.SecureStringToBSTR(passwordBox.SecurePassword);
			try
			{
				return System.Runtime.InteropServices.Marshal.PtrToStringBSTR(passwordPtr);
			}
			finally
			{
				System.Runtime.InteropServices.Marshal.ZeroFreeBSTR(passwordPtr);
			}
		}
		#endregion
	}
}
