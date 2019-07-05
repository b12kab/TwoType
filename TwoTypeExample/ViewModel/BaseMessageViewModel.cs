using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using TwoTypeExample.Models;
using TwoTypeExample.Services;
using TwoTypeExample.Validator;
using Xamarin.Forms;

namespace TwoTypeExample.ViewModel
{
    public class BaseMessageViewModel : INotifyPropertyChanged
    {
        public MessageInfo _message;
        public INavigation _navigation;
        public MessageValidator _messageValidator;
        public IRepository _repository;

        private IList<MessageInfoWithContact> _messageAndContactList;
        public IList<MessageInfoWithContact> MessageAndContactList
        {
            get => _messageAndContactList;
            set
            {
                _messageAndContactList = value;
                NotifyPropertyChanged("MessageAndContactList");
            }
        }

        private string _messageText;
        public string MessageText
        {
            get => _messageText;
            set
            {
                _messageText = value;
                if (_message != null)
                {
                    _message.Message = value;
                }
                NotifyPropertyChanged("MessageText");
            }
        }

        private ContactInfo _selectedContactItem;
        public ContactInfo SelectedContact
        {
            get => _selectedContactItem;
            set
            {
                if (value != null)
                {
                    _selectedContactItem = value;
                    if (_message == null)
                    {
                        _message = new MessageInfo();
                    }
                    _message.FromContactId = value.Id;
                    NotifyPropertyChanged("SelectedContactInfo");
                }
            }
        }

        private List<ContactInfo> _contacts;
        public List<ContactInfo> ContactList
        {
            get => _contacts;
            set
            {
                if (value != null)
                {
                    _contacts = value;
                    NotifyPropertyChanged("ContactList");
                }
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;
        protected void NotifyPropertyChanged([CallerMemberName] string propertyName = "")
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
