INCLUDE globals.ink

Greetings Traveler! Welcome to <color=\#F9B524>Hawai'i!</color>  #speaker: Opunui #portrait: NPC1
 ->introduction


==introduction==
 I am Opunui, it is my honor to serve this community as a <color=\#F9B524>Ho'okele</color>, a navigator. 
    
    I guide our ships when they head out on the water to fish and trade with our neighbors.
~islands_visited = 1
~visited_island_1 = true
-> main

==main==
What questions do you have?
   
+ [Directions]
 It is said that anyone who visits every other island will have the way to the central island shown to them.
 
 Sail <color=\#F9B524>Kona</color> (Southwest) from here to reach the next island.
 
 * [Compass]
          Opunui draws something on your notebook...
        ~max_pages = 2
        ~notebook_compass = true
        
        This is a <color=\#F9B524>Star Compass</color> It's what we use to keep track of directions when sailing. 
        
        It's somewhat similar to the english compass, but we have our own names for the directions.
        
      


- Do you have any more questions?

+ [no]
Travel safely on your journey, or as we say, E huakaʻi me ka palekana.
    -> END
    
+ [yes]
->main