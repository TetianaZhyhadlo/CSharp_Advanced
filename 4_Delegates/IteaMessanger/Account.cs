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
        public List<Groups> GroupList { get; set; }// список груп Account
        public int QM{ get; set; }

        public List<Message> Messages { get; set; }

        public event OnSend OnSend;
        public Account User { get; set; }

        public OnMessage NewMessage { get; set; }

        public Account(string username)
        {
            Username = username;
            Messages = new List<Message>();
            GroupList = new List<Groups>();
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
        public void Subscribe(Account user)//Подписка на сообщение в группе
        {     
            User = user;
            User.OnSend += SendMessageToGroup;
            
        }


        public void SendMessageToGroup(object sender, OnSendEventArgs e)//отправка сообщения в группу
        {
            foreach (Groups a in GroupList)
            {
                if (a.GroupName == e.To)
                {
                    foreach (var item in a.Group)
                        NewMessage += Send;
                }
                else
                    Console.WriteLine("The message can not be sent. Group does not exist.");

                Console.WriteLine("Message to group is sent");//проверка
            }
            

        }
         public void CreateGroup(string name)//создание группы
         {
            Groups newGroup = new Groups(name);
            GroupList.Add(newGroup);
            Console.WriteLine(newGroup.GroupName);//проверка
         }
        public void AddToGroup(string name, Account x)//добавление Account в группу
        {
            foreach (Groups a in GroupList)
            {
                if (a.GroupName == name)
                {
                    a.Group.Add(x);
                }
                else
                    Console.WriteLine("Group does not exist, please check group name or create a new group.");

                Console.WriteLine(a.Group.Count());//проверка
            }
            
        }
        public void GroupMessageNotification(Message message)//уведомление всех Account в группе сообщением
        {
            if (message.Send)
            {
                foreach (Groups receive in GroupList)
                    if (message.To.Username == receive.GroupName)
                        foreach (Account any in receive.Group)
                            if(any!=message.From)
                                ToConsole($"OnNewMessage: {message.From.Username}: {message.Preview}", ConsoleColor.DarkYellow);
            }
        }
        public void MessageNotification(Message message)//уведомление всех Account в группе что есть новое сообщение
        {
            if (message.Send)
            {
                foreach (Groups receive in GroupList)
                    if (message.To.Username == receive.GroupName)
                        foreach (Account any in receive.Group)
                            if (any != message.From)
                                ToConsole($"You have a new message in {receive.GroupName}", ConsoleColor.DarkYellow);
            }
        }

    }
}
