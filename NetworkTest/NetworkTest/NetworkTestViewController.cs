using System;
using System.Drawing;
using SQLite;
using Foundation;
using UIKit;
using System.Threading.Tasks;
using System.Linq;
using System.Collections.Generic;
using System.IO;
using System.Net.Http;
using ModernHttpClient;

namespace NetworkTest
{
	public partial class NetworkTestViewController : UIViewController
	{


		public NetworkTestViewController (IntPtr handle) : base (handle)
		{
		}

		public override void DidReceiveMemoryWarning ()
		{
			// Releases the view if it doesn't have a superview.
			base.DidReceiveMemoryWarning ();
			
			// Release any cached data, images, etc that aren't in use.
		}

		#region View lifecycle

		public override void ViewDidLoad ()
		{
			base.ViewDidLoad ();
			
			// Perform any additional setup after loading the view, typically from a nib.
			string uri = Settings.GetURI ();
			if (uri != null)
				URLTextBox.Text = uri;

			ResultsTextView.Editable = false;
		

		}

		public override void ViewWillAppear (bool animated)
		{
			base.ViewWillAppear (animated);
		}

		public override void ViewDidAppear (bool animated)
		{
			base.ViewDidAppear (animated);
		}

		public override void ViewWillDisappear (bool animated)
		{
			base.ViewWillDisappear (animated);
		}

		public override void ViewDidDisappear (bool animated)
		{
			base.ViewDidDisappear (animated);
		}
			
		partial void btnHttpClient_TouchUpInside (UIButton sender)
		{
			Settings.StoreURI(URLTextBox.Text);

			ResultsTextView.Text = RetrieveURL(URLTextBox.Text, false);
		}

		partial void btnModernHttpClient_TouchUpInside (UIButton sender)
		{
			Settings.StoreURI(URLTextBox.Text);

			ResultsTextView.Text = RetrieveURL(URLTextBox.Text, true);
		}	

		public string RetrieveURL(string url, bool useModernHttpClient)
		{
			
			HttpClient httpClient;
			if (useModernHttpClient)
				httpClient = new HttpClient(new NativeMessageHandler());
			else
				httpClient = new HttpClient();


			Task<HttpResponseMessage> getResponse = httpClient.GetAsync(url);

			HttpResponseMessage msg = getResponse.Result;

			Task<string> finalMsg = msg.Content.ReadAsStringAsync();

			string headers = null;
			foreach (var s in msg.Headers) {
				string values = null;
				foreach (var p in s.Value) {
					values += p + " ";
				}
				headers += s.Key + ":" + values + "\n";
			}
			return headers + "HTTP (" + msg.StatusCode.ToString() + ")\n" + finalMsg.Result;

		}


		#endregion


	}
}

