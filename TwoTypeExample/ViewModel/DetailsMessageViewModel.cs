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
    public class DetailsMessageViewModel : BaseMessageViewModel
    {
        public ICommand UpdateMessageCommand { get; private set; }
        public ICommand DeleteMessageCommand { get; private set; }

        public DetailsMessageViewModel(INavigation navigation, int selectedID)
        {
            System.Diagnostics.Debug.WriteLine("==================================");
            System.Diagnostics.Debug.Flush();
            DateTime date = DateTime.Now;
            var test = date.ToString("yyyy-MM-dd H:mm:ss.fffffffzzz");
            System.Diagnostics.Debug.WriteLine("DetailsMessageViewModel() constructor -1- start - timestamp: " + test);
            System.Diagnostics.Debug.Flush();

            _navigation = navigation;

            GetDBConnection dbConnection = new GetDBConnection();

            if (dbConnection == null || dbConnection.Connection == null)
            {
                string errMsg = dbConnection.ConnException.Message;
                App.DatabaseError("DetailsMessageViewModel() constructor", errMsg);
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

                System.Diagnostics.Debug.WriteLine("DetailsMessageViewModel() constructor -2- sqlite connection: " + textPointer);
            }

            _repository = new Repository(dbConnection.Connection);

            UpdateMessageCommand = new Command(async () => await UpdateMessage());
            DeleteMessageCommand = new Command(async () => await DeleteMessage());

            FetchDetails(selectedID);

            test = date.ToString("yyyy-MM-dd H:mm:ss.fffffffzzz");
            System.Diagnostics.Debug.WriteLine("DetailsMessageViewModel() constructor -3- end - timestamp: " + test);
            System.Diagnostics.Debug.WriteLine("==================================");
            System.Diagnostics.Debug.Flush();
        }

        ~DetailsMessageViewModel()
        {
            System.Diagnostics.Debug.WriteLine("==================================");
            System.Diagnostics.Debug.Flush();
            DateTime date = DateTime.Now;
            var test = date.ToString("yyyy-MM-dd H:mm:ss.fffffffzzz");
            System.Diagnostics.Debug.WriteLine("DetailsMessageViewModel destructor -1- timestamp: " + test);
            System.Diagnostics.Debug.Flush();

            App.GarbCollect();

            date = DateTime.Now;
            test = date.ToString("yyyy-MM-dd H:mm:ss.fffffffzzz");
            System.Diagnostics.Debug.WriteLine("DetailsMessageViewModel destructor -2- post Garb Collect - " + test);
            System.Diagnostics.Debug.WriteLine("==================================");
            System.Diagnostics.Debug.Flush();
        }

        void FetchDetails(int selectedID)
        {
            _message = _repository.GetMessage(selectedID);
            MessageText = _message.Message;

            ContactList = _repository.GetAllContactsData();

            List<int> list = new List<int>();
            foreach (ContactInfo contact in ContactList)
            {
                if (contact.Id > 0)
                {
                    if (contact.Id == _message.FromContactId)
                    {
                        SelectedContact = contact;
                    }
                    list.Add(contact.Id);
                }
            }
            _messageValidator = new MessageValidator(list);
        }

        async Task UpdateMessage()
        {
            var validationResults = _messageValidator.Validate(_message);

            if (validationResults.IsValid)
            {
                bool isUserAccept = await Application.Current.MainPage.DisplayAlert("Message Details", "Update Message Details", "OK", "Cancel");
                if (isUserAccept)
                {
                    _message.Message = MessageText;
                    _repository.UpdateMessage(_message);
                    await _navigation.PopAsync();
                }
            }
            else
            {
                await Application.Current.MainPage.DisplayAlert("Message Details", validationResults.Errors[0].ErrorMessage, "Ok");
            }
        }

        async Task DeleteMessage()
        {
            bool isUserAccept = await Application.Current.MainPage.DisplayAlert("Message Details", "Delete Message", "OK", "Cancel");
            if (isUserAccept)
            {
                _repository.DeleteMessage(_message.Id);
                await _navigation.PopAsync();
            }
        }
    }
}
