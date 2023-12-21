using LeggySetLib;
using System;

string[] allWords = File.ReadAllLines("words.txt");

ISet<int> badCharacters = new BinarySet64(0,25);


bool couldBeThisWord(string word)
{
    foreach (char letter in word)
    {
        if (badCharacters.Contains(getNumber(letter)))
        {
            return false;
        }
    }
    return true;
}

string? getGuess()
{
    foreach(string word in allWords)
    {
        if(couldBeThisWord(word))
        {
            return word;
        }
    }
    return null;
}

int getNumber(char letter)
{
    return (int)(letter) -  97;
}

Console.WriteLine("My first guess: audio");

while(true){
    Console.Write("Input letters that are not in the word: (e.g. asdf)\n");
    string input = Console.ReadLine();
    foreach(char character in input.ToLower())
    {
        badCharacters.Add(getNumber(character));
    }
    Console.WriteLine("My guess: "+getGuess());
}