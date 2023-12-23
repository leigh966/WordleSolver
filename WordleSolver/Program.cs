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

IEnumerable<string> getGuesses()
{
    foreach(string word in allWords)
    {
        if(couldBeThisWord(word))
        {
            yield return word;
        }
    }
}

char?[] knownLetters = { null, null, null, null, null };

string[] misplacedLetters = { string.Empty, string.Empty, string.Empty, string.Empty, string.Empty };

string? getGuess()
{
    IEnumerable<string> guesses = getGuesses();
    foreach(string guess in guesses)
    {
        bool canBeThisWord = true;
        for(int i = 0; i < guess.Length; i++)
        {
            bool doesntHaveRequiredLetter = knownLetters[i] != null && knownLetters[i] != guess[i];
            bool letterCantBeHere = misplacedLetters[i].Contains(guess[i]);
            if (doesntHaveRequiredLetter || letterCantBeHere)
            {
                canBeThisWord = false;
                break;
            }
        }
        if (canBeThisWord)
        {
            return guess;
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
    string? input = Console.ReadLine();
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
    Console.WriteLine("Which letters are in the right place? (give indexes like this: 034)");
    input = Console.ReadLine();
    foreach(char character in input)
    {
        int index = int.Parse(character.ToString());
        knownLetters[index] = lastGuess[index];
    }
    for(int i = 0; i < 5; i++)
    {
        if (knownLetters[i]==null)
        {
            misplacedLetters[i] += lastGuess[i];
        }
    }
}

while(true){
    getInput();
    lastGuess = getGuess();
    Console.WriteLine("My guess: "+lastGuess);
}