﻿using System;

using Catalog.Views;
using MvvmCross.Forms.Platform;
using Xamarin.Forms;

namespace Catalog
{
	public partial class App : MvxFormsApplication
    {
		public App ()
		{
			InitializeComponent();
        }

		protected override void OnStart ()
		{
			// Handle when your app starts
		}

		protected override void OnSleep ()
		{
			// Handle when your app sleeps
		}

		protected override void OnResume ()
		{
			// Handle when your app resumes
		}
	}
}
