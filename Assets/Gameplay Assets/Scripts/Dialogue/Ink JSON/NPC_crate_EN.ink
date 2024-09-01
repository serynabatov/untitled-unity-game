//Этот диалог предназначен для НПС головоломки с ящиками. С ним игрок встречается уже после того, как поговорил с основным НПС.
INCLUDE globals.ink
 //Эта переменная извне должна стать true в тот моент, когда игрок погвоорит с основным НПС.
{mainVarCrateFinished: ->main.finishedPuzzle|{mainVarCrate: ->main.prePurpose|->main}}
=== main ===
I've been waiting a long time for you. The sunset outside my tent lasts indefinitely; I don't know why, but it helps brighten the wait. #speaker:Lemas #portrait:Violet Mage 
Have you ever wondered what it would be like to wait for the end of a sunset that doesn't want to end? Few people seem to have such thoughts.
Since we don't have much time, let's get right to the point.
~ mainVarCrate = true
->purpose

= purpose
To pass the test, you must drag the crates to the place where their shadow is hidden and open the way up. It's simple; you don't even have to think of anything complicated.
+[I'll be back later.] #exit:0
->END
+[<b>PASS THE TEST</b>] #start:Crate Puzzle #exit:0
->END 

= prePurpose
Prince, I see you haven't passed the test yet. Need I remind you of the details? #speaker:Lemas #portrait:Violet Mage
+[Need a hint]
->purpose
+[No, goodbye] #exit:0
->END
+[<b>PASS THE TEST</b>] #start:Crate Puzzle #exit:0
->END 

= finishedPuzzle
Congratulations! You have passed the attention test and put the boxes in the right place. I hope you didn't get tired, remember if it was hard, you are probably doing something worthwhile. #speaker:Lemas #portrait:Violet Mage
->END