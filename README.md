
# Command Library

Simple, probably bad library to register terminal commands.

## How do I register a command?

Use `Commands.RegisterCommand` and pass in the word that you want to trigger the command, the text to return when it is executed, the name of the terminal event (if you don't want to use this, just input an empty string), and your terminal instance.

## Why the fuck did you make such a small library?

I don't like having something that could be separated and reused incorporated in a mod.
