using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace WcfChatPost
{
    public class ChatEngine
    {
        private List<ChatUser> connectedUsers = new List<ChatUser>();
        private Dictionary<string, List<ChatMessage>> incomingMessages = new Dictionary<string, List<ChatMessage>>(); 
        public List<ChatUser> ConnectedUsers
        {
            get { return connectedUsers; }
            set { connectedUsers = value; }
        }

        public ChatUser AddNewChatUser(ChatUser newUser)
        {
            var exists =
                from ChatUser e in this.ConnectedUsers
                where e.UserName == newUser.UserName
                select e;

            if (exists.Count() == 0)
            {
                this.ConnectedUsers.Add(newUser);
                incomingMessages.Add(newUser.UserName, new List<ChatMessage>()
                {
                    new ChatMessage(){User=newUser,Date=DateTime.Now,Message="Welcome to the ChaTiK"}
                });
                Console.WriteLine("\nNew user connected:" + newUser.UserName);
                return newUser;
            }
            else 
                return null;
            }

        public void AddNewMessage(ChatMessage newMessage)
        {
            Console.Write(newMessage.Date + ": " +  newMessage.User.UserName + ": " + newMessage.Message);
            foreach (var user in this.ConnectedUsers)
            {
                if(!newMessage.User.UserName.Equals(user.UserName))
                {
                    incomingMessages[user.UserName].Add(newMessage);
                }
            }
        }

        public List<ChatMessage> GetNewMessages(ChatUser user)
        {
            List<ChatMessage> myNewMessages = incomingMessages[user.UserName];
            incomingMessages[user.UserName] = new List<ChatMessage>();
            if (myNewMessages.Count > 0)
                return myNewMessages;
            else
                return null;
        }

        public void RemoveUser(ChatUser user)
        {
            this.ConnectedUsers.RemoveAll(u => u.UserName == user.UserName);
            this.ConnectedUsers.Remove(user);
        }
    }
}
