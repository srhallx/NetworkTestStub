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
			

		//
		// Button press event handlers
		//
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

		partial void btnNSURLConnection_TouchUpInside (UIButton sender)
		{
			Settings.StoreURI(URLTextBox.Text);

			NSMutableUrlRequest request = new NSMutableUrlRequest(new NSUrl(URLTextBox.Text));
			request.HttpMethod = "GET";
			request.ShouldHandleCookies = false;

			//Add Headers
			NSMutableDictionary header = new NSMutableDictionary();
			header.Add(NSObject.FromObject("content-type"), NSObject.FromObject("text/xml"));

			request.Headers = header;

			URLConnDelegate connectionDelegate = new URLConnDelegate ();
			NSUrlConnection connection = new NSUrlConnection (request, connectionDelegate, true);

			connectionDelegate.Finished += (object sdr, EventArgs e) => {
				ResultsTextView.Text = connectionDelegate.Response;
			};

			connection.Start();

		}
			


		/// <summary>
		/// Retrieves the URL
		/// </summary>
		/// <returns>String of response data and headers.</returns>
		/// <param name="url">URL</param>
		/// <param name="useModernHttpClient">If set to <c>true</c> use modern http client.</param>
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

	public class URLConnDelegate : NSUrlConnectionDataDelegate
	{

		public string Response { get; set; }
		public event EventHandler Finished;

		public override void  ReceivedResponse (NSUrlConnection connection, NSUrlResponse response)
		{
			NSHttpUrlResponse httpResponse = response as NSHttpUrlResponse;

			string data = null;

			foreach (var s in httpResponse.AllHeaderFields) {
				data += s.Key + ":" + s.Value + "\n";
			}

			data += "HTTP (" + httpResponse.StatusCode.ToString () + ")\n";
			Response = data;

		}

		public override void ReceivedData (NSUrlConnection connection, NSData data)
		{
			Response += data.ToString ();
		}
			
		public override void FinishedLoading (NSUrlConnection connection)
		{
			if (Finished != null)
				Finished (this, new EventArgs ());
		}
	}
}

