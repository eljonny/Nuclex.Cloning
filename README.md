[![Nuclex.Cloning Build-Test](https://github.com/eljonny/Nuclex.Cloning/actions/workflows/dotnet.yml/badge.svg)](https://github.com/eljonny/Nuclex.Cloning/actions/workflows/dotnet.yml)

Nuclex.Cloning
==============

This is a C# based .NET 4 library that is used to deep clone objects, whether they are serializable or not. 
It intends to be much faster than the normal binary serialization method of deep cloning objects.

This is basically just slightly tidied up code from http://blog.nuclex-games.com/mono-dotnet/fast-deep-cloning/ (still online as of May 6 2025), and
if it works well I intend to publish it to NuGet as well, as currently at 03/Dec/2013 there are only 2 other deep cloning libraries (none of which is as flexible as this, I believe).

This library can be used staticly or injected using the ICloneFactory interface.

Licensed under Common Public License V1 http://opensource.org/licenses/cpl1.0.php

Usage
--------------

    var original = new ComplicatedStuff(42);

	ComplicatedStuff clone1 = ReflectionCloner.DeepFieldClone(original);

or

    /* 
        You need to make sure you are invoking via ICloneFactory, the var keyword will not work since the implemented methods are 
        static 
    */
	 ICloneFactory cloner = new ReflectionCloner(); 
	 
	 var clone = cloner.DeepFieldClone(original);

# Update May 2025

The library can now be used with both .Net Framework and modern .Net, including 5+, Core, and Standard.

The original repo for this is: https://github.com/junweilee/Nuclex.Cloning/
