using System.Text.RegularExpressions;

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

            Match match = Regex.Match(requestMessage,"is (.*?) order"); // (.*?) 
        
            if( match.Success )
                MessageOrderNumber=match.Value.Replace("is ",string.Empty).Replace(" order",string.Empty);
 
        }

    }
}
