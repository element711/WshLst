using System;
using System.Collections.Generic;
using System.Linq;

using MonoTouch.Foundation;
using MonoTouch.UIKit;

using Cirrious.MvvmCross.ExtensionMethods;
using Cirrious.MvvmCross.Interfaces.ServiceProvider;
using Cirrious.MvvmCross.Interfaces.ViewModels;
using Cirrious.MvvmCross.Touch.Platform;
using Cirrious.MvvmCross.Touch.Views.Presenters;

namespace WshLst.MonoTouch
{
	// The UIApplicationDelegate for the application. This class is responsible for launching the 
	// User Interface of the application, as well as listening (and optionally responding) to 
	// application events from iOS.
	[Register ("AppDelegate")]
	public partial class AppDelegate : MvxApplicationDelegate, IMvxServiceConsumer
	{
		// class-level declarations
		UIWindow window;

		//
		// This method is invoked when the application has loaded and is ready to run. In this 
		// method you should instantiate the window, load the UI into it and then make the window
		// visible.
		//
		// You have 17 seconds to return from this method, or iOS will terminate your application.
		//
		public override bool FinishedLaunching (UIApplication app, NSDictionary options)
		{
			// create a new window instance based on the screen size
			window = new UIWindow (UIScreen.MainScreen.Bounds);

			// initialize app for single screen iPhone display
			var presenter = new MvxTouchViewPresenter(this, window);
			var s = new Setup (this, presenter);


			//var setup = new Setup(this, presenter);
			s.Initialize();

			// start the app
			var start = this.GetService<IMvxStartNavigation>();
			start.Start();

            var geo = this.GetService<WshLst.Core.Interfaces.IGeolocator>();
            geo.StartTracking();
		
			// make the window visible
			window.MakeKeyAndVisible ();
			
			return true;
		}

        public override void WillTerminate(UIApplication application)
        {
            var geo = this.GetService<WshLst.Core.Interfaces.IGeolocator>();
            geo.StopTracking();

            base.WillTerminate(application);
        }
	}
}
