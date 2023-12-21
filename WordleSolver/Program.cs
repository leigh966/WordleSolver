using LeggySetLib;
using System;
using System.Net;

string[] allWords = File.ReadAllLines("words.txt");

ISet<int> badCharacters = new BinarySet32(0,25), goodCharacters = new BinarySet32(0,25);


char getLetter(int number)
{
    return (char)(number + 97);
}


bool goodCharactersCovered(string word)
{
    foreach (int number in goodCharacters)
    {
        if (!word.Contains(getLetter(number))) return false;
    }
    return true;
}

bool couldBeThisWord(string word)
{
    if(!goodCharactersCovered(word)) return false;
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
string lastGuess = "audio";
void getInput()
{
    Console.Write("Input letters that are not in the word: (e.g. asdf)\n");
    string input = Console.ReadLine();
    foreach (char character in input.ToLower())
    {
        badCharacters.Add(getNumber(character));
    }
    foreach(char character in lastGuess)
    {
        int charNumber = getNumber(character);
        if (!badCharacters.Contains(charNumber))
        {
            goodCharacters.Add(charNumber);
        }
    }
}

while(true){
    getInput();
    lastGuess = getGuess();
    Console.WriteLine("My guess: "+lastGuess);
}