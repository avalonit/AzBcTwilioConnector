using System;
using Microsoft.Extensions.Configuration;

namespace com.businesscentral
{

    public partial class InputMessage
    {
        public enum MessageCommand { AskOrderDetail };

        public MessageCommand Command {get;set;}
        public string MessageOrderNumber {get;set;}

        public InputMessage(string requestMessage)
        {
           Command = MessageCommand.AskOrderDetail;
           MessageOrderNumber="20NB-ORD010";

        }

    }
}
