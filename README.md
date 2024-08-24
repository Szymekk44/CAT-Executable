# CAT-Executable
A small c# "executable" format for CosmosOS projects!
> [!Caution]
> CAT Executable is still in development, not suitable for major use.<br>
> Contributions are welcome!

# How it works?
Cat utilizes a two-stage compilation process:

**Ra# (High-Level Language)
CLL (Cat Low-Level)**
These two stages are independent of each other, allowing flexibility in how you use or extend Cat. You can write your own high-level language that compiles down to Cat Low-Level (CLL), or you can directly write code in CLL without the need for a high-level language.
## CLL compilation
When working with CLL, the compilation process generates a .cat file. This file contains bytecode instructions that are executed by the Cat runner, a lightweight runtime written in C# without any additional libraries.
## Compatibility
The entire Cat system, including both compilation stages and the runtime, supports multiple platforms: Windows, Linux, Mac, and, most importantly, CosmosOS. This makes Cat a versatile and portable tool for various development environments.

> [!note]
> Check our to do list [here](ToDo.md)
