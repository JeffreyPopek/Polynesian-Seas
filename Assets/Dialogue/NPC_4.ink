INCLUDE globals.ink

 Welcome to <color=\#3A84E2>Tahiti</color>, Traveler. #speaker: Enoha #portrait: NPC4
 ->introduction
==introduction==
I am called Enoha by those who know me. I am a Tahu'a, or "one who knows".
~islands_visited = 4
~visited_island_4 = true
 
 + [Is a Tahu'a a priest?]
    
    Yes and no. We are keepers of knowledge. For some of us this knowledge is spiritual. 
    
    But other Tahu'a are knowledgable about different things.
    
    I myself am skilled at healing. When islanders are sick or injured, they come to me.
->main

==main==
What questions do you have?
 
+ [Directions]
 
  <color=\#3A84E2>Akau</color> (North) of here you wil find a tree in the water.
  
  sail <color=\#3A84E2>Ho'olua</color> (Northwest) from it to reach your destination.
 

- Do you have any more questions?

+ [no]
Nana, Traveler.
    -> END
    
+ [yes]
->main