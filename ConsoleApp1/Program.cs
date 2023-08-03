// See https://aka.ms/new-console-template for more information
using Azure.AI.OpenAI;
using Azure;
using Microsoft.Extensions.Configuration;

Console.WriteLine("Azure OpenAI Serviceを利用する");

var configuration = new ConfigurationBuilder()
    .AddUserSecrets<Program>()
    .Build();


var client = new OpenAIClient(
    new Uri(configuration["Azure:OpenAI:EndpointURL"]),
    new AzureKeyCredential(configuration["Azure:OpenAI:ApiKey"])
    );

var chatCompletionsOptions = new ChatCompletionsOptions()
{
    Messages =
    {
        new ChatMessage(ChatRole.System, @"あなたは.NETアプリケーション開発のスペシャリストです。300文字以内で回答を返します。"),
        new ChatMessage(ChatRole.User, @"Windows Formsアプリケーションにデータグリッドを実装する方法を教えてください。"),
    },
    MaxTokens = 500
};

var response = await client.GetChatCompletionsAsync(configuration["Azure:OpenAI:DeployName"], chatCompletionsOptions);
var result = response.Value.Choices[0].Message.Content;

Console.WriteLine(result);
