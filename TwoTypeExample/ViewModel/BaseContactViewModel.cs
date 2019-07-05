using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using FluentValidation;
using TwoTypeExample.Models;
using TwoTypeExample.Services;
using Xamarin.Forms;

namespace TwoTypeExample.ViewModel
{
    public class BaseContactViewModel : INotifyPropertyChanged
    {
        public ContactInfo _contact;

        public INavigation _navigation;
        public IValidator _contactValidator;
        public IRepository _repository;

        public string Name
        {
            get => _contact.Name;
            set
            {
                _contact.Name = value;
                NotifyPropertyChanged("Name");
            }
        }

        public string MobileNumber
        {
            get => _contact.MobileNumber;
            set
            {
                _contact.MobileNumber = value;
                NotifyPropertyChanged("MobileNumber");
            }
        }

        public string Age
        {
            get => _contact.Age;
            set
            {
                _contact.Age = value;
                NotifyPropertyChanged("Age");
            }
        }

        public string Gender
        {
            get => _contact.Gender;
            set
            {
                _contact.Gender = value;
                NotifyPropertyChanged("Gender");
            }
        }

        public DateTime DOB
        {
            get => _contact.DOB;
            set
            {
                _contact.DOB = value;
                NotifyPropertyChanged("DOB");
            }
        }

        public string Address
        {
            get => _contact.Address;
            set
            {
                _contact.Address = value;
                NotifyPropertyChanged("Address");
            }
        }

        List<ContactInfo> _contactList;
        public List<ContactInfo> ContactList
        {
            get => _contactList;
            set
            {
                _contactList = value;
                NotifyPropertyChanged("ContactList");
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;
        protected void NotifyPropertyChanged([CallerMemberName] string propertyName = "")
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
