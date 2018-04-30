using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.Text;

namespace WcfChatPost
{
    [ServiceContract(SessionMode=SessionMode.Allowed)]
    public interface IChatService
    {
        [OperationContract]
        ChatUser ClientConnect(string userName);

        [OperationContract]
        List<ChatMessage> GetNewMessages(ChatUser user);

        [OperationContract]
        void SendNewMessage(ChatMessage newMessage);

        [OperationContract]
        List<ChatUser> GetAllUsers();

        [OperationContract]
        void RemoveUser(ChatUser user);
    }

    [DataContract]
    public class ChatMessage
    {
        private ChatUser user;
        private string message;
        private DateTime date;

        [DataMember]
        public ChatUser User
        {
            get { return user; }
            set { user = value; }
        }
        [DataMember]
        public string Message
        {
            get { return message; }
            set { message = value; }
        }
        [DataMember]
        public DateTime Date
        {
            get { return date; }
            set { date = value; }
        }
    }

    [DataContract]
    public class ChatUser
    {
        private string userName, ipAdress, hostName;

        [DataMember]
        public string UserName
        {
            get { return userName; }
            set { userName = value; }
        }
        [DataMember]
        public string IpAdress
        {
            get { return ipAdress; }
            set { ipAdress = value; }
        }
        [DataMember]
        public string HostName
        {
            get { return hostName; }
            set { hostName = value; }
        }
        public override string ToString()
        {
            return this.UserName;
        }

    }
}
