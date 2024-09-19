//Этот диалог предназначен для главного НПС, который находится около клетки с трупом с флягой. С ним по сюжету игрок производит диалог впервые.
INCLUDE globals.ink
{mainVarCage: ->main.prePurpose|->main}

=== main ===
You've come, <color=\#F8FF30>prince</color>; we've been expecting you. The princess is in danger — no the kingdom is in danger, her highness in a burning castle. She must be rescued or the darkness will take over the kingdom and turn this blooming place into a desert. #speaker:Vádos #portrait:Green Mage

The enemy is too strong, our knights tried to rescue her, but none of them returned.
 + [...]
 
 - Princess Ailina has been holding back the darkness that is trying to consume the kingdom, but a sorcerer has overpowered her with a spell, and now she is in a coma.
 
For now, this forest is protected, and by obtaining the <color=\#F8FF30>nilúria</color> flower, we can awaken Ailina, and she will save us all. We have to hurry.
 ~ mainVarCage = true
  ->purpose
  
  = purpose
  You need to pass one trial from each of my brothers, and then you will be able to <color=\#F8FF30>fill the flask</color> with sacred water. This water will help the flower to bloom. Only then will you be able to use it to help Ailina.
  Explore the area, and you will meet my brothers. Talk to each of them; they will help you. Good luck and patience prince, you will need it.
  ~ certificate = true
  -> END
  
  = prePurpose
Prince, what are you doing here?! Need I remind you of your business? #speaker:Vádos #portrait:Green Mage 
+ [Yes]
->purpose
+ [No] #exit:0
->END