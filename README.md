====================
== RPG ROGUE-LIKE ==
====================

Global architecture of the game can be found here: https://docs.google.com/a/isart.fr/drawings/d/1Z0IxE_CaeBvFC1KCRSHuWx0M2KcGtUNMjjmXMpOl9Lo/edit?usp=sharing

---------------------
Implemented features:
---------------------
= GUI: Main Menu / InGame / Pause / Magic / Inventry / Stats - implemented by Thomas and Quentin
= Inventory and Weapons - implemented by Adrien
= Audio - implemented by Adrien
= Dungeon and procedural - implemented by Quentin
= Characteristics - implemented by Quentin
= Camera - implemented by Thomas
= Magic and GUI actions - implemented by Thomas
= Controllers and Animations - implemented by Guillaume
= Characters / Player and Enemies - implemented by Guillaume

---------------------
Limitations and bugs:
---------------------
When moving up stairs, Character is very slow and if he jump too much, he could pass throught. 
Better way is going right and left while running.
Sometimes, the dungeon bug and create a few room at the same position.
Also, if th eplayer spam the spacebar to cast spell, it could become impossible to cast again.

------------
To be known:
------------
The Characters are made of multiple colliders, so the more colliders an attack touches, the more damages are made.

------
Input:
------

Z - to draw weapon
0 - stop casting spell