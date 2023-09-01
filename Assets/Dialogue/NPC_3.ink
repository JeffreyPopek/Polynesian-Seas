INCLUDE globals.ink

Greetings Traveler! Welcome to <color=\#2BB627>Mo'orea!</color>  #speaker: Tiarenui #portrait: NPC3
->introduction

==introduction==
 My name is Tiarenui, I'm a farmer. 
 
 I help grow crops like taro and sweet potato in fields built into the hills of this island.
 ~islands_visited = 3
 ~visited_island_3 = true
 ->main
==main==
What questions do you have?
+ [Directions]

 The largest island my people inhabit is directly <color=\#2BB627>Hikina</color> 
 (East) of here. 
 
 but be sure to watch out for the rocks in-between!
 
- Do you have any more questions?

+ [no]
Nana!, until we meet again!
    -> END
    
+ [yes]
->main
