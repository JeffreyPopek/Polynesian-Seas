INCLUDE globals.ink

Hello Traveler, Welcome to <color=\#D65A40>Samoa!</color>  #speaker: Fetu #portrait: NPC2
->introduction

==introduction==
I am Fetu, a Tulafale. or "speaking chief" in your tounge. I serve as the voice for my Ali'i or "sitting chief".
    
     Although "chief" is not totally accurate.
     
     we are people who have been entrusted by our 'Aiga(extended family) to ensure their welfare.
     
     We serve them, not the other way around.
     ~islands_visited = 2
     ~visited_island_2 = true
     ->main
==main==
What questions do you have?

+ [Directions]
<color=\#D65A40>Hema</color> (south) of here you will find five rocks in the shape of a plus sign.

once you reach them sail <color=\#D65A40>Hikina</color> (East) to arrive at the next island.

One more thing, the sail of your <color=\#D65A40>Va'a</color>(boat) seems to be torn.

let's get that fixed up for you.

~player_speed = 2
 

- Do you have any more questions?

+ [no]
Manuia le malaga, have a good trip!
    -> END
    
+ [yes]
->main
