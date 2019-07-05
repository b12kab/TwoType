using System;
using System.Collections.Generic;
using TwoTypeExample.Models;

namespace TwoTypeExample.Services
{
    public interface IRepository
    {
        List<ContactInfo> GetAllContactsData();

        IList<int> GetAllContactKeys();

        //Get Specific Contact data  
        ContactInfo GetContactData(int contactID);

        // Delete Specific Contact  
        bool DeleteContact(int contactID);

        // Insert new Contact to DB   
        bool InsertContact(ContactInfo contact);

        // Update Contact Data  
        bool UpdateContact(ContactInfo contact);

        bool DeleteAllContacts();

        //---------------------------------------------

        IList<MessageInfo> GetAllMessages();

        MessageInfo GetMessage(int messageID);

        bool DeleteMessage(int messageID);

        bool InsertMessage(MessageInfo messageInfo);

        bool UpdateMessage(MessageInfo messageInfo);

        //---------------------------------------------

        IList<MessageInfoWithContact> GetAllMessagesWithContacts();

        MessageInfoWithContact GetMessageWithContacts(int messageID);
    }
}
