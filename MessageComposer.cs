using System;
using System.Text;

namespace com.businesscentral
{

    public partial class MessageComposer
    {
        public string DataBindMessage(SalesOrders orders, InputMessage inputMessage)
        {
            StringBuilder message= new StringBuilder();

            if( orders==null || orders.Value==null || orders.Value.Count<=0)
                return String.Format("Sorry, order {0} not found",inputMessage.MessageOrderNumber);

            if( orders.Value.Count>1)
                return String.Format("Sorry, there is more than one order with given number {0} ",inputMessage.MessageOrderNumber);

            SalesOrder order=orders.Value[0];

            message.Append(String.Format("The {0} order ",inputMessage.MessageOrderNumber));
            message.Append(String.Format("is in status {0} ",order.Status));
            
            if(order.FullyShipped)
                message.Append(String.Format("the order is fully shipped "));
            else if(order.PartialShipping)
                message.Append(String.Format("the order is partially shipped "));

            message.Append(String.Format("the customer is {0} ",order.CustomerName));
            message.Append(String.Format("the order amount including vat is {0} ",order.TotalAmountIncludingTax.ToString("N2")));

            if(order.RequestedDeliveryDate.Year!=1)
                message.Append(String.Format("the request delivery date is {0} ",order.RequestedDeliveryDate.ToString("dd/MM/yyyy")));

            if(order.PostingDate.Year!=1)
                message.Append(String.Format("the order has been posted on {0} ",order.PostingDate.ToString("dd/MM/yyyy")));

            if(order.LastModifiedDateTime.Year!=1)
                message.Append(String.Format("and last updated {0} ",order.LastModifiedDateTime.ToString("dd/MM/yyyy HH:mm")));

            return message.ToString();
        }

    }
}
