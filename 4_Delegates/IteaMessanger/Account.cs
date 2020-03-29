using System;
using System.Collections.Generic;
using System.Linq;

using static ITEA_Collections.Common.Extensions;

namespace IteaDelegates.IteaMessanger
{
    public delegate void OnMessage(Message message);
    public delegate void OnSend(object sender, OnSendEventArgs e);

    public class Account
    {
        public string Username { get; private set; }

        public List<Message> Messages { get; set; }
       // public List<Account> GroupName { get; private set; }

        public event OnSend OnSend;
        public Account User { get; set; }

        public OnMessage NewMessage { get; set; }

        public Account(string username)
        {
            Username = username;
            Messages = new List<Message>();
           // GroupName = new List<Account>();
            NewMessage += OnNewMessage;
        }

        public Message CreateMessage(string text, Account to)
        {
            var message = new Message(this, to, text);
            Messages.Add(message);
            return message;
        }

        public void Send(Message message)
        {
            message.Send = true;
            message.To.Messages.Add(message);
            message.To.NewMessage(message);
            OnSend?.Invoke(this, new OnSendEventArgs(message.ReadMessage(this), message.From.Username, message.To.Username));
        }

        public void OnNewMessage(Message message)
        {
            if (message.Send)
                ToConsole($"OnNewMessage: {message.From.Username}: {message.Preview}", ConsoleColor.DarkYellow);
        }

        public void ShowDialog(string username)
        {
            List<Message> messageDialog = Messages
                .Where(x => x.To.Username.Equals(username) || x.From.Username.Equals(username))
                .Where(x => x.Send)
                .OrderBy(x => x.Created)
                .ToList();
            string str = $"Dialog with {username}";
            ToConsole($"---{str}---");
            foreach (Message message in messageDialog)
            {
                ToConsole($"{(message.From.Username.Equals(username) ? username : Username)}: {message.ReadMessage(this)}",
                    message.From.Username.Equals(username) ? ConsoleColor.Cyan : ConsoleColor.DarkYellow);
            }
            ToConsole($"---{string.Concat(str.Select(x => "-"))}---");
        }
        public void Subscribe(Account user)
        {     
            User = user;
            User.OnSend += SendMessageToGroup;
        }


        public void SendMessageToGroup(object sender, OnSendEventArgs e)
        {
            var myGroup = new Groups(e.To);
            foreach (var item in myGroup.Group)
                NewMessage += Send;

        }
         public void CreateGroup(string name)
         {
             Groups newGroup = new Groups(name);

         }
        public void AddToGroup(string name)
        {
            Groups newGroup = new Groups(name);
            if (name == newGroup.GroupName )
                newGroup.Group.Add(this);
        }
        public void GroupMessageNotification(Message message)
        {
            //foreach (var item in Groups Unknown)
             //   OnNewMessage(message);
        }
        public void MessageNotification(Message message)
        {
            if(message.Send)
            {
                ToConsole($"{message.To} has new message", ConsoleColor.DarkYellow);

            }
        }

    }
}
