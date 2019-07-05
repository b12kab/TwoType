using Xamarin.Forms;
using System.Collections.Generic;
using System.Linq;
using System;
using SQLite;
using TwoTypeExample.Models;

namespace TwoTypeExample.Helpers
{
    public class DatabaseHelper
    {
        public const string DbFileName = "Contacts.db";

        public DatabaseHelper(SQLiteConnection connection)
        {
            connection.CreateTable<ContactInfo>();
            connection.CreateTable<MessageInfo>();
        }

        // Get All Contact data
        public List<ContactInfo> GetAllContactsData(SQLiteConnection connection)
        {
            return (from data in connection.Table<ContactInfo>()
                    select data).ToList();
        }

        // Get all Contact key values
        public IList<int> GetAllContactKeys(SQLiteConnection connection)
        {
            return (from data in connection.Table<ContactInfo>()
                    select data.Id).ToList();
        }

        //Get Specific Contact data  
        public ContactInfo GetContactData(SQLiteConnection connection, int id)
        {
            ContactInfo info = connection.Table<ContactInfo>().FirstOrDefault(t => t.Id == id);
            return info;
        }

        // Delete Specific Contact  
        public bool DeleteContact(SQLiteConnection connection, int id)
        {
            try
            {
                connection.RunInTransaction(() =>
                {
                    connection.Delete<ContactInfo>(id);
                });
                connection.Commit();
            }
            catch (SQLiteException ex)
            {
                connection.Rollback();
                System.Diagnostics.Debug.WriteLine("DeleteContact(" + id + ") failed - " +
                                                   ex.Message);
                return true;
            }

            return false;
        }

        public bool DeleteAllContacts(SQLiteConnection connection)
        {
            int count = 0;
            try
            {
                connection.RunInTransaction(() =>
                {
                    count = connection.DeleteAll<ContactInfo>();
                });
                connection.Commit();
            }
            catch (SQLiteException ex)
            {
                connection.Rollback();
                System.Diagnostics.Debug.WriteLine("DeleteAllContacts failed - " +
                                                   ex.Message);
                return true;
            }

            return false;
        }

        // Insert new Contact to DB   
        public bool InsertContact(SQLiteConnection connection, ContactInfo contact)
        {
            try
            {
                connection.RunInTransaction(() =>
                {
                    connection.Insert(contact);
                });
                connection.Commit();
            }
            catch (SQLiteException ex)
            {
                connection.Rollback();
                System.Diagnostics.Debug.WriteLine("InsertContact(" + contact.Id + ") failed - " +
                                                   ex.Message);
                return true;
            }

            return false;
        }

        // Update Contact Data  
        public bool UpdateContact(SQLiteConnection connection, ContactInfo contact)
        {
            try
            {
                connection.RunInTransaction(() =>
                {
                    connection.Update(contact);
                });
                connection.Commit();
            }
            catch (SQLiteException ex)
            {
                connection.Rollback();
                System.Diagnostics.Debug.WriteLine("UpdateContact(" + contact.Id + ") failed - " +
                                                   ex.Message);
                return true;
            }

            return false;
        }

        //---------------------------------------------

        // Get All MessageInfo data      
        public List<MessageInfo> GetAllMessages(SQLiteConnection connection)
        {
            return (from data in connection.Table<MessageInfo>()
                    select data).ToList();
        }

        //Get Specific Contact data  
        public MessageInfo GetMessage(SQLiteConnection connection, int id)
        {
            return connection.Table<MessageInfo>().FirstOrDefault(t => t.Id == id);
        }

        // Delete Specific Message  
        public bool DeleteMessage(SQLiteConnection connection, int id)
        {
            try
            {
                connection.RunInTransaction(() =>
                {
                    connection.Delete<MessageInfo>(id);
                });
                connection.Commit();
            }
            catch (SQLiteException ex)
            {
                connection.Rollback();
                System.Diagnostics.Debug.WriteLine("DeleteMessage(" + id + ") failed - " +
                                                   ex.Message);
                return true;
            }

            return false;
        }

        // Insert new MessageInfo
        public bool InsertMessage(SQLiteConnection connection, MessageInfo messageInfo)
        {
            try
            {
                connection.RunInTransaction(() =>
                {
                    messageInfo.MessageCreated = DateTime.UtcNow;
                    connection.Insert(messageInfo);
                });
                connection.Commit();
            }
            catch (SQLiteException ex)
            {
                connection.Rollback();
                System.Diagnostics.Debug.WriteLine("InsertMessage(" + messageInfo.Id + ") failed - " +
                                                   ex.Message);
                return true;
            }

            return false;
        }

        // Update MessageInfo
        public bool UpdateMessage(SQLiteConnection connection, MessageInfo messageInfo)
        {
            try
            {
                connection.RunInTransaction(() =>
                {
                    connection.Update(messageInfo);
                });
                connection.Commit();
            }
            catch (SQLiteException ex)
            {
                connection.Rollback();
                System.Diagnostics.Debug.WriteLine("UpdateMessage(" + messageInfo.Id + ") failed - " +
                                                   ex.Message);
                return true;
            }

            return false;
        }

        //---------------------------------------------

        public IList<MessageInfoWithContact> GetAllMessagesWithContacts(SQLiteConnection connection)
        {
            var list = (from msg in connection.Table<MessageInfo>()
                        join cont in connection.Table<ContactInfo>()
                                    on msg.FromContactId equals cont.Id
                        orderby msg.MessageCreated
                        select new MessageInfoWithContact
                        {
                            MessageId = msg.Id,
                            ContactId = msg.FromContactId,
                            MessageText = msg.Message,
                            MessageCreated = msg.MessageCreated,
                            ContactName = cont.Name
                        }
                        ).ToList();
            return list;
        }

        public MessageInfoWithContact GetMessageWithContacts(SQLiteConnection connection, int messageId)
        {
            var list = (from msg in connection.Table<MessageInfo>()
                        join cont in connection.Table<ContactInfo>()
                                    on msg.FromContactId equals cont.Id
                        where msg.Id == messageId
                        orderby msg.MessageCreated
                        select new MessageInfoWithContact
                        {
                            MessageId = msg.Id,
                            ContactId = msg.FromContactId,
                            MessageText = msg.Message,
                            MessageCreated = msg.MessageCreated,
                            ContactName = cont.Name
                        }
                        ).FirstOrDefault();
            return list;
        }

    }
}
