using System;
using System.Collections.Generic;
using TwoTypeExample.ViewModel;
using Xamarin.Forms;

namespace TwoTypeExample.Views
{
    public partial class MessageList : ContentPage
    {
        public MessageList()
        {
            InitializeComponent();
        }

        ~MessageList()
        {
            DateTime date = DateTime.Now;
            var test = date.ToString("yyyy-MM-dd H:mm:ss.fffffffzzz");
            System.Diagnostics.Debug.WriteLine("MessageList destructor - " +
                test);
            System.Diagnostics.Debug.Flush();
        }

        protected override void OnAppearing()
        {
            DateTime date = DateTime.Now;
            var test = date.ToString("yyyy-MM-dd H:mm:ss.fffffffzzz");
            System.Diagnostics.Debug.WriteLine("==================================");
            System.Diagnostics.Debug.WriteLine("MessageList OnAppearing -1- " +
                test);
            System.Diagnostics.Debug.Flush();

            base.OnAppearing();

            date = DateTime.Now;
            test = date.ToString("yyyy-MM-dd H:mm:ss.fffffffzzz");
            System.Diagnostics.Debug.WriteLine("MessageList OnAppearing -2- after base.OnAppearing - " +
                test);
            System.Diagnostics.Debug.Flush();

            App.GarbCollect();

            date = DateTime.Now;
            test = date.ToString("yyyy-MM-dd H:mm:ss.fffffffzzz");
            System.Diagnostics.Debug.WriteLine("MessageList OnAppearing -3- post Garbage Collect - " + test);
            System.Diagnostics.Debug.Flush();

            this.BindingContext = new MessageListViewModel(Navigation);

            date = DateTime.Now;
            test = date.ToString("yyyy-MM-dd H:mm:ss.fffffffzzz");
            System.Diagnostics.Debug.WriteLine("MessageList OnAppearing -4- new MessageListViewModel - " + test);
            System.Diagnostics.Debug.WriteLine("==================================");
            System.Diagnostics.Debug.Flush();
        }

        protected override void OnDisappearing()
        {
            DateTime date = DateTime.Now;
            var test = date.ToString("yyyy-MM-dd H:mm:ss.fffffffzzz");
            System.Diagnostics.Debug.WriteLine("==================================");
            System.Diagnostics.Debug.WriteLine("MessageList OnDisappearing -1- start - " +
                test);
            System.Diagnostics.Debug.Flush();

            base.OnDisappearing();

            date = DateTime.Now;
            test = date.ToString("yyyy-MM-dd H:mm:ss.fffffffzzz");
            System.Diagnostics.Debug.WriteLine("MessageList OnDisappearing -2- after base.OnDisappearing - " + test);
            System.Diagnostics.Debug.Flush();

            this.BindingContext = null;

            date = DateTime.Now;
            test = date.ToString("yyyy-MM-dd H:mm:ss.fffffffzzz");
            System.Diagnostics.Debug.WriteLine("MessageList OnDisappearing -3- BindingContext set to null - " + test);
            System.Diagnostics.Debug.Flush();

            App.GarbCollect();

            date = DateTime.Now;
            test = date.ToString("yyyy-MM-dd H:mm:ss.fffffffzzz");
            System.Diagnostics.Debug.WriteLine("MessageList OnDisappearing -4- post Garbage Collect - " + test);
            System.Diagnostics.Debug.WriteLine("==================================");
            System.Diagnostics.Debug.Flush();
        }
    }
}
