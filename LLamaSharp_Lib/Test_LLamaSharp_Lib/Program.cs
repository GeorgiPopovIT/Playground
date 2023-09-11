using System;
using System.Collections.Generic;
using LLama.Common;
using LLama;

string modelPath = "../../../models"; // change it to your own model path

var prompt = "Transcript of a dialog, where the User interacts with an Assistant named Bob. Bob is helpful, kind, honest, good at writing, and never fails to answer the User's requests immediately and with precision.\r\n\r\nUser: Hello, Bob.\r\nBob: Hello. How may I help you today?\r\nUser: Please tell me the largest city in Europe.\r\nBob: Sure. The largest city in Europe is Moscow, the capital of Russia.\r\nUser:"; // use the "chat-with-bob" prompt here.

// Initialize a chat session
var ex = new InteractiveExecutor(new LLamaModel(new ModelParams(modelPath, contextSize: 1024, seed: 1337, gpuLayerCount: 5)));
ChatSession session = new ChatSession(ex);

// show the prompt
Console.WriteLine();
Console.Write(prompt);

// run the inference in a loop to chat with LLM
while (true)
{
    foreach (var text in session.Chat(prompt, new InferenceParams() { Temperature = 0.6f, AntiPrompts = new List<string> { "User:" } }))
    {
        Console.Write(text);
    }

    Console.ForegroundColor = ConsoleColor.Green;
    prompt = Console.ReadLine();
    Console.ForegroundColor = ConsoleColor.White;
}