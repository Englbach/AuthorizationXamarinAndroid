using Android.App;
using Android.Widget;
using Android.OS;
using Xamarin.Auth;
using System;


namespace AuthorizationXamarinAndroid
{
	[Activity (Label = "Authorization Xamarin Android", MainLauncher = true, Icon = "@mipmap/icon")]
	public class MainActivity : Activity
	{
		OAuth2Authenticator auth;
		Button FaceBook;
		Button Google;
		Button Soundcloud;
		protected override void OnCreate (Bundle savedInstanceState)
		{
			base.OnCreate (savedInstanceState);

			// Set our view from the "main" layout resource
			SetContentView (Resource.Layout.Main);

			// Get our button from the layout resource,
			// and attach an event to it
			FaceBook=FindViewById<Button>(Resource.Id.btnLoginFacebook);
			FaceBook.Click+= (object sender, System.EventArgs e) => 
			{
				Oauth2(0);
			};

		}
	
		private void OauthParameter(string client_id, string client_secret, string auUrl,string reUrl, string acUrl)
		{
			auth=new OAuth2Authenticator(
				clientId:client_id,
				clientSecret:client_secret,
				scope:"",
				authorizeUrl:new Uri(auUrl),
				redirectUrl:new Uri(reUrl),
				accessTokenUrl:new Uri(acUrl),
				getUsernameAsync: null
			);
		}
		private void Oauth2(int id)
		{
			
			if (id == 0) {
				OauthParameter (
					"1037908022964402",
					"1007c99adf1cb1ea95d9c65ad111053a",
					"https://www.facebook.com/dialog/oauth",
					"https://localhost:3000/auth/facebook/callback", 
					"https://graph.facebook.com/oauth/access_token"
					);
			} else if (id == 1) {

				OauthParameter (
					"1037908022964402",
					"1007c99adf1cb1ea95d9c65ad111053a",
					"https://www.facebook.com/dialog/oauth",
					"https://localhost:3000/auth/facebook/callback", 
					"https://graph.facebook.com/oauth/access_token"
				);


			} else {
				
			}

			
			StartActivity(auth.GetUI(this));

			auth.Completed+= (object sender, AuthenticatorCompletedEventArgs e)=> 
			{
				if(e.IsAuthenticated)
				{
					AlertDialog.Builder builder = new AlertDialog.Builder(this);
					builder.SetTitle("Login successful...");
					builder.SetMessage("Access_Token: "+ e.Account.Properties["access_token"]);
					builder.SetNegativeButton("Cancel", delegate {
						Toast.MakeText(this,"Cancel",ToastLength.Long).Show();	
					});
					Dialog dialog= builder.Create();
					dialog.Show();
				}
				else
				{
					Toast.MakeText(this,"Fail...",ToastLength.Long).Show();
				}
			};
		}
	}
}


