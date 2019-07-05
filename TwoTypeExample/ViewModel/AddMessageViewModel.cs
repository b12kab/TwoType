using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Windows.Input;
using TwoTypeExample.Models;
using TwoTypeExample.Services;
using TwoTypeExample.Validator;
using Xamarin.Forms;
namespace TwoTypeExample.ViewModel
{
    public class AddMessageViewModel : BaseMessageViewModel
    {
        public ICommand AddMessageCommand { get; private set; }

        public AddMessageViewModel(INavigation navigation)
        {
            System.Diagnostics.Debug.WriteLine("==================================");
            System.Diagnostics.Debug.Flush();
            DateTime date = DateTime.Now;
            var test = date.ToString("yyyy-MM-dd H:mm:ss.fffffffzzz");
            System.Diagnostics.Debug.WriteLine("AddMessageViewModel() constructor -1- start - timestamp: " + test);
            System.Diagnostics.Debug.Flush();

            _navigation = navigation;
            _message = new MessageInfo();

            GetDBConnection dbConnection = new GetDBConnection();

            if (dbConnection == null || dbConnection.Connection == null)
            {
                string errMsg = dbConnection.ConnException.Message;
                App.DatabaseError("AddMessageViewModel() constructor", errMsg);
                return;
            }
            else
            {
                string textPointer = "pointer is null";
                if (dbConnection.Connection != null)
                {
                    if (dbConnection.Connection.Handle == null)
                    {
                        textPointer = "handle is null";
                    }
                    else
                    {
                        textPointer = dbConnection.Connection.Handle.ptr.ToString();
                    }
                }

                System.Diagnostics.Debug.WriteLine("AddMessageViewModel() constructor -2- sqlite connection: " + textPointer);
            }

            _repository = new Repository(dbConnection.Connection);

            ContactList = _repository.GetAllContactsData();

            IList<int> foreignKeys = _repository.GetAllContactKeys();
            _messageValidator = new MessageValidator(foreignKeys);

            AddMessageCommand = new Command(async () => await AddMessage());

            test = date.ToString("yyyy-MM-dd H:mm:ss.fffffffzzz");
            System.Diagnostics.Debug.WriteLine("AddMessageViewModel() constructor -3- end - timestamp: " + test);
            System.Diagnostics.Debug.WriteLine("==================================");
            System.Diagnostics.Debug.Flush();
        }

        ~AddMessageViewModel()
        {
            System.Diagnostics.Debug.WriteLine("==================================");
            System.Diagnostics.Debug.Flush();
            DateTime date = DateTime.Now;
            var test = date.ToString("yyyy-MM-dd H:mm:ss.fffffffzzz");
            System.Diagnostics.Debug.WriteLine("AddMessageViewModel destructor -1- timestamp: " + test);
            System.Diagnostics.Debug.Flush();

            App.GarbCollect();

            date = DateTime.Now;
            test = date.ToString("yyyy-MM-dd H:mm:ss.fffffffzzz");
            System.Diagnostics.Debug.WriteLine("AddMessageViewModel destructor -2- post Garb Collect - " + test);
            System.Diagnostics.Debug.WriteLine("==================================");
            System.Diagnostics.Debug.Flush();
        }

        async Task AddMessage()
        {
            var validationResults = _messageValidator.Validate(_message);

            if (validationResults.IsValid)
            {
                bool isUserAccept = await Application.Current.MainPage.DisplayAlert("Add Message", "Do you want to save the Message?", "OK", "Cancel");
                if (isUserAccept)
                {
                    _repository.InsertMessage(_message);
                    await _navigation.PopAsync();
                }
            }
            else
            {
                await Application.Current.MainPage.DisplayAlert("Add Message", validationResults.Errors[0].ErrorMessage, "Ok");
            }
        }
    }
}
