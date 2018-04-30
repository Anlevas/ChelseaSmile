using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.Text;

namespace WcfChatPost
{
   [ServiceBehavior(InstanceContextMode=InstanceContextMode.Single)]
    public class ChatService: IChatService
    {
        private ChatEngine mainEngine = new ChatEngine();

        public ChatUser ClientConnect(string userName)
        {
            return mainEngine.AddNewChatUser(new ChatUser() { UserName = userName });
        }

        public List<ChatMessage> GetNewMessages(ChatUser user)
        {
            return mainEngine.GetNewMessages(user);
        }

        public void SendNewMessage(ChatMessage newMessage)
        {
            mainEngine.AddNewMessage(newMessage);
        }

        public List<ChatUser> GetAllUsers()
        {
            return mainEngine.ConnectedUsers;
        }

        public void RemoveUser(ChatUser user)
        {
            mainEngine.RemoveUser(user);
        }
    }
}
