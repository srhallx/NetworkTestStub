// WARNING
//
// This file has been generated automatically by Xamarin Studio from the outlets and
// actions declared in your storyboard file.
// Manual changes to this file will not be maintained.
//
using Foundation;
using System;
using System.CodeDom.Compiler;
using UIKit;

namespace NetworkTest
{
	[Register ("NetworkTestViewController")]
	partial class NetworkTestViewController
	{
		[Outlet]
		[GeneratedCode ("iOS Designer", "1.0")]
		UIButton btnHttpClient { get; set; }

		[Outlet]
		[GeneratedCode ("iOS Designer", "1.0")]
		UIButton btnModernHttpClient { get; set; }

		[Outlet]
		[GeneratedCode ("iOS Designer", "1.0")]
		UIButton btnNSURLConnection { get; set; }

		[Outlet]
		[GeneratedCode ("iOS Designer", "1.0")]
		UITextView ResultsTextView { get; set; }

		[Outlet]
		[GeneratedCode ("iOS Designer", "1.0")]
		UITextField URLTextBox { get; set; }

		[Action ("btnHttpClient_TouchUpInside:")]
		[GeneratedCode ("iOS Designer", "1.0")]
		partial void btnHttpClient_TouchUpInside (UIButton sender);

		[Action ("btnModernHttpClient_TouchUpInside:")]
		[GeneratedCode ("iOS Designer", "1.0")]
		partial void btnModernHttpClient_TouchUpInside (UIButton sender);

		[Action ("btnNSURLConnection_TouchUpInside:")]
		[GeneratedCode ("iOS Designer", "1.0")]
		partial void btnNSURLConnection_TouchUpInside (UIButton sender);

		void ReleaseDesignerOutlets ()
		{
			if (btnHttpClient != null) {
				btnHttpClient.Dispose ();
				btnHttpClient = null;
			}
			if (btnModernHttpClient != null) {
				btnModernHttpClient.Dispose ();
				btnModernHttpClient = null;
			}
			if (btnNSURLConnection != null) {
				btnNSURLConnection.Dispose ();
				btnNSURLConnection = null;
			}
			if (ResultsTextView != null) {
				ResultsTextView.Dispose ();
				ResultsTextView = null;
			}
			if (URLTextBox != null) {
				URLTextBox.Dispose ();
				URLTextBox = null;
			}
		}
	}
}
