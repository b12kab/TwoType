﻿using System;
using System.Collections.Generic;
using TwoTypeExample.ViewModel;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace TwoTypeExample.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class DetailsContactPage : ContentPage
    {
        public DetailsContactPage(int id)
        {
            InitializeComponent();
            this.BindingContext = new DetailsContactViewModel(Navigation, id);
        }

        ~DetailsContactPage()
        {
            DateTime date = DateTime.Now;
            var test = date.ToString("yyyy-MM-dd H:mm:ss.fffffffzzz");
            System.Diagnostics.Debug.WriteLine("DetailsContactPage destructor - " +
                test);
            System.Diagnostics.Debug.Flush();
        }

        protected override void OnAppearing()
        {
            DateTime date = DateTime.Now;
            var test = date.ToString("yyyy-MM-dd H:mm:ss.fffffffzzz");
            System.Diagnostics.Debug.WriteLine("==================================");
            System.Diagnostics.Debug.WriteLine("DetailsContactPage OnAppearing -1- " +
                test);
            System.Diagnostics.Debug.Flush();

            base.OnAppearing();

            date = DateTime.Now;
            test = date.ToString("yyyy-MM-dd H:mm:ss.fffffffzzz");
            System.Diagnostics.Debug.WriteLine("DetailsContactPage OnAppearing -2- after base.OnAppearing - " +
                test);
            System.Diagnostics.Debug.Flush();

            App.GarbCollect();

            date = DateTime.Now;
            test = date.ToString("yyyy-MM-dd H:mm:ss.fffffffzzz");
            System.Diagnostics.Debug.WriteLine("DetailsContactPage OnAppearing -3- post Garbage Collect - " + test);
            System.Diagnostics.Debug.WriteLine("==================================");
            System.Diagnostics.Debug.Flush();
        }

        protected override void OnDisappearing()
        {
            DateTime date = DateTime.Now;
            var test = date.ToString("yyyy-MM-dd H:mm:ss.fffffffzzz");
            System.Diagnostics.Debug.WriteLine("==================================");
            System.Diagnostics.Debug.WriteLine("DetailsContactPage OnDisappearing -1- start - " +
                test);
            System.Diagnostics.Debug.Flush();

            base.OnDisappearing();

            date = DateTime.Now;
            test = date.ToString("yyyy-MM-dd H:mm:ss.fffffffzzz");
            System.Diagnostics.Debug.WriteLine("DetailsContactPage OnDisappearing -2- after base.OnDisappearing - " + test);
            System.Diagnostics.Debug.Flush();

            App.GarbCollect();

            date = DateTime.Now;
            test = date.ToString("yyyy-MM-dd H:mm:ss.fffffffzzz");
            System.Diagnostics.Debug.WriteLine("DetailsContactPage OnDisappearing -3- post Garbage Collect - " + test);
            System.Diagnostics.Debug.WriteLine("==================================");
            System.Diagnostics.Debug.Flush();
        }
    }
}
