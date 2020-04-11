# AzBcTwilioConnector

This example shows a simple WhatsApp Integration by using Azure Functions and Business Central API.

SCENARIO

1. A sales man send a request with a Whatsapp message, eg: “What is XYZ order status?“.
2. The request is processed by an Azure function that queries Business Central API and gets the response.
3. The answer from Business Central is dispatched by the Azure function to the sales man via whatsapp chat.

The description of the project:
https://businesscentraldotblog.wordpress.com/2020/04/10/whatsapp-integration-easy-and-fast/